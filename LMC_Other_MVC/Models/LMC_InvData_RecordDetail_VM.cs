using LMC_Other_InventoryData.DB_Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LMC_Other_MVC.Models
{
    public class LMC_InvData_RecordDetail_VM
    {
        public List<LMC_InvData_RecordDetail_Model>? Lo_InvData_RecordDetails { get; set; }

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
