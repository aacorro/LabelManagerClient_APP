﻿using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_PalletExact_GetPalletExactSummaryLastLabelDateController : Controller
    {
        // Declaring a private readonly field for the repository
        private readonly LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Repo _repo;

        // Passing an instance of the repository through dependency injection
        public LMC_PalletExact_GetPalletExactSummaryLastLabelDateController(LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Repo repo)
        {
            // Assigning the provided repository instance to the private field
            _repo = repo;
        }

        [HttpGet] // HTTP GET action method for handling requests to the Index page
        public async Task<IActionResult> Index(string productNumber, DateTime? runStartDate, DateTime? runEndDate)
        {
            try
            {
                // Use null-coalescing operator to set default values if not provided
                runStartDate ??= DateTime.Now;
                runEndDate ??= DateTime.Now;

                // Calling the repository method to get pallet exact summary with the last label date
                LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Model result = await _repo.GetPalletExactSummaryLastLabelDate(productNumber, (DateTime)runStartDate, (DateTime)runEndDate);

                // Check if the result is null or if the LastLabelDate is DateTime.MinValue
                if (result != null && result.LastLabelDate != DateTime.MinValue)
                {
                    // Returning the view along with the result
                    return View(result);
                }
                else
                {
                    // Log the case where no data is available
                    Log.Information("No data available for the selected data range");

                    // Return a view for the case where no data is available
                    return View("NoDataView"); 
                }

            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("Error", ex.Message, ex.StackTrace);

                // Return null in case of an exception
                return null; 
            }

        }
    }
}
