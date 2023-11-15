using LMC_Other_InventoryData;
using System.ComponentModel.DataAnnotations;

namespace LMC_Other_MVC.Models
{
    public class LMC_InvData_RecordDetail_VM
    {
        public List<LMC_InvData_RecordDetail_Model>? Lo_InvData_RecordDetails { get; set; }

        //[Required(ErrorMessage = "Serial Number is required")]
        //[StringLength(12, ErrorMessage = "Serial Number must be 3 characters")]
        public required string SerialNumber { get; set; }

        //[Required(ErrorMessage = "Scale ID is required")]
        //[StringLength(3, ErrorMessage = "Scale ID must be 3 characters")]
        public string? ScaleID { get; set; }
    }
}
