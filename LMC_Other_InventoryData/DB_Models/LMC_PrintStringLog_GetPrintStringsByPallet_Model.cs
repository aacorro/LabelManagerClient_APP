using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMC_Other_InventoryData.DB_Models
{
    public class LMC_PrintStringLog_GetPrintStringsByPallet_Model
    {   
        public int pkPrintStringLogID {  get; set; }
        public string? SerialNumber { get; set; }
        public string? ProductNumber { get; set; }
        public string? PalletNumber { get; set; }
        public string? PrintString { get; set; }
        public string? ScaleName { get; set; }
        public string? ScaleID { get; set; }
        public DateTime? InsertDate { get; set; }

    }
}
