namespace LMC_Other_InventoryData.DB_Models
{
    public class LMC_InventoryData_Model
    {
        public string? ScaleName { get; set; }
        public int? ScaleID { get; set; }
        public string? PalletNo { get; set; }
        public string? LotNo { get; set; }
        public int? PalletCount { get; set; }
        public DateTime? EarliestDate { get; set; }
        public DateTime? LastInventorySync { get; set; }
        public bool? Exported { get; set; }

    }
}