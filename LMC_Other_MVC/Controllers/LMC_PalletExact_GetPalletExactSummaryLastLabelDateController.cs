using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using Microsoft.AspNetCore.Mvc;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_PalletExact_GetPalletExactSummaryLastLabelDateController : Controller
    {
        private readonly LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Repo _repo;

        public LMC_PalletExact_GetPalletExactSummaryLastLabelDateController(LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Repo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string productNumber, DateTime runStartDate, DateTime runEndDate)
        {
            try
            {
                LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Model result = await _repo.GetPalletExactSummaryLastLabelDate(productNumber, runStartDate, runEndDate);

                return View(result);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }

        }
    }
}
