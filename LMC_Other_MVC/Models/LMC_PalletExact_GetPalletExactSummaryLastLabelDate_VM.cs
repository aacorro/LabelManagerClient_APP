namespace LMC_Other_MVC.Models
{
    public class LMC_PalletExact_GetPalletExactSummaryLastLabelDate_VM
    {
        public string ProductNumber { get; set; }
        public DateTime RunStartDate { get; set; }
        public DateTime RunEndDate { get; set; } = DateTime.Now;
        public DateTime LastLabelDate { get; set; }
    }
}
