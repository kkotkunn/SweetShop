using System.Collections.Generic;
using SweetShopModel;

namespace SweetShopServiceImplementList
{
    class SDataListSingleton
    {
        private static SDataListSingleton instance;
        public List<SClient> SClients { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<SOrder> SOrders { get; set; }
        public List<Sweet> Sweets { get; set; }
        public List<SweetIngredient> SweetIngredients { get; set; }
        public List<SStock> SStocks { get; set; }
        public List<StockIngredient> StockIngredients { get; set; }
        private SDataListSingleton()
        {
            SClients = new List<SClient>();
            Ingredients = new List<Ingredient>();
            SOrders = new List<SOrder>();
            Sweets = new List<Sweet>();
            SweetIngredients = new List<SweetIngredient>();
            SStocks = new List<SStock>();
            StockIngredients = new List<StockIngredient>();
        }
        public static SDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new SDataListSingleton();
            }
            return instance;
        }
    }
}

