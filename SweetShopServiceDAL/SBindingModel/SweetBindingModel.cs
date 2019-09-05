using System.Collections.Generic;

namespace SweetShopServiceDAL.SBindingModel
{
    public class SweetBindingModel
    {
        public int SId { get; set; }
        public string SweetName { get; set; }
        public decimal SPrice { get; set; }
        public List<SweetIngredientBindingModel> SweetIngredients { get; set; }
    }
}