using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMC_Other_InventoryData
{
    public class LMC_InventoryData_Model
    {
        public string ScaleName { get; set; }
        public int ScaleID { get; set; }
        public string PalletNo { get; set; }
        public string LotNo { get; set;}
        public bool Exported { get; set;} //null?
        public int PalletCount { get; set;}
        public DateTime EarliestDate { get; set;}
        public DateTime LastInventorySync { get; set;}
    }
}