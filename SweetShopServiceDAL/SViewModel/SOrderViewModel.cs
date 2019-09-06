using System.ComponentModel;

namespace SweetShopServiceDAL.SViewModel
{
    public class SOrderViewModel
    {
        public int SId { get; set; }
        public int SClientId { get; set; }
        [DisplayName("ФИО Клиента")]
        public string SClientFIO { get; set; }
        public int SweetId { get; set; }
        [DisplayName("Продукт")]
        public string SweetName { get; set; }
        [DisplayName("Количество")]
        public int SCount { get; set; }
        [DisplayName("Сумма")]
        public decimal SSum { get; set; }
        [DisplayName("Статус")]
        public string SStatus { get; set; }
        [DisplayName("Дата создания")]
        public string SDateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public string SDateImplement { get; set; }
    }
}
