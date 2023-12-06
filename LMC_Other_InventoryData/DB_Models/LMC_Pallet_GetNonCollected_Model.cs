namespace LMC_Other_InventoryData.DB_Models
{
    public class LMC_Pallet_GetNonCollected_Model
    {
        public string PalletNumber { get; set; }
        public string ProductNumber { get; set; }
        public string LotNumber { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? InsertDate { get; set; }
        public string? Description { get; set; }
        public bool? Collected { get; set; }

    }
}
