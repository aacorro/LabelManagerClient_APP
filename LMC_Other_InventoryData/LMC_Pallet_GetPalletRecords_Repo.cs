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
    public class LMC_Pallet_GetPalletRecords_Repo
    {
        private readonly IDbConnection _conn;

        public LMC_Pallet_GetPalletRecords_Repo(IDbConnection conn)
        {
            _conn = conn;
        }
        public List<LMC_Pallet_GetPalletRecords_Model> GetPalletRecordDetails(string serialNumber, string scaleID)
        {
            List<LMC_Pallet_GetPalletRecords_Model> lPalletRecordDetails = new();
            LMC_Pallet_GetPalletRecords_Model oPalletRecordDetail;
            SqlDataReader rdr;

            try
            {
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetPalletRecordDetail", (SqlConnection)_conn))
                {
                    _conn.Open();

                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    oSQLCmd.Parameters.Add(new SqlParameter("@SerialNumber", SqlDbType.VarChar, 12) { Value = serialNumber });
                    oSQLCmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.VarChar, 2) { Value = scaleID });

                    rdr = oSQLCmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            oPalletRecordDetail = new LMC_Pallet_GetPalletRecords_Model();

                            oPalletRecordDetail.SerialNumber = rdr["SerialNumber"].ToString() ?? string.Empty;
                            oPalletRecordDetail.PalletNumber = rdr["PalletNumber"].ToString();
                            oPalletRecordDetail.ProductNumber = rdr["ProductNumber"].ToString();
                            oPalletRecordDetail.LotNumber = rdr["LotNumber"].ToString();
                            oPalletRecordDetail.SartoriLotNumber = rdr["SartoriLotNumber"].ToString();
                            oPalletRecordDetail.StockNumber = rdr["StockNumber"].ToString();
                            oPalletRecordDetail.MfgNumber = rdr["MfgNumber"].ToString();
                            oPalletRecordDetail.ProductDate = (DateTime)rdr["ProductDate"];
                            oPalletRecordDetail.PackDate = (DateTime)rdr["PackDate"];
                            oPalletRecordDetail.MakeDate = (DateTime)rdr["MakeDate"];
                            oPalletRecordDetail.CaseWeight = (decimal)rdr["CaseWeight"];
                            oPalletRecordDetail.TareWeight = (decimal)rdr["TareWeight"];
                            oPalletRecordDetail.AverageWeight = (decimal)rdr["AverageWeight"];
                            oPalletRecordDetail.ShelfLife = (int)rdr["ShelfLife"];
                            oPalletRecordDetail.TotalCaseCount = (int)rdr["TotalCaseCount"];
                            oPalletRecordDetail.LabelSize = rdr["LabelSize"].ToString();
                            oPalletRecordDetail.Header = rdr["Header"].ToString();
                            oPalletRecordDetail.Description = rdr["Description"].ToString();
                            oPalletRecordDetail.CollectionTime = (DateTime)rdr["CollectionTime"];
                            oPalletRecordDetail.Collected = (bool)rdr["Collected"];
                            oPalletRecordDetail.ID = (long)rdr["ID"];
                            oPalletRecordDetail.Exported = rdr["Exported"] as bool? ?? default(bool);
                            oPalletRecordDetail.InsertDate = (DateTime)rdr["InsertDate"];

                            lPalletRecordDetails.Add(oPalletRecordDetail);
                        }
                    }
                }
                rdr.Close();

                return lPalletRecordDetails;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
