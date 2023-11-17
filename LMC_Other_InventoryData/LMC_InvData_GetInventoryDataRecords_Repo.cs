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
    public class LMC_InvData_GetInventoryDataRecords_Repo
    {
        private readonly IDbConnection _conn;

        public LMC_InvData_GetInventoryDataRecords_Repo(IDbConnection conn)
        {
            _conn = conn;
        }

        public List<LMC_GetUpload_InventoryRecords_Model> GetUploadInventoryRecords()
        {
            List<LMC_GetUpload_InventoryRecords_Model> lInvDataRecords = new();
            LMC_GetUpload_InventoryRecords_Model oInvDataRecord;
            SqlDataReader rdr;

            try
            {
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetUploadInventoryRecords", (SqlConnection)_conn))
                {
                    _conn.Open();

                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    rdr = oSQLCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            oInvDataRecord = new LMC_GetUpload_InventoryRecords_Model();

                            oInvDataRecord.SerialNo = rdr["SerialNo"].ToString();
                            oInvDataRecord.ProductNo = rdr["ProductNo"] as string ?? default(string);
                            oInvDataRecord.BestBy = rdr["BestBy"] as DateTime? ?? default(DateTime);
                            oInvDataRecord.CaseWeight = (decimal)rdr["CaseWeight"];
                            oInvDataRecord.PackDate = rdr["PackDate"] as DateTime? ?? default(DateTime); ;
                            oInvDataRecord.ScaleName = rdr["ScaleName"] as string ?? default(string);
                            oInvDataRecord.ScaleID = rdr["ScaleID"] as int? ?? default(int);
                            oInvDataRecord.PalletNo = rdr["PalletNo"] as string ?? default(string);
                            oInvDataRecord.LotNo = rdr["LotNo"] as string ?? default(string);
                            oInvDataRecord.ID = rdr["ID"] as long? ?? default(long) ;
                            oInvDataRecord.MfgID = rdr["MfgID"] as string ?? default(string);
                            oInvDataRecord.CollectionTime = rdr["CollectionTime"] as DateTime? ?? default(DateTime);
                            oInvDataRecord.Exported = rdr["Exported"] as bool? ?? default(bool) ;
                            oInvDataRecord.ExportFailed = rdr["ExportFailed"] as bool? ?? default(bool);
                            oInvDataRecord.FailureCount = rdr["FailureCount"] as int? ?? default(int);
                            oInvDataRecord.LastFailure = rdr["LastFailure"] as DateTime? ?? default(DateTime);
                            oInvDataRecord.InsertDate = rdr["InsertDate"] as DateTime? ?? default(DateTime);

                            lInvDataRecords.Add(oInvDataRecord);
                        }
                    }
                }
                rdr.Close();

                return lInvDataRecords;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
 