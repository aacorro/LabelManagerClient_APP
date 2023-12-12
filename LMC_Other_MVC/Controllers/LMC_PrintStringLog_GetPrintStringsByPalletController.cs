using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System;
using Serilog;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_PrintStringLog_GetPrintStringsByPalletController : Controller
    {
        // Declaring a private readonly field for the repository
        private readonly LMC_PrintStringLog_GetPrintStringsByPallet_Repo _repo;

        // Passing an instance of the repository through dependency injection
        public LMC_PrintStringLog_GetPrintStringsByPalletController(LMC_PrintStringLog_GetPrintStringsByPallet_Repo repo)
        {
            // Assigning the provided repository instance to the private field
            _repo = repo;
        }

        [HttpGet] // HTTP GET action method for handling requests to the Index page
        public IActionResult Index(string? palletNumber)
        {
            try
            {
                // Checking if palletNumber is null or empty or contains only whitespaces
                if (string.IsNullOrWhiteSpace(palletNumber))
                {
                    // Setting palletNumber to empty string if null, empty, or contains only whitespaces
                    palletNumber = string.Empty;
                }

                // Calling the repository method to get print strings by pallet
                List<LMC_PrintStringLog_GetPrintStringsByPallet_Model> lPrintStringLog_GetPrintStringsByPallet = _repo.GetPrintStringsByPallet(palletNumber);

                // Creating a view model instance to pass data to the view
                LMC_PrintStringLog_GetPrintStringsByPallet_VM oPrintStringLog_GetPrintStringsByPallet = new()
                {
                    lo_PrintStringLog_GetPrintStringsByPallet = lPrintStringLog_GetPrintStringsByPallet,
                    PalletNumber = palletNumber
                };

                // Returning the view along with the populated view model
                return View(oPrintStringLog_GetPrintStringsByPallet);
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

