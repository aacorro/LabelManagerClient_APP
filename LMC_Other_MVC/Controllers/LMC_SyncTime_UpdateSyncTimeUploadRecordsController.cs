using LMC_Other_InventoryData.DB_Models;
using Microsoft.AspNetCore.Mvc;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_SyncTime_UpdateSyncTimeUploadRecordsController : Controller
    {
        private readonly LMC_SyncTime_UpdateSyncTimeUploadRecords_Repo _repo;

        public LMC_SyncTime_UpdateSyncTimeUploadRecordsController(LMC_SyncTime_UpdateSyncTimeUploadRecords_Repo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                LMC_SyncTime_UpdateSyncTimeUploadRecords_Model oUploadRecordsResult = _repo.UploadRecords();

                if (oUploadRecordsResult != null)
                {
                    return View(oUploadRecordsResult);
                }
                else
                {
                    return View("Error");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                return View("Error");
            }
            
        }
    }
}
