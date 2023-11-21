using LMC_Other_InventoryData.DB_Models;

namespace LMC_Other_MVC.Models
{
    public class LMC_LabelCounter_GetUploadLabelCounterRecords_VM
    {
        public List<LMC_LabelCounter_GetUploadLabelCounterRecords_Model> loLabelCounterRecords {  get; set; }   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
