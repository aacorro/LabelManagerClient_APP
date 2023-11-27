using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_Pallet_GetPalletRecordsController : Controller
    {
        private readonly LMC_Pallet_GetPalletRecords_Repo _repo;

        public LMC_Pallet_GetPalletRecordsController(LMC_Pallet_GetPalletRecords_Repo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index(string? serialNumber, string? scaleID)
        {
            try
            {
                if (string.IsNullOrEmpty(serialNumber) || string.IsNullOrEmpty(scaleID))
                {
                    serialNumber = string.Empty;
                    scaleID = string.Empty;
                }

                List<LMC_Pallet_GetPalletRecords_Model> lPallet_PalletRecordDetails = string.IsNullOrEmpty(serialNumber) ? new List<LMC_Pallet_GetPalletRecords_Model>() : _repo.GetPalletRecordDetails(serialNumber, scaleID);

                LMC_Pallet_GetPalletRecords_VM oPalletGetRecordDetail = new()
                {
                    ScaleID = scaleID,
                    SerialNumber = serialNumber,
                    Lo_Pallet_PalletRecordDetails = lPallet_PalletRecordDetails
                };
                return View(oPalletGetRecordDetail);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");

                return View("Error");
            }

        }
    }
}
