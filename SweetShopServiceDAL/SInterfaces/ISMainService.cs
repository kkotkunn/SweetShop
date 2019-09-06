using System.Collections.Generic;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SViewModel;

namespace SweetShopServiceDAL.SInterfaces
{
    public interface ISMainService
    {
        List<SOrderViewModel> GetList();
        void CreateOrder(SOrderBindingModel model);
        void TakeOrderInWork(SOrderBindingModel model);
        void FinishOrder(SOrderBindingModel model);
        void PayOrder(SOrderBindingModel model);
        void PutIngredientOnStock(StockIngredientBindingModel model);
    }
}
