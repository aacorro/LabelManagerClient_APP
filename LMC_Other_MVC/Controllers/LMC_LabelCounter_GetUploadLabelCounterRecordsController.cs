using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_LabelCounter_GetUploadLabelCounterRecordsController : Controller
    {
        // Declaring a private readonly field for the repository
        private readonly LMC_LabelCounter_GetUploadLabelCounterRecords_Repo _repo;

        // Passing an instance of the repository through dependency injection
        public LMC_LabelCounter_GetUploadLabelCounterRecordsController(LMC_LabelCounter_GetUploadLabelCounterRecords_Repo repo)
        {
            // Assigning the provided repository instance to the private field
            _repo = repo;
        }

        [HttpGet]  // HTTP GET action method for handling requests to the Index page
        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Checking if either startDate or endDate is null
                if (startDate == null || endDate == null)
                {
                    endDate = DateTime.Now; // Setting endDate to the current date and time if null
                    startDate = DateTime.Now;  // Setting startDate to the current date and time if null
                }

                // Calling the repository method to get label counter records within the specified date range
                List<LMC_LabelCounter_GetUploadLabelCounterRecords_Model> lLabelCounterRecords = _repo.GetUploadLabelCounterRecords_Repos(startDate, endDate);

                // Creating a view model instance to pass data to the view
                LMC_LabelCounter_GetUploadLabelCounterRecords_VM oLabelCounterRecordVM = new();

                // Assigning values to the view model properties
                oLabelCounterRecordVM.StartDate = startDate.Value;
                oLabelCounterRecordVM.EndDate = endDate.Value;
                oLabelCounterRecordVM.loLabelCounterRecords = lLabelCounterRecords;

                // Returning the view along with the populated view model
                return View(oLabelCounterRecordVM);
            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("Error", ex.Message, ex.StackTrace);

                return null; // Return null in case of an exception
            }

        }
    }
}
