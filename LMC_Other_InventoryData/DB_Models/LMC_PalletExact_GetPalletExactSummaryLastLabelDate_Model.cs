﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMC_Other_InventoryData.DB_Models
{
    public class LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Model
    {
        public DateTime LastLabelDate { get; set; }

        public string ProductNumber { get; set; }
        public DateTime RunStartDate { get; set; }
        public DateTime RunEndDate { get; set; } = DateTime.Now;
    }
}
