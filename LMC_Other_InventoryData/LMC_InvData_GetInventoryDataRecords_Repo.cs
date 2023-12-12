using LMC_Other_InventoryData.DB_Models;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace LMC_Other_InventoryData
{
    public class LMC_InvData_GetInventoryDataRecords_Repo
    {
        // Declaring a private field for IDbConnection
        private readonly IDbConnection _conn;

        public LMC_InvData_GetInventoryDataRecords_Repo(IDbConnection conn)
        {
            // Assigning the provided IDbConnection to the private field
            _conn = conn;
        }


        /// <summary>
        /// GetUploadInventoryRecords
        ///     -Gets the inventory records for upload to the webservice.
        /// </summary>
        /// <returns>List of Inventory Records Updated</returns>
        public List<LMC_GetUpload_InventoryRecords_Model> GetUploadInventoryRecords()
        {
            // Create a list to store the model objects 
            List<LMC_GetUpload_InventoryRecords_Model> lInvDataRecords = new();
            // Declare an instance of the model for holding inventory records for upload to the webservice
            LMC_GetUpload_InventoryRecords_Model oInvDataRecord;
            // Declare a SqlDataReader to read the results from the SQL query execution
            SqlDataReader rdr;

            try
            {
                //Setup the using command with stored procedure to connect to sql server
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetUploadInventoryRecords", (SqlConnection)_conn))
                {
                    //Open the sql connection
                    _conn.Open();

                    // Set the command type to stored procedure
                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    //Execute the command to fill the reader
                    rdr = oSQLCmd.ExecuteReader();

                    //Check if the reader has rows
                    if (rdr.HasRows)
                    {
                        //Loop through all records in data reader
                        while (rdr.Read())
                        {
                            //Instantiate new inventory records object
                            oInvDataRecord = new LMC_GetUpload_InventoryRecords_Model();

                            //Load all record data into inventory records object
                            oInvDataRecord.SerialNo = rdr["SerialNo"].ToString(); //setting SerialNo property from SqlDataReader...
                            oInvDataRecord.ProductNo = rdr["ProductNo"] as string ?? default(string);
                            oInvDataRecord.BestBy = rdr["BestBy"] as DateTime? ?? default(DateTime);
                            oInvDataRecord.CaseWeight = (decimal)rdr["CaseWeight"];
                            oInvDataRecord.PackDate = rdr["PackDate"] as DateTime? ?? default(DateTime); ;
                            oInvDataRecord.ScaleName = rdr["ScaleName"] as string ?? default(string);
                            oInvDataRecord.ScaleID = rdr["ScaleID"] as int? ?? default(int);
                            oInvDataRecord.PalletNo = rdr["PalletNo"] as string ?? default(string);
                            oInvDataRecord.LotNo = rdr["LotNo"] as string ?? default(string);
                            oInvDataRecord.ID = rdr["ID"] as long? ?? default(long);
                            oInvDataRecord.MfgID = rdr["MfgID"] as string ?? default(string);
                            oInvDataRecord.CollectionTime = rdr["CollectionTime"] as DateTime? ?? default(DateTime);
                            oInvDataRecord.Exported = rdr["Exported"] as bool? ?? default(bool);
                            oInvDataRecord.ExportFailed = rdr["ExportFailed"] as bool? ?? default(bool);
                            oInvDataRecord.FailureCount = rdr["FailureCount"] as int? ?? default(int);
                            oInvDataRecord.LastFailure = rdr["LastFailure"] as DateTime? ?? default(DateTime);
                            oInvDataRecord.InsertDate = rdr["InsertDate"] as DateTime? ?? default(DateTime);

                            //Add inventory records object to list of inventory records objects
                            lInvDataRecords.Add(oInvDataRecord);

                            // Clear the inventory records object
                            oInvDataRecord = null;
                        }
                    }
                }
                //Cleanup the sql reader
                rdr.Close();

                //Return the list of inventory records objects
                return lInvDataRecords;
            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("GetUploadInventoryRecords", "Error", ex.Message, ex.StackTrace);

                // Return null in case of an exception
                return null;
            }
            finally
            {
                // Ensure connection is closed, even if an exception occurs.
                _conn.Close();
            }
        }
    }
}
