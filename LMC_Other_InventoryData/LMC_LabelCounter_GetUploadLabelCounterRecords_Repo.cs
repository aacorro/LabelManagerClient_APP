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
    public class LMC_LabelCounter_GetUploadLabelCounterRecords_Repo
    {
        private readonly IDbConnection _conn;

        public LMC_LabelCounter_GetUploadLabelCounterRecords_Repo(IDbConnection conn)
        {
            _conn = conn;
        }

        public List<LMC_LabelCounter_GetUploadLabelCounterRecords_Model> GetUploadLabelCounterRecords_Repos(DateTime? startDate, DateTime? endDate)
        {
            List<LMC_LabelCounter_GetUploadLabelCounterRecords_Model> lLabelCounterRecords = new();
            LMC_LabelCounter_GetUploadLabelCounterRecords_Model oLabelCounterRecord;
            SqlDataReader rdr;

            try
            {
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetUploadLabelCounterRecords_WParms", (SqlConnection)_conn))
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
                            oLabelCounterRecord = new LMC_LabelCounter_GetUploadLabelCounterRecords_Model();

                            oLabelCounterRecord.pkLabelCounterID = rdr["pkLabelCounterID"] as long? ?? default(long);
                            oLabelCounterRecord.ScaleName = rdr["ScaleName"] as string ?? default(string);
                            oLabelCounterRecord.ScaleID = rdr["ScaleID"] as int? ?? default(int);
                            oLabelCounterRecord.VariationNo = rdr["VariationNo"] as string ?? default (string);
                            oLabelCounterRecord.ProductNo = rdr["ProductNo"] as string ?? default(string);
                            oLabelCounterRecord.CountGood = rdr["CountGood"] as int? ?? default(int);
                            oLabelCounterRecord.CountBad = rdr["CountBad"] as int? ?? default(int);
                            oLabelCounterRecord.StartDateTime = rdr["StartDateTime"] as DateTime? ?? default(DateTime);
                            oLabelCounterRecord.EndDateTime = rdr["EndDateTime"] as DateTime? ?? default(DateTime);
                            oLabelCounterRecord.Exported = rdr["Exported"] as bool? ?? default(bool);
                            oLabelCounterRecord.ExportedDate = rdr["ExportedDate"] as DateTime? ?? default(DateTime);
                            oLabelCounterRecord.InsertDate = rdr["InsertDate"] as DateTime? ?? default(DateTime);
                            oLabelCounterRecord.ModifiedDate = rdr["ModifiedDate"] as DateTime? ?? default(DateTime);

                            lLabelCounterRecords.Add(oLabelCounterRecord);
                        }
                    }
                }
                rdr.Close();

                return lLabelCounterRecords;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
