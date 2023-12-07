using LMC_Other_InventoryData;
using LMC_Other_InventoryData.DB_Models;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_InvData_GetInventoryData_RecordsController : Controller
    {

        private readonly LMC_InvData_GetInventoryDataRecords_Repo _repo;

        public LMC_InvData_GetInventoryData_RecordsController(LMC_InvData_GetInventoryDataRecords_Repo repo)
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

                List<LMC_GetUpload_InventoryRecords_Model> olInvDataRecords = _repo.GetUploadInventoryRecords();

                LMC_InvData_GetInventoryDataRecords_VM oViewModel = new LMC_InvData_GetInventoryDataRecords_VM();
                
                oViewModel.loInvDat_GetInvDataRecords = olInvDataRecords;

                return View(oViewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
