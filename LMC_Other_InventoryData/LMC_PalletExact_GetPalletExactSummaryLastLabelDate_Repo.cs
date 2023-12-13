using LMC_Other_InventoryData.DB_Models;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace LMC_Other_InventoryData
{
    public class LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Repo
    {
        // Declaring a private field for IDbConnection
        private readonly IDbConnection _conn;

        public LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Repo(IDbConnection conn)
        {
            // Assigning the provided IDbConnection to the private field
            _conn = conn;
        }


        /// <summary>
        /// GetPalletExactSummaryLastLabelDate
        ///     - Gets the last label date by product
        /// </summary>
        /// <param name="productNumber"></param>
        /// <param name="runStartDate"></param>
        /// <param name="runEndDate"></param>
        /// <returns>the last label date by product object</returns>
        public async Task<LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Model> GetPalletExactSummaryLastLabelDate(string productNumber, DateTime runStartDate, DateTime runEndDate)
        {
            // Declare an instance of the model for holding the last label date
            LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Model oLastLabelDate = new();
            // Declare a SqlDataReader to read the results from the SQL query execution
            SqlDataReader rdr;

            try
            {
                //Setup the using command with stored procedure to connect to sql server
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetPalletExactSummaryLastLabelDate", (SqlConnection)_conn))
                {
                    //Open the sql connection
                    _conn.Open();

                    // Set the command type to stored procedure
                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    //Setup the SQL parameters
                    oSQLCmd.Parameters.AddWithValue("@ProductNumber", productNumber);
                    oSQLCmd.Parameters.AddWithValue("@RunStartDate", runStartDate);
                    oSQLCmd.Parameters.AddWithValue("@RunEndDate", runEndDate);

                    // Output parameter for storing the last label date
                    SqlParameter lastLabelDateParameter = oSQLCmd.Parameters.Add("@LastLabelDate", SqlDbType.DateTime);
                    lastLabelDateParameter.Direction = ParameterDirection.Output;

                    //Execute the command to fill the reader
                    rdr = oSQLCmd.ExecuteReader();

                    // Checking if there are records in the reader
                    if (rdr.Read())
                    {
                        // Creating an instance of the model for holding the last label date
                        oLastLabelDate = new LMC_PalletExact_GetPalletExactSummaryLastLabelDate_Model();

                        // Setting the LastLabelDate property of the model with the value from the output parameter
                        if (lastLabelDateParameter.Value != DBNull.Value)
                        {
                            oLastLabelDate.LastLabelDate = (DateTime)lastLabelDateParameter.Value;
                        }
                        else
                        {
                            // Log a message to help identify why the value is null
                            Log.Information("LastLabelDate is null or DBNull.Value");
                            return null; // Return null if LastLabelDate is not available
                        }
                    }
                    else
                    {
                        // Log the case where no records are found
                        Log.Information("No records found for the specified criteria");
                        return null; // Return null if no records are found
                    }
                }
                //Cleanup the sql reader
                rdr.Close();

                //Return the last label date object
                return oLastLabelDate;
            }
            catch (Exception ex)
            {
                //Log error message to text file using Serilog
                Log.Error("GetPalletExactSummaryLastLabelDate", "Error", ex.Message, ex.StackTrace);

                return null; // Return null in case of an exception
            }
            finally
            {
                _conn.Close(); // Ensure connection is closed, even if an exception occurs.
            }
        }
    }
}
