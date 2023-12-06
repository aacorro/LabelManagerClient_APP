using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_Pallet_GetNonCollectedController : Controller
    {
        private readonly LMC_Pallet_GetNonCollected_Repo _repo;

        public LMC_Pallet_GetNonCollectedController(LMC_Pallet_GetNonCollected_Repo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                List<LMC_Pallet_GetNonCollected_Model> lGetNonCollected_From_Repo = _repo.GetNonCollected();

                LMC_Pallet_GetNonCollected_VM oNonCollected_VM = new LMC_Pallet_GetNonCollected_VM();

                oNonCollected_VM.lNonCollected_From_VM = lGetNonCollected_From_Repo;
                
                return View(oNonCollected_VM);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
