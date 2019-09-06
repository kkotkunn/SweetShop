namespace SweetShopModel
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class SweetIngredient
    {
        public int SId { get; set; }
        public int SweetId { get; set; }
        public int IngredientId { get; set; }
        public int SCount { get; set; }
    }
}
