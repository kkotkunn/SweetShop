using System;
using System.Collections.Generic;
using System.Linq;
using SweetShopModel;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SInterfaces;
using SweetShopServiceDAL.SViewModel;

namespace SweetShopServiceImplementList.SImplementations
{
    public class SMainServiceList : ISMainService
    {
        private SDataListSingleton source;
        public SMainServiceList()
        {
            source = SDataListSingleton.GetInstance();
        }
        public List<SOrderViewModel> GetList()
        {
            List<SOrderViewModel> result = source.SOrders
            .Select(rec => new SOrderViewModel
            {
                SId = rec.SId,
                SClientId = rec.SClientId,
                SweetId = rec.SweetId,
                SDateCreate = rec.SDateCreate.ToLongDateString(),
                SDateImplement = rec.SDateImplement?.ToLongDateString(),
                SStatus = rec.SStatus.ToString(),
                SCount = rec.SCount,
                SSum = rec.SSum,
                SClientFIO = source.SClients.FirstOrDefault(recC => recC.SId == rec.SClientId)?.SClientFIO,
                SweetName = source.Sweets.FirstOrDefault(recP => recP.SId == rec.SweetId)?.SweetName,
            })
            .ToList();
            return result;
        }
        public void CreateOrder(SOrderBindingModel model)
        {
            int maxId = source.SOrders.Count > 0 ? source.SOrders.Max(rec => rec.SId) : 0;
            source.SOrders.Add(new SOrder
            {
                SId = maxId + 1,
                SClientId = model.SClientId,
                SweetId = model.SweetId,
                SDateCreate = DateTime.Now,
                SCount = model.SCount,
                SSum = model.SSum,
                SStatus = SOrderStatus.Принят
            });
        }
        public void TakeOrderInWork(SOrderBindingModel model)
        {
            SOrder element = source.SOrders.FirstOrDefault(rec => rec.SId == model.SId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.SStatus != SOrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            var sweetIngredients = source.SweetIngredients.Where(rec => rec.SweetId == element.SweetId);
            foreach (var sweetIngredient in sweetIngredients)
            {
                int countOnStocks = source.StockIngredients
                .Where(rec => rec.IngredientId == sweetIngredient.IngredientId)
                .Sum(rec => rec.SCount);
                if (countOnStocks < sweetIngredient.SCount * element.SCount)
                {
                    var ingredientName = source.Ingredients.FirstOrDefault(rec => rec.SId == sweetIngredient.IngredientId);
                    throw new Exception("Не достаточно компонента " + ingredientName?.IngredientName + " требуется " + (sweetIngredient.SCount * element.SCount) + ", в наличии " + countOnStocks);
                }
            }
            foreach (var sweetIngredient in sweetIngredients)
            {
                int countOnStocks = sweetIngredient.SCount * element.SCount;
                var stockIngredients = source.StockIngredients.Where(rec => rec.IngredientId == sweetIngredient.IngredientId);
                foreach (var stockIngredient in stockIngredients)
                {
                    if (stockIngredient.SCount >= countOnStocks)
                    {
                        stockIngredient.SCount -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= stockIngredient.SCount;
                        stockIngredient.SCount = 0;
                    }
                }
            }
            element.SDateImplement = DateTime.Now;
            element.SStatus = SOrderStatus.Выполняется;
        }
        public void FinishOrder(SOrderBindingModel model)
        {
            SOrder element = source.SOrders.FirstOrDefault(rec => rec.SId == model.SId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.SStatus != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.SStatus = SOrderStatus.Готов;
        }
        public void PayOrder(SOrderBindingModel model)
        {
            SOrder element = source.SOrders.FirstOrDefault(rec => rec.SId == model.SId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.SStatus != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.SStatus = SOrderStatus.Оплачен;
        }
        public void PutIngredientOnStock(StockIngredientBindingModel model)
        {
            StockIngredient element = source.StockIngredients.FirstOrDefault(rec => rec.SStockId == model.SStockId && rec.IngredientId == model.IngredientId);
            if (element != null)
            {
                element.SCount += model.SCount;
            }
            else
            {
                int maxId = source.StockIngredients.Count > 0 ? source.StockIngredients.Max(rec => rec.SId) : 0;
                source.StockIngredients.Add(new StockIngredient
                {
                    SId = ++maxId,
                    SStockId = model.SStockId,
                    IngredientId = model.IngredientId,
                    SCount = model.SCount
                });
            }
        }
    }
}