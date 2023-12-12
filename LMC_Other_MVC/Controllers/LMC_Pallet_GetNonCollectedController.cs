using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_Pallet_GetNonCollectedController : Controller
    {
        // Declaring a private readonly field for the repository
        private readonly LMC_Pallet_GetNonCollected_Repo _repo;

        // Passing an instance of the repository through dependency injection
        public LMC_Pallet_GetNonCollectedController(LMC_Pallet_GetNonCollected_Repo repo)
        {
            // Assigning the provided repository instance to the private field
            _repo = repo;
        }

        [HttpGet] // HTTP GET action method for handling requests to the Index page
        public IActionResult Index()
        {
            try
            {
                // Checking if the model state is not valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Getting a list of non-collected items from the repository
                List<LMC_Pallet_GetNonCollected_Model> lGetNonCollected_From_Repo = _repo.GetNonCollected();

                // Creating a view model for non-collected items instance to pass data to the view
                LMC_Pallet_GetNonCollected_VM oNonCollected_VM = new LMC_Pallet_GetNonCollected_VM();

                // Assigning the list of non-collected items to the view model
                oNonCollected_VM.lNonCollected_From_VM = lGetNonCollected_From_Repo;

                // Returning the Index view with the non-collected items view model
                return View(oNonCollected_VM);
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
