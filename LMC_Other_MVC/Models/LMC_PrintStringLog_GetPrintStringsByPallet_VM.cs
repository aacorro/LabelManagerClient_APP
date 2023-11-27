using LMC_Other_InventoryData.DB_Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LMC_Other_MVC.Models
{
    public class LMC_PrintStringLog_GetPrintStringsByPallet_VM
    {
        public List<LMC_PrintStringLog_GetPrintStringsByPallet_Model> lo_PrintStringLog_GetPrintStringsByPallet {  get; set; }

        [Required]
        [StringLength(18)]
        [DisplayName("Pallet Number")]
        public string? PalletNumber { get; set; }
    }
}
