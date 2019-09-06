using System.Collections.Generic;
using System.ComponentModel;

namespace SweetShopServiceDAL.SViewModel
{
    public class SweetViewModel
    {
        public int SId { get; set; }
        [DisplayName("Название продукта")]
        public string SweetName { get; set; }
        [DisplayName("Цена")]
        public decimal SPrice { get; set; }
        public List<SweetIngredientViewModel> SweetIngredients { get; set; }
    }
}
