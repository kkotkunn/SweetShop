using System.Collections.Generic;
using System.ComponentModel;

namespace SweetShopServiceDAL.SViewModel
{
    public class SStockViewModel
    {
        public int SId { get; set; }
        [DisplayName("Название склада")]
        public string SStockName { get; set; }
        public List<StockIngredientViewModel> StockIngredients { get; set; }
    }
}
