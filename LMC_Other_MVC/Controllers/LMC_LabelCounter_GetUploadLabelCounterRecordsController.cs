using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_LabelCounter_GetUploadLabelCounterRecordsController : Controller
    {
        private readonly LMC_LabelCounter_GetUploadLabelCounterRecords_Repo _repo;

        public LMC_LabelCounter_GetUploadLabelCounterRecordsController(LMC_LabelCounter_GetUploadLabelCounterRecords_Repo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            try
            {

                if (startDate == null || endDate == null)
                {
                    endDate = DateTime.Now;
                    startDate = DateTime.Now;
                }
                List<LMC_LabelCounter_GetUploadLabelCounterRecords_Model> lLabelCounterRecords = _repo.GetUploadLabelCounterRecords_Repos(startDate, endDate);

                LMC_LabelCounter_GetUploadLabelCounterRecords_VM oLabelCounterRecordVM = new();

                oLabelCounterRecordVM.StartDate = startDate.Value;
                oLabelCounterRecordVM.EndDate = endDate.Value;
                oLabelCounterRecordVM.loLabelCounterRecords = lLabelCounterRecords;

                return View(oLabelCounterRecordVM);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return View("Error");
            }

        }
    }
}
