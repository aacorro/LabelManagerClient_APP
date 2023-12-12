using System.Data;
using System.Data.SqlClient;
using LMC_Other_InventoryData.DB_Models;
using Serilog;

namespace LMC_Other_InventoryData
{
    public class LMC_InvData_RecordDetail_Repo
    {
        // Declaring a private field for IDbConnection
        private readonly IDbConnection _conn;

        public LMC_InvData_RecordDetail_Repo(IDbConnection conn)
        {
            // Assigning the provided IDbConnection to the private field
            _conn = conn;
        }

        /// <summary>
        /// Get_Inventory_Record_Detail
        ///     -Gets the inventory record detail from the InventoryData table based on @SerialNumber and @ScaleID values.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="scaleID"></param>
        /// <returns>list of inventory records detail</returns>
        public List<LMC_InvData_RecordDetail_Model> Get_Inventory_Record_Detail(string serialNumber, string scaleID)
        {
            // Create a list to store the model objects 
            List<LMC_InvData_RecordDetail_Model> lInvdataRecordsDetail = new();
            // Declare an instance of the model for holding inventory record detail
            LMC_InvData_RecordDetail_Model oInvdataRecordDetail;
            // Declare a SqlDataReader to read the results from the SQL query execution
            SqlDataReader rdr;

            try
            {
                //Setup the using command with stored procedure to connect to sql server
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetInventoryRecordDetail", (SqlConnection)_conn))
                {
                    //Open the sql connection
                    _conn.Open();

                    // Set the command type to stored procedure
                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    //Setup the SQL parameters
                    oSQLCmd.Parameters.Add(new SqlParameter("@SerialNumber", SqlDbType.VarChar, 12) { Value = serialNumber });
                    oSQLCmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.VarChar, 3) { Value = scaleID });

                    //Execute the command to fill the reader
                    rdr = oSQLCmd.ExecuteReader();

                    //Check if the reader has rows
                    if (rdr.HasRows)
                    {
                        //Loop through all records in data reader
                        while (rdr.Read())
                        {
                            //Instantiate new inventory records detail object
                            oInvdataRecordDetail = new LMC_InvData_RecordDetail_Model();

                            //Load all record data into inventory records detail object
                            oInvdataRecordDetail.ID = (long)rdr["ID"];
                            oInvdataRecordDetail.SerialNo = rdr["SerialNumber"].ToString() ?? string.Empty; //setting SerialNo property from SqlDataReader...
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

                            //Add inventory records object to list of inventory records detail objects
                            lInvdataRecordsDetail.Add(oInvdataRecordDetail);

                            // Clear the inventory records detail object
                            oInvdataRecordDetail = null;
                        }
                    }
                }
                //Cleanup the sql reader
                rdr.Close();

                //Return the list of inventory records detail objects
                return lInvdataRecordsDetail;
            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("Get_Inventory_Record_Detail", "Error", ex.Message, ex.StackTrace);

                return null; // Return null in case of an exception
            }
            finally
            {
                _conn.Close(); // Ensure connection is closed, even if an exception occurs.
            }
        }
    }
}
