using System.Data;
using System.Data.SqlClient;
using LMC_Other_InventoryData.DB_Models;
using Serilog;

namespace LMC_Other_InventoryData
{
    public class LMC_SyncTime_UpdateSyncTimeUploadRecords_Repo
    {
        // Declaring a private field for IDbConnection
        private readonly IDbConnection _conn;

        public LMC_SyncTime_UpdateSyncTimeUploadRecords_Repo(IDbConnection conn)
        {
            // Assigning the provided IDbConnection to the private field
            _conn = conn;
        }


        /// <summary>
        /// UploadRecords
        ///     - Sets all synctime data records that have been uploaded to web service to exported = 1
        /// </summary>
        /// <returns> object synctime data records</returns>
        public LMC_SyncTime_UpdateSyncTimeUploadRecords_Model UploadRecords()
        {
            // Declare an instance of the model for holding update sync time upload records
            LMC_SyncTime_UpdateSyncTimeUploadRecords_Model oUpdateSyncTimeUploadRecords = new();
            // Declare a SqlDataReader to read the results from the SQL query execution
            SqlDataReader rdr = null;

            try
            {
                //Setup the using command with stored procedure to connect to sql server
                using (SqlCommand oSQLCmd = new SqlCommand("usp_UpdateSyncTimeUploadRecords", (SqlConnection)_conn))
                {
                    //Open the sql connection
                    _conn.Open();

                    // Set the command type to stored procedure
                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    //Setup the SQL parameters (hard-coded for testing purposes)
                    SqlParameter lastRecordDateParam = new SqlParameter("@LastRecordDate", SqlDbType.DateTime);
                    lastRecordDateParam.Value = DateTime.Parse("10-10-2023");
                    oSQLCmd.Parameters.Add(lastRecordDateParam);

                    //Output parameter
                    SqlParameter statusParam = new SqlParameter("@Status", SqlDbType.Int);
                    statusParam.Direction = ParameterDirection.Output;
                    oSQLCmd.Parameters.Add(statusParam);

                    //Execute the command to fill the reader
                    rdr = oSQLCmd.ExecuteReader();

                    // Creating an instance of the model for holding update sync time upload records
                    oUpdateSyncTimeUploadRecords = new LMC_SyncTime_UpdateSyncTimeUploadRecords_Model();

                    // Checking if there are records in the reader
                    if (rdr.Read())
                    {
                        // Setting the Status property of the model with the value from the reader
                        oUpdateSyncTimeUploadRecords.Status = (int)rdr["status"];
                    }

                }
                //Cleanup the sql reader
                rdr.Close();

                // Return the model with the update sync time upload records 
                return oUpdateSyncTimeUploadRecords;
            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("Error", ex.Message, ex.StackTrace);

                // Setting a default value for the error status in case of an exception
                oUpdateSyncTimeUploadRecords.Status = -1; // Default value for error Status
            }
            finally
            {
                // Closing the reader in the finally block to ensure it's closed even if an exception occurs
                rdr?.Close();
            }

            // Returning the model with the update sync time upload records (default or error status)
            return oUpdateSyncTimeUploadRecords;
        }
    }
}
