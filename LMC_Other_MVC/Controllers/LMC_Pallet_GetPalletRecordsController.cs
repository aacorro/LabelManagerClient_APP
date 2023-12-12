using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_Pallet_GetPalletRecordsController : Controller
    {
        // Declaring a private readonly field for the repository
        private readonly LMC_Pallet_GetPalletRecords_Repo _repo;

        // Passing an instance of the repository through dependency injection
        public LMC_Pallet_GetPalletRecordsController(LMC_Pallet_GetPalletRecords_Repo repo)
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
                    serialNumber = string.Empty; // Set serialNumber to empty string if null or empty
                    scaleID = string.Empty; // Set scaleID to empty string if null or empty
                }

                // Calling the repository method to get pallet records details based on serialNumber and scaleID
                List<LMC_Pallet_GetPalletRecords_Model> lPallet_PalletRecordDetails = string.IsNullOrEmpty(serialNumber) ? new List<LMC_Pallet_GetPalletRecords_Model>() : _repo.GetPalletRecordDetails(serialNumber, scaleID);

                // Creating a view model instance to pass data to the view
                LMC_Pallet_GetPalletRecords_VM oPalletGetRecordDetail = new()
                {
                    ScaleID = scaleID,
                    SerialNumber = serialNumber,
                    Lo_Pallet_PalletRecordDetails = lPallet_PalletRecordDetails
                };

                // Returning the view along with the populated view model
                return View(oPalletGetRecordDetail);
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
