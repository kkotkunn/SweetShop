using System.ComponentModel;

namespace SweetShopServiceDAL.SViewModel
{
    public class SweetIngredientViewModel
    {
        public int SId { get; set; }
        public int SweetId { get; set; }
        public int IngredientId { get; set; }
        [DisplayName("Ингредиент")]
        public string IngredientName { get; set; }
        [DisplayName("Количество")]
        public int SCount { get; set; }
    }
}