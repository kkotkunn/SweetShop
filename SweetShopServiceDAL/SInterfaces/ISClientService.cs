using System.Collections.Generic;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SViewModel;

namespace SweetShopServiceDAL.SInterfaces
{
    public interface ISClientService
    {
        List<SClientViewModel> GetList();
        SClientViewModel GetElement(int id);
        void AddElement(SClientBindingModel model);
        void UpdElement(SClientBindingModel model);
        void DelElement(int id);
    }
}