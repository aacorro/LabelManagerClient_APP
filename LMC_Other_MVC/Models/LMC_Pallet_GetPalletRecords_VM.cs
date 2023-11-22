using LMC_Other_InventoryData.DB_Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMC_Other_MVC.Models
{
    public class LMC_Pallet_GetPalletRecords_VM
    {
        public List<LMC_Pallet_GetPalletRecords_Model>? Lo_Pallet_PalletRecordDetails { get; set; }
        [Required]
        [StringLength(12)]
        [DisplayName("Serial Number")]
        public string? SerialNumber { get; set; }

        [Required]
        [StringLength(2)]
        [DisplayName("Scale ID")]
        public string? ScaleID { get; set; }
    }
}
