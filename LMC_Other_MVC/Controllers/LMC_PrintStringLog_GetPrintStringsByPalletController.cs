using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_PrintStringLog_GetPrintStringsByPalletController : Controller
    {
        private readonly LMC_PrintStringLog_GetPrintStringsByPallet_Repo _repo;

        public LMC_PrintStringLog_GetPrintStringsByPalletController(LMC_PrintStringLog_GetPrintStringsByPallet_Repo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index(string? palletNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(palletNumber))
                {
                    palletNumber = string.Empty;
                }

                List<LMC_PrintStringLog_GetPrintStringsByPallet_Model> lPrintStringLog_GetPrintStringsByPallet = _repo.GetPrintStringsByPallet(palletNumber);

                LMC_PrintStringLog_GetPrintStringsByPallet_VM oPrintStringLog_GetPrintStringsByPallet = new()
                {
                    lo_PrintStringLog_GetPrintStringsByPallet = lPrintStringLog_GetPrintStringsByPallet,
                    PalletNumber = palletNumber
                };

                return View(oPrintStringLog_GetPrintStringsByPallet);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");

                return View("Error");
            }
        }
    }
}

