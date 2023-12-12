using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_SyncTime_UpdateSyncTimeUploadRecordsController : Controller
    {
        // Declaring a private readonly field for the repository
        private readonly LMC_SyncTime_UpdateSyncTimeUploadRecords_Repo _repo;

        // Passing an instance of the repository through dependency injection
        public LMC_SyncTime_UpdateSyncTimeUploadRecordsController(LMC_SyncTime_UpdateSyncTimeUploadRecords_Repo repo)
        {
            // Assigning the provided repository instance to the private field
            _repo = repo;
        }

        [HttpGet] // HTTP GET action method for handling requests to the Index page
        public IActionResult Index()
        {
            try
            {
                // Calling the repository method to upload records and update synchronization time
                LMC_SyncTime_UpdateSyncTimeUploadRecords_Model oUploadRecordsResult = _repo.UploadRecords();

                // Checking if the result is not null
                if (oUploadRecordsResult != null)
                {
                    // Returning the view with the result
                    return View(oUploadRecordsResult); 
                }
                else
                {
                    // Returning the Error view if the result is null
                    return View("Error"); 
                }
                
            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("Error", ex.Message, ex.StackTrace);

                // Returning null in case of an exception
                return null; 
            }

        }
    }
}
