using System.ComponentModel;

namespace SweetShopServiceDAL.SViewModel
{
    public class SClientViewModel
    {
        public int SId { get; set; }
        [DisplayName("ФИО Клиента")]
        public string SClientFIO { get; set; }
    }
}