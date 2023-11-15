using System.Data;
using System.Data.SqlClient;

namespace LMC_Other_InventoryData
{
    public class LMC_InvData_RecordDetail_Repo
    {
        private readonly IDbConnection _conn;

        public LMC_InvData_RecordDetail_Repo(IDbConnection conn)
        {
            _conn = conn;
        }

        public List<LMC_InvData_RecordDetail_Model> Get_Inventory_Record_Detail(string serialNumber, string scaleID)
        {
            List<LMC_InvData_RecordDetail_Model> lInvdataRecordsDetail = new();
            LMC_InvData_RecordDetail_Model oInvdataRecordDetail;
            SqlDataReader rdr;

            try
            {
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetInventoryRecordDetail", (SqlConnection)_conn))
                {
                    _conn.Open();

                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    oSQLCmd.Parameters.Add(new SqlParameter("@SerialNumber", SqlDbType.VarChar, 12) { Value = serialNumber });
                    oSQLCmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.VarChar, 3) { Value = scaleID });


                    rdr = oSQLCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            oInvdataRecordDetail = new LMC_InvData_RecordDetail_Model();

                            oInvdataRecordDetail.ID = (long)rdr["ID"];
                            oInvdataRecordDetail.SerialNo = rdr["SerialNumber"].ToString() ?? string.Empty;
                            oInvdataRecordDetail.PalletNo = rdr["PalletNumber"].ToString();
                            oInvdataRecordDetail.ProductNo = rdr["ProductNumber"].ToString();
                            oInvdataRecordDetail.LotNo = rdr["LotNumber"].ToString();
                            oInvdataRecordDetail.SartoriLotNumber = rdr["SartoriLotNumber"].ToString();
                            oInvdataRecordDetail.StockNumber = rdr["StockNumber"].ToString();
                            oInvdataRecordDetail.MfgID = rdr["MfgNumber"].ToString();
                            oInvdataRecordDetail.ProductDate = (DateTime)rdr["ProductDate"];
                            oInvdataRecordDetail.PackDate = (DateTime)rdr["PackDate"];
                            oInvdataRecordDetail.MakeDate = (DateTime)rdr["MakeDate"];
                            oInvdataRecordDetail.CaseWeight = (decimal)rdr["CaseWeight"];
                            oInvdataRecordDetail.TareWeight = (decimal)rdr["TareWeight"];
                            oInvdataRecordDetail.AverageWeight = (decimal)rdr["AverageWeight"];
                            oInvdataRecordDetail.ShelfLife = (int)rdr["ShelfLife"];
                            oInvdataRecordDetail.TotalCaseCount = (int)rdr["TotalCaseCount"];
                            oInvdataRecordDetail.LabelSize = rdr["LabelSize"].ToString();
                            oInvdataRecordDetail.Header = rdr["Header"].ToString();
                            oInvdataRecordDetail.Description = rdr["Description"].ToString();
                            oInvdataRecordDetail.CollectionTime = (DateTime)rdr["CollectionTime"];
                            oInvdataRecordDetail.Collected = (bool)rdr["Collected"];
                            oInvdataRecordDetail.Exported = (bool)rdr["Exported"];
                            oInvdataRecordDetail.InsertDate = (DateTime)rdr["InsertDate"];

                            lInvdataRecordsDetail.Add(oInvdataRecordDetail);
                        }
                    }
                }
                rdr.Close();

                return lInvdataRecordsDetail;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
