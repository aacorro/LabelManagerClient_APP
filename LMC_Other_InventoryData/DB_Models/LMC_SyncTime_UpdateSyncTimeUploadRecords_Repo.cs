using System.Data;
using System.Data.SqlClient;

namespace LMC_Other_InventoryData.DB_Models
{
    public class LMC_SyncTime_UpdateSyncTimeUploadRecords_Repo
    {
        private readonly IDbConnection _conn;

        public LMC_SyncTime_UpdateSyncTimeUploadRecords_Repo(IDbConnection conn)
        {
            _conn = conn;
        }

        public LMC_SyncTime_UpdateSyncTimeUploadRecords_Model UploadRecords()
        {
            LMC_SyncTime_UpdateSyncTimeUploadRecords_Model oUpdateSyncTimeUploadRecords = new();
            SqlDataReader rdr = null ;

            try
            {
                using (SqlCommand oSQLCmd = new SqlCommand("usp_UpdateSyncTimeUploadRecords", (SqlConnection)_conn))
                {
                    _conn.Open();

                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    // Create a SqlParameter for @LastRecordDate
                    SqlParameter lastRecordDateParam = new SqlParameter("@LastRecordDate", SqlDbType.DateTime);
                    lastRecordDateParam.Value = DateTime.Parse("10-10-2023");
                    oSQLCmd.Parameters.Add(lastRecordDateParam);

                    // Create a SqlParameter for @Status
                    SqlParameter statusParam = new SqlParameter("@Status", SqlDbType.Int);
                    statusParam.Direction = ParameterDirection.Output;
                    oSQLCmd.Parameters.Add(statusParam);

                    rdr = oSQLCmd.ExecuteReader();

                    oUpdateSyncTimeUploadRecords = new LMC_SyncTime_UpdateSyncTimeUploadRecords_Model();

                    if (rdr.Read())
                    {
                        oUpdateSyncTimeUploadRecords.Status = (int)rdr["status"];
                    }

                }
                rdr.Close();

                return oUpdateSyncTimeUploadRecords;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Log the exception or handle it as needed
                // Set a default or meaningful value for Status
                oUpdateSyncTimeUploadRecords.Status = -1; // Example: Set a default value for Status
            }
            finally
            {
                rdr?.Close();
            }

            return oUpdateSyncTimeUploadRecords;
        }
    }
}
