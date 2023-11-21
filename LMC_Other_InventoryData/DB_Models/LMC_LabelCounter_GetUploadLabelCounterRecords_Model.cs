using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMC_Other_InventoryData.DB_Models
{
    public class LMC_LabelCounter_GetUploadLabelCounterRecords_Model
    {
        public long pkLabelCounterID { get; set; }
        public string? ScaleName { get; set; }
        public int? ScaleID { get; set; }
        public string? VariationNo { get; set; }
        public string? ProductNo { get; set; }
        public int? CountGood { get; set; }
        public int? CountBad { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool? Exported { get; set; }
        public DateTime? ExportedDate { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
