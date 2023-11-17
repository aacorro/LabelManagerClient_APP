using LMC_Other_InventoryData.DB_Models;
namespace LMC_Other_MVC.Models
{
    public class LMC_InventoryData_VM
    {
        public List<LMC_InventoryData_Model> loInvDataExp {get;set;}
        public DateTime StartDate { get;set;}
        public DateTime EndDate { get;set;}

    }
}
