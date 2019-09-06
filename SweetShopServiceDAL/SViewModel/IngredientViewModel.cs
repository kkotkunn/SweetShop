using System.ComponentModel;

namespace SweetShopServiceDAL.SViewModel
{
    public class IngredientViewModel
    {
        public int SId { get; set; }
        [DisplayName("Название ингредиента")]
        public string IngredientName { get; set; }
    }
}