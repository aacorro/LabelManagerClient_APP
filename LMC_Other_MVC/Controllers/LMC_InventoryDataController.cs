using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_InventoryDataController : Controller
    {
        // Declaring a private readonly field for the repository
        private readonly LMC_InventoryData_Repo _repo;

        // Passing an instance of the repository through dependency injection
        public LMC_InventoryDataController(LMC_InventoryData_Repo repo)
        {
            // Assigning the provided repository instance to the private field
            _repo = repo;
        }

        [HttpGet] // HTTP GET action method for handling requests to the Index page
        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Checking if either startDate or endDate is null
                if (startDate == null || endDate == null) 
                {
                    endDate = DateTime.Now; // Setting endDate to the current date and time if null
                    startDate = endDate.Value.AddDays(-30); // Setting startDate to 30 days before endDate

                }

                // Calling the repository method to get exported inventory data within the specified date range
                List<LMC_InventoryData_Model> lInvDataExported = _repo.Get_InventoryData_Exported(startDate.Value, endDate.Value);

                // Creating a view model instance to pass data to the view
                LMC_InventoryData_VM oInvDataVM = new LMC_InventoryData_VM();

                // Assigning values to the view model properties
                oInvDataVM.StartDate = startDate.Value;
                oInvDataVM.EndDate = endDate.Value;
                oInvDataVM.loInvDataExp = lInvDataExported;

                // Returning the view along with the populated view model
                return View(oInvDataVM);
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
