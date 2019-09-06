namespace SweetShopModel
{
    /// <summary>
    /// Сколько компонентов хранится на складе
    /// </summary>
    public class StockIngredient
    {
        public int SId { get; set; }
        public int SStockId { get; set; }
        public int IngredientId { get; set; }
        public int SCount { get; set; }
    }
}
