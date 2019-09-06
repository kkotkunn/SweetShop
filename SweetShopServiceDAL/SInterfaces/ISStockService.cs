using System.Collections.Generic;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SViewModel;

namespace SweetShopServiceDAL.SInterfaces
{
    public interface ISStockService
    {
        List<SStockViewModel> GetList();
        SStockViewModel GetElement(int id);
        void AddElement(SStockBindingModel model);
        void UpdElement(SStockBindingModel model);
        void DelElement(int id);
    }
}
