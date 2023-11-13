using LMC_Other_InventoryData;
using LMC_Other_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMC_Other_MVC.Controllers
{
    public class LMC_InvData_RecordDetailController : Controller
    {
        private readonly LMC_InvData_RecordDetail_Repo _repo;

        public LMC_InvData_RecordDetailController(LMC_InvData_RecordDetail_Repo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index(string serialNumber, string scaleID)
        {
            List<LMC_InvData_RecordDetail_Model> lInvDataRecordDetails = _repo.Get_Inventory_Record_Detail(serialNumber, scaleID);

            LMC_InvData_RecordDetail_VM oInvDataRecordDetailVM = new();

            oInvDataRecordDetailVM.ScaleId = scaleID;
            oInvDataRecordDetailVM.SerialNumber = serialNumber;
            oInvDataRecordDetailVM.lo_InvData_RecordDetails = lInvDataRecordDetails;

            return View(oInvDataRecordDetailVM);
        }
    }
}
