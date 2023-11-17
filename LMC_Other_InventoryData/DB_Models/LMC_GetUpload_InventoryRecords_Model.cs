using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMC_Other_InventoryData.DB_Models
{
    public class LMC_GetUpload_InventoryRecords_Model
    {
        [Key]
        public string SerialNo { get; set; }
        public string? ProductNo { get; set; }
        public DateTime? BestBy { get; set; }
        public decimal? CaseWeight { get; set; }
        public DateTime? PackDate { get; set; }
        public string? ScaleName { get; set; }
        public int? ScaleID { get; set; }
        public string? PalletNo { get; set; }
        public string? LotNo { get; set; }
        public long ID { get; set; }
        public string? MfgID { get; set; }
        public DateTime? CollectionTime { get; set; }
        public bool? Exported { get; set; }
        public bool? ExportFailed { get; set; }
        public int? FailureCount { get; set; }
        public DateTime? LastFailure {  get; set; }
        public DateTime? InsertDate { get; set; } = DateTime.Now;

    }
}
   