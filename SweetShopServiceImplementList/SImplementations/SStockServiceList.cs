using System;
using System.Collections.Generic;
using System.Linq;
using SweetShopModel;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SInterfaces;
using SweetShopServiceDAL.SViewModel;

namespace SweetShopServiceImplementList.SImplementations
{
    public class SStockServiceList : ISStockService
    {
        private SDataListSingleton source;
        public SStockServiceList()
        {
            source = SDataListSingleton.GetInstance();
        }
        public List<SStockViewModel> GetList()
        {
            List<SStockViewModel> result = source.SStocks
            .Select(rec => new SStockViewModel
            {
                SId = rec.SId,
                SStockName = rec.SStockName,
                StockIngredients = source.StockIngredients
            .Where(recPC => recPC.SStockId == rec.SId)
            .Select(recPC => new StockIngredientViewModel
            {
                SId = recPC.SId,
                SStockId = recPC.SStockId,
                IngredientId = recPC.IngredientId,
                IngredientName = source.Ingredients
            .FirstOrDefault(recC => recC.SId == recPC.IngredientId)?.IngredientName,
                SCount = recPC.SCount
            })
            .ToList()
            })
            .ToList();
            return result;
        }
        public SStockViewModel GetElement(int id)
        {
            SStock element = source.SStocks.FirstOrDefault(rec => rec.SId == id);
            if (element != null)
            {
                return new SStockViewModel
                {
                    SId = element.SId,
                    SStockName = element.SStockName,
                    StockIngredients = source.StockIngredients
                .Where(recPC => recPC.SStockId == element.SId)
                .Select(recPC => new StockIngredientViewModel
                {
                    SId = recPC.SId,
                    SStockId = recPC.SStockId,
                    IngredientId = recPC.IngredientId,
                    IngredientName = source.Ingredients
                .FirstOrDefault(recC => recC.SId == recPC.IngredientId)?.IngredientName,
                    SCount = recPC.SCount
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SStockBindingModel model)
        {
            SStock element = source.SStocks.FirstOrDefault(rec => rec.SStockName ==
            model.SStockName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.SStocks.Count > 0 ? source.SStocks.Max(rec => rec.SId) : 0;
            source.SStocks.Add(new SStock
            {
                SId = maxId + 1,
                SStockName = model.SStockName
            });
        }
        public void UpdElement(SStockBindingModel model)
        {
            SStock element = source.SStocks.FirstOrDefault(rec =>
            rec.SStockName == model.SStockName && rec.SId != model.SId);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.SStocks.FirstOrDefault(rec => rec.SId == model.SId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SStockName = model.SStockName;
        }
        public void DelElement(int id)
        {
            SStock element = source.SStocks.FirstOrDefault(rec => rec.SId == id);
            if (element != null)
            {
                source.StockIngredients.RemoveAll(rec => rec.SStockId == id);
                source.SStocks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}

