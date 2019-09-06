using System;
using System.Collections.Generic;
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
            List<SOrderViewModel> result = new List<SOrderViewModel>();
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                string clientFIO = string.Empty;
                for (int j = 0; j < source.SClients.Count; ++j)
                {
                    if (source.SClients[j].SId == source.SOrders[i].SClientId)
                    {
                        clientFIO = source.SClients[j].SClientFIO;
                        break;
                    }
                }
                string sweetName = string.Empty;
                for (int j = 0; j < source.Sweets.Count; ++j)
                {
                    if (source.Sweets[j].SId == source.SOrders[i].SweetId)
                    {
                        sweetName = source.Sweets[j].SweetName;
                        break;
                    }
                }
                result.Add(new SOrderViewModel
                {
                    SId = source.SOrders[i].SId,
                    SClientId = source.SOrders[i].SClientId,
                    SClientFIO = clientFIO,
                    SweetId = source.SOrders[i].SweetId,
                    SweetName = sweetName,
                    SCount = source.SOrders[i].SCount,
                    SSum = source.SOrders[i].SSum,
                    SDateCreate = source.SOrders[i].SDateCreate.ToLongDateString(),
                    SDateImplement = source.SOrders[i].SDateImplement?.ToLongDateString(),
                    SStatus = source.SOrders[i].SStatus.ToString()
                });
            }
            return result;
        }
        public void CreateOrder(SOrderBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                if (source.SOrders[i].SId > maxId)
                {
                    maxId = source.SOrders[i].SId;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                if (source.SOrders[i].SId == model.SId)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.SOrders[index].SStatus != SOrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            source.SOrders[index].SDateImplement = DateTime.Now;
            source.SOrders[index].SStatus = SOrderStatus.Выполняется;
        }
        public void FinishOrder(SOrderBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                if (source.SOrders[i].SId == model.SId)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.SOrders[index].SStatus != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            source.SOrders[index].SStatus = SOrderStatus.Готов;
        }
        public void PayOrder(SOrderBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.SOrders.Count; ++i)
            {
                if (source.SOrders[i].SId == model.SId)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            if (source.SOrders[index].SStatus != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            source.SOrders[index].SStatus = SOrderStatus.Оплачен;
        }
    }
}
