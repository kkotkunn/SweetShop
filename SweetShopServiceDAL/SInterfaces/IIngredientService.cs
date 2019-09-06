using System.Collections.Generic;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SViewModel;

namespace SweetShopServiceDAL.SInterfaces
{
    public interface IIngredientService
    {
        List<IngredientViewModel> GetList();
        IngredientViewModel GetElement(int id);
        void AddElement(IngredientBindingModel model);
        void UpdElement(IngredientBindingModel model);
        void DelElement(int id);
    }
}