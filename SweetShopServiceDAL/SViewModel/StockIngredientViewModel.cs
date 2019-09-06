using System.ComponentModel;

namespace SweetShopServiceDAL.SViewModel
{
    public class StockIngredientViewModel
    {
        public int SId { get; set; }
        public int SStockId { get; set; }
        public int IngredientId { get; set; }
        [DisplayName("Название компонента")]
        public string IngredientName { get; set; }
        [DisplayName("Количество")]
        public int SCount { get; set; }
    }
}
