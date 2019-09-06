using System.ComponentModel;

namespace SweetShopServiceDAL.SViewModel
{
    public class IngredientViewModel
    {
        public int SId { get; set; }
        [DisplayName("Название компонента")]
        public string IngredientName { get; set; }
    }
}
