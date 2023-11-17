using LMC_Other_InventoryData;
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
            var olInvDataRecords =  _repo.GetUploadInventoryRecords();

            var viewModel = new LMC_InvData_GetInventoryDataRecords_VM
            {
                loInvDat_GetInvDataRecords = olInvDataRecords,
            };
            return View(viewModel);
        }
    }
}
