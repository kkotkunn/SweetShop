using System.Collections.Generic;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SViewModel;

namespace SweetShopServiceDAL.SInterfaces
{
    public interface ISweetService
    {
        List<SweetViewModel> GetList();
        SweetViewModel GetElement(int id);
        void AddElement(SweetBindingModel model);
        void UpdElement(SweetBindingModel model);
        void DelElement(int id);
    }
}