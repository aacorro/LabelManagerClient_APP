using LMC_Other_InventoryData;

namespace LMC_Other_MVC.Models
{
    public class LMC_InvData_RecordDetail_VM
    {
        public List<LMC_InvData_RecordDetail_Model> lo_InvData_RecordDetails { get; set; }
        public string SerialNumber { get; set; }
        public string ScaleId { get; set; }
    }
}
