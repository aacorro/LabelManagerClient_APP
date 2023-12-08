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
    public class LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Repo
    {
        private readonly IDbConnection _conn;

        public LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Repo(IDbConnection conn)
        {
            _conn = conn;
        }

        public async Task<LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Model> GetPalletExactSummaryLastLabelDate(string productNumber, DateTime runStartDate, DateTime runEndDate)
        {
            LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Model oLastLabelDate = new();
            SqlDataReader rdr;

            try
            {
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetPalletExactSummaryLastLabelDate", (SqlConnection)_conn))
                {
                    _conn.Open();

                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    oSQLCmd.Parameters.AddWithValue("@ProductNumber", productNumber);
                    oSQLCmd.Parameters.AddWithValue("@RunStartDate", runStartDate);
                    oSQLCmd.Parameters.AddWithValue("@RunEndDate", runEndDate);

                    //Output parameter
                    SqlParameter lastLabelDateParameter = oSQLCmd.Parameters.Add("@LastLabelDate", SqlDbType.DateTime);
                    lastLabelDateParameter.Direction = ParameterDirection.Output;

                    rdr = oSQLCmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        oLastLabelDate = new LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Model();

                        //oLastLabelDate.LastLabelDate = (DateTime)rdr["lastLabelDateParameter"];

                        oLastLabelDate.LastLabelDate = (DateTime)lastLabelDateParameter.Value;

                    }
                }
                rdr.Close();

                return oLastLabelDate;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conn.Close(); // Ensure connection is closed, even if an exception occurs.
            }
        }
    }
}
