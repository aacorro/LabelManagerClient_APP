using LMC_Other_InventoryData.DB_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMC_Other_InventoryData
{
    public class LMC_PrintStringLog_GetPrintStringsByPallet_Repo
    {
        private readonly IDbConnection _conn;

        public LMC_PrintStringLog_GetPrintStringsByPallet_Repo(IDbConnection conn)
        {
            _conn = conn;
        }

        public List<LMC_PrintStringLog_GetPrintStringsByPallet_Model> GetPrintStringsByPallet(string palletNumber)
        {
            List<LMC_PrintStringLog_GetPrintStringsByPallet_Model> lPrintStringsByPallet = new();
            LMC_PrintStringLog_GetPrintStringsByPallet_Model oPrintStringsByPallet;
            SqlDataReader rdr;

            try
            {
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetPrintStringsByPallet", (SqlConnection)_conn))
                {
                    _conn.Open();

                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    oSQLCmd.Parameters.Add(new SqlParameter("@PalletNumber", SqlDbType.VarChar, 18) { Value = palletNumber });

                    rdr = oSQLCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            oPrintStringsByPallet = new LMC_PrintStringLog_GetPrintStringsByPallet_Model();

                            oPrintStringsByPallet.pkPrintStringLogID = (int)rdr["pkPrintStringLogID"];
                            oPrintStringsByPallet.SerialNumber = rdr["SerialNumber"].ToString();
                            oPrintStringsByPallet.ProductNumber = rdr["ProductNumber"].ToString();
                            oPrintStringsByPallet.PalletNumber = rdr["PalletNumber"].ToString();
                            oPrintStringsByPallet.PrintString = rdr["PrintString"].ToString();
                            oPrintStringsByPallet.ScaleName = rdr["ScaleName"].ToString();
                            oPrintStringsByPallet.ScaleID = rdr["ScaleID"].ToString();
                            oPrintStringsByPallet.InsertDate = (DateTime)rdr["InsertDate"];

                            lPrintStringsByPallet.Add(oPrintStringsByPallet);
                        }
                    }
                }
                rdr.Close();

                return lPrintStringsByPallet;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
