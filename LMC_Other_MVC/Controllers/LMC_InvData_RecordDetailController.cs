using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_InvData_RecordDetailController : Controller
    {
        // Declaring a private readonly field for the repository
        private readonly LMC_InvData_RecordDetail_Repo _repo;

        // Passing an instance of the repository through dependency injection
        public LMC_InvData_RecordDetailController(LMC_InvData_RecordDetail_Repo repo)
        {
            // Assigning the provided repository instance to the private field
            _repo = repo;
        }

        [HttpGet] // HTTP GET action method for handling requests to the Index page
        public IActionResult Index(string? serialNumber, string? scaleID)
        {

            try
            {
                // Checking if either serialNumber or scaleID is null or empty
                if (string.IsNullOrEmpty(serialNumber) || string.IsNullOrEmpty(scaleID))
                {
                    serialNumber = string.Empty; 
                    scaleID = string.Empty;    
                }

                // Calling the repository method to get inventory record details based on serialNumber and scaleID
                List<LMC_InvData_RecordDetail_Model> lInvDataRecordDetails = string.IsNullOrEmpty(serialNumber) 
                    ? new List<LMC_InvData_RecordDetail_Model>()   // Creating an empty list if serialNumber is null or empty
                    : _repo.Get_Inventory_Record_Detail(serialNumber, scaleID);

                // Creating a view model instance to pass data to the view
                LMC_InvData_RecordDetail_VM oInvDataRecordDetailVM = new()
                {
                    ScaleID = scaleID,
                    SerialNumber = serialNumber,
                    Lo_InvData_RecordDetails = lInvDataRecordDetails
                };

                // Returning the view along with the populated view model
                return View(oInvDataRecordDetailVM);
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
