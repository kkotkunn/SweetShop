using System;

namespace SweetShopModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class SOrder
    {
        public int SId { get; set; }
        public int SClientId { get; set; }
        public int SweetId { get; set; }
        public int SCount { get; set; }
        public decimal SSum { get; set; }
        public SOrderStatus SStatus { get; set; }
        public DateTime SDateCreate { get; set; }
        public DateTime? SDateImplement { get; set; }
    }
}
