using System;
using System.Collections.Generic;
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
            List<SClientViewModel> result = new List<SClientViewModel>();
            for (int i = 0; i < source.SClients.Count; ++i)
            {
                result.Add(new SClientViewModel
                {
                    SId = source.SClients[i].SId,
                    SClientFIO = source.SClients[i].SClientFIO
                });
            }
            return result;
        }
        public SClientViewModel GetElement(int id)
        {
            for (int i = 0; i < source.SClients.Count; ++i)
            {
                if (source.SClients[i].SId == id)
                {
                    return new SClientViewModel
                    {
                        SId = source.SClients[i].SId,
                        SClientFIO = source.SClients[i].SClientFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SClientBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.SClients.Count; ++i)
            {
                if (source.SClients[i].SId > maxId)
                {
                    maxId = source.SClients[i].SId;
                }
                if (source.SClients[i].SClientFIO == model.SClientFIO)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            source.SClients.Add(new SClient
            {
                SId = maxId + 1,
                SClientFIO = model.SClientFIO
            });
        }
        public void UpdElement(SClientBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.SClients.Count; ++i)
            {
                if (source.SClients[i].SId == model.SId)
                {
                    index = i;
                }
                if (source.SClients[i].SClientFIO == model.SClientFIO &&
                source.SClients[i].SId != model.SId)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.SClients[index].SClientFIO = model.SClientFIO;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.SClients.Count; ++i)
            {
                if (source.SClients[i].SId == id)
                {
                    source.SClients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}