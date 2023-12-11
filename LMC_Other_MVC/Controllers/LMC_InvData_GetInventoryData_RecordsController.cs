using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_InvData_GetInventoryData_RecordsController : Controller
    {
        // Declaring a private readonly field for the repository
        private readonly LMC_InvData_GetInventoryDataRecords_Repo _repo;

        // Passing an instance of the repository through dependency injection
        public LMC_InvData_GetInventoryData_RecordsController(LMC_InvData_GetInventoryDataRecords_Repo repo)
        {
            // Assigning the provided repository instance to the private field
            _repo = repo;
        }

        [HttpGet]  // HTTP GET action method for handling requests to the Index page
        public IActionResult Index()
        {
            try
            {
                // Checking if the model state is valid
                if (!ModelState.IsValid)
                {
                    // Returning a bad request if the model state is not valid
                    return BadRequest(ModelState);
                }

                // Calling the repository method to get a list of inventory records
                List<LMC_GetUpload_InventoryRecords_Model> olInvDataRecords = _repo.GetUploadInventoryRecords();

                // Creating a view model instance to pass data to the view
                LMC_InvData_GetInventoryDataRecords_VM oViewModel = new LMC_InvData_GetInventoryDataRecords_VM();

                // Assigning the list of inventory records to the view model
                oViewModel.loInvDat_GetInvDataRecords = olInvDataRecords;

                // Returning the view along with the populated view model
                return View(oViewModel);
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
