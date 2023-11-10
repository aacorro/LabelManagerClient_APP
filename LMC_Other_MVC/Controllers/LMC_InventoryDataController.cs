using LMC_Other_InventoryData;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_InventoryDataController : Controller
    {
        private readonly LMC_InventoryData_Repo _repo;

        public LMC_InventoryDataController(LMC_InventoryData_Repo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                endDate = DateTime.Now;
                startDate = endDate.Value.AddDays(-30);
                
            }

            List<LMC_InventoryData_Model> lInvDataExported = _repo.Get_InventoryData_Exported(startDate.Value, endDate.Value);

            LMC_InventoryData_VM oInvDataVM = new LMC_InventoryData_VM();

            oInvDataVM.StartDate = startDate.Value;
            oInvDataVM.EndDate = endDate.Value;
            oInvDataVM.loInvDataExp = lInvDataExported;

            return View(oInvDataVM);
        }
    }
}
