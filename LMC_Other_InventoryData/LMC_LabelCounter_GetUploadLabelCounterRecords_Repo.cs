using LMC_Other_InventoryData.DB_Models;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace LMC_Other_InventoryData
{
    public class LMC_LabelCounter_GetUploadLabelCounterRecords_Repo
    {
        // Declaring a private field for IDbConnection
        private readonly IDbConnection _conn;

        public LMC_LabelCounter_GetUploadLabelCounterRecords_Repo(IDbConnection conn)
        {
            // Assigning the provided IDbConnection to the private field
            _conn = conn;
        }


        /// <summary>
        /// GetUploadLabelCounterRecords_Repos
        ///     - Gets the label counter records from LabelCounter based on @StartDate and @EndDate values.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>list of label counter records</returns>
        public List<LMC_LabelCounter_GetUploadLabelCounterRecords_Model> GetUploadLabelCounterRecords_Repos(DateTime? startDate, DateTime? endDate)
        {
            // Create a list to store the model objects 
            List<LMC_LabelCounter_GetUploadLabelCounterRecords_Model> lLabelCounterRecords = new();
            // Declare an instance of the model for holding label counter records
            LMC_LabelCounter_GetUploadLabelCounterRecords_Model oLabelCounterRecord;
            // Declare a SqlDataReader to read the results from the SQL query execution
            SqlDataReader rdr;

            try
            {
                //Setup the using command with stored procedure to connect to sql server
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetUploadLabelCounterRecords_WParms", (SqlConnection)_conn))
                {
                    //Open the sql connection
                    _conn.Open();

                    // Set the command type to stored procedure
                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    //Setup the SQL parameters
                    oSQLCmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = startDate });
                    oSQLCmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = endDate });

                    //Execute the command to fill the reader
                    rdr = oSQLCmd.ExecuteReader();

                    //Check if the reader has rows
                    if (rdr.HasRows)
                    {
                        //Loop through all records in data reader
                        while (rdr.Read())
                        {
                            //Instantiate new inventory records detail object
                            oLabelCounterRecord = new LMC_LabelCounter_GetUploadLabelCounterRecords_Model();

                            //Load all record data into inventory records detail object
                            oLabelCounterRecord.pkLabelCounterID = rdr["pkLabelCounterID"] as long? ?? default(long);
                            oLabelCounterRecord.ScaleName = rdr["ScaleName"] as string ?? default(string); //setting ScaleName property from SqlDataReader...
                            oLabelCounterRecord.ScaleID = rdr["ScaleID"] as int? ?? default(int);
                            oLabelCounterRecord.VariationNo = rdr["VariationNo"] as string ?? default(string);
                            oLabelCounterRecord.ProductNo = rdr["ProductNo"] as string ?? default(string);
                            oLabelCounterRecord.CountGood = rdr["CountGood"] as int? ?? default(int);
                            oLabelCounterRecord.CountBad = rdr["CountBad"] as int? ?? default(int);
                            oLabelCounterRecord.StartDateTime = rdr["StartDateTime"] as DateTime? ?? default(DateTime);
                            oLabelCounterRecord.EndDateTime = rdr["EndDateTime"] as DateTime? ?? default(DateTime);
                            oLabelCounterRecord.Exported = rdr["Exported"] as bool? ?? default(bool);
                            oLabelCounterRecord.ExportedDate = rdr["ExportedDate"] as DateTime? ?? default(DateTime);
                            oLabelCounterRecord.InsertDate = rdr["InsertDate"] as DateTime? ?? default(DateTime);
                            oLabelCounterRecord.ModifiedDate = rdr["ModifiedDate"] as DateTime? ?? default(DateTime);

                            //Add label counter records object to list of label counter records objects
                            lLabelCounterRecords.Add(oLabelCounterRecord);

                            // Clear the label counter records object
                            oLabelCounterRecord = null;
                        }
                    }
                }
                //Cleanup the sql reader
                rdr.Close();

                //Return the list of inventory records detail objects
                return lLabelCounterRecords;
            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("GetUploadLabelCounterRecords_Repos", "Error", ex.Message, ex.StackTrace);

                return null; // Return null in case of an exception
            }
            finally
            {
                _conn.Close(); // Ensure connection is closed, even if an exception occurs.
            }
        }
    }
}
