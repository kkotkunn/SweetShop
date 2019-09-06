using System;
using System.Collections.Generic;
using System.Linq;
using SweetShopModel;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SInterfaces;
using SweetShopServiceDAL.SViewModel;

namespace SweetShopServiceImplementList.SImplementations
{
    public class SClientServiceList : ISClientService
    {
        private SDataListSingleton source;
        public SClientServiceList()
        {
            source = SDataListSingleton.GetInstance();
        }
        public List<SClientViewModel> GetList()
        {
            List<SClientViewModel> result = source.SClients.Select(rec => new SClientViewModel
            {
                SId = rec.SId,
                SClientFIO = rec.SClientFIO
            })
            .ToList();
            return result;
        }
        public SClientViewModel GetElement(int id)
        {
            SClient element = source.SClients.FirstOrDefault(rec => rec.SId == id);
            if (element != null)
            {
                return new SClientViewModel
                {
                    SId = element.SId,
                    SClientFIO = element.SClientFIO
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SClientBindingModel model)
        {
            SClient element = source.SClients.FirstOrDefault(rec => rec.SClientFIO == model.SClientFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.SClients.Count > 0 ? source.SClients.Max(rec => rec.SId) : 0;
            source.SClients.Add(new SClient
            {
                SId = maxId + 1,
                SClientFIO = model.SClientFIO
            });
        }
        public void UpdElement(SClientBindingModel model)
        {
            SClient element = source.SClients.FirstOrDefault(rec => rec.SClientFIO == model.SClientFIO && rec.SId != model.SId);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.SClients.FirstOrDefault(rec => rec.SId == model.SId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SClientFIO = model.SClientFIO;
        }
        public void DelElement(int id)
        {
            SClient element = source.SClients.FirstOrDefault(rec => rec.SId == id);
            if (element != null)
            {
                source.SClients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}