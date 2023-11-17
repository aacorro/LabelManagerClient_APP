namespace LMC_Other_InventoryData.DB_Models
{
    public class LMC_InvData_RecordDetail_Model
    {
        public string? SerialNo { get; set; }
        public string? PalletNo { get; set; }
        public string? ProductNo { get; set; }
        public string? LotNo { get; set; }
        public string? SartoriLotNumber { get; set; }
        public string? StockNumber { get; set; }
        public string? MfgID { get; set; }
        public DateTime? ProductDate { get; set; }
        public DateTime? PackDate { get; set; }
        public DateTime? MakeDate { get; set; }
        public decimal? CaseWeight { get; set; }
        public decimal? TareWeight { get; set; }
        public decimal? AverageWeight { get; set; }
        public int? ShelfLife { get; set; }
        public int? TotalCaseCount { get; set; }
        public string? LabelSize { get; set; }
        public string? Header { get; set; }
        public string? Description { get; set; }
        public DateTime? CollectionTime { get; set; }
        public bool? Collected { get; set; }
        public long ID { get; set; }
        public bool? Exported { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}
