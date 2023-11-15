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
        public IActionResult Index(string serialNumber, string? scaleID)
        {

            try
            {
                if (string.IsNullOrEmpty(scaleID) || string.IsNullOrEmpty(scaleID))
                {
                    serialNumber = string.Empty;
                    scaleID = string.Empty;
                }

                Console.WriteLine($"SerialNumber: {serialNumber}, ScaleID: {scaleID}");

                List<LMC_InvData_RecordDetail_Model> lInvDataRecordDetails = _repo.Get_Inventory_Record_Detail(serialNumber, scaleID);

                LMC_InvData_RecordDetail_VM oInvDataRecordDetailVM = new()
                {
                    ScaleID = scaleID,
                    SerialNumber = serialNumber,
                    Lo_InvData_RecordDetails = lInvDataRecordDetails
                };

                return View(oInvDataRecordDetailVM);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");

                return View("Error");
            }
            
        }
    }
}
