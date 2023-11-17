using System.Data;
using System.Data.SqlClient;
using LMC_Other_InventoryData.DB_Models;

namespace LMC_Other_InventoryData
{
    public class LMC_InventoryData_Repo
    {
        private readonly IDbConnection _conn;

        public LMC_InventoryData_Repo(IDbConnection conn)
        {
            _conn = conn;
        }

        public List<LMC_InventoryData_Model> Get_InventoryData_Exported(DateTime startDate, DateTime endDate)
        {
            List<LMC_InventoryData_Model> lInvDataExp = new List<LMC_InventoryData_Model>();
            LMC_InventoryData_Model oInvDataExp;
            SqlDataReader rdr;

            try
            {
                using (SqlCommand oSQLCmd = new SqlCommand("usp_Get_All_No_Exported_Dynamic_W_ExportedColumn", (SqlConnection)_conn))
                {
                    _conn.Open();

                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    oSQLCmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = startDate });
                    oSQLCmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = endDate });

                    rdr = oSQLCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            oInvDataExp = new LMC_InventoryData_Model();

                            oInvDataExp.ScaleName = rdr["ScaleName"].ToString();
                            oInvDataExp.ScaleID = (int)rdr["ScaleID"];
                            oInvDataExp.PalletNo = rdr["PalletNo"].ToString();
                            oInvDataExp.LotNo = rdr["LotNo"].ToString();
                            oInvDataExp.PalletCount = (int)rdr["PalletCount"];
                            oInvDataExp.EarliestDate = (DateTime)rdr["EarliestDate"];
                            oInvDataExp.LastInventorySync = (DateTime)rdr["LastInventorySync"];
                            oInvDataExp.Exported = rdr["Exported"] as bool?;

                            lInvDataExp.Add(oInvDataExp);
                        }
                    }
                }
                rdr.Close();

                return lInvDataExp;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
