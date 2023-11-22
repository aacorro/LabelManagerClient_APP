using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMC_Other_InventoryData.DB_Models
{
    public class LMC_Pallet_GetPalletRecords_Model
    {
        public string SerialNumber { get; set; }
        public string PalletNumber { get; set; }
        public string ProductNumber { get; set;}
        public string LotNumber { get; set;}
        public string? SartoriLotNumber { get; set;}
        public string? StockNumber { get; set;}
        public string? MfgNumber { get; set;}
        public DateTime? ProductDate { get; set;}
        public DateTime? PackDate { get; set;}
        public DateTime? MakeDate { get; set;}
        public decimal? CaseWeight { get; set;}
        public decimal? TareWeight { get; set;}
        public decimal? AverageWeight { get; set;}
        public int? ShelfLife { get; set;}
        public int? TotalCaseCount { get; set;}
        public string? LabelSize { get; set;}
        public string? Header { get; set;}
        public string? Description { get; set;}
        public DateTime? CollectionTime { get; set;}
        public bool? Collected { get; set;}
        public long ID { get; set;}
        public int? Exported { get; set;}
        public DateTime? InsertDate { get; set;}
    }
}
