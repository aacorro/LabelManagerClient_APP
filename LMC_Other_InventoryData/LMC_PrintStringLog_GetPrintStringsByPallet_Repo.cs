using LMC_Other_InventoryData.DB_Models;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace LMC_Other_InventoryData
{
    public class LMC_PrintStringLog_GetPrintStringsByPallet_Repo
    {
        // Declaring a private field for IDbConnection
        private readonly IDbConnection _conn;

        public LMC_PrintStringLog_GetPrintStringsByPallet_Repo(IDbConnection conn)
        {
            // Assigning the provided IDbConnection to the private field
            _conn = conn;
        }


        /// <summary>
        /// GetPrintStringsByPallet
        ///     - Gets the print string record values in the PrintStringLog table by pallet number
        /// </summary>
        /// <param name="palletNumber"></param>
        /// <returns>list of print string record values</returns>
        public List<LMC_PrintStringLog_GetPrintStringsByPallet_Model> GetPrintStringsByPallet(string palletNumber)
        {
            // Create a list to store the model objects 
            List<LMC_PrintStringLog_GetPrintStringsByPallet_Model> lPrintStringsByPallet = new();
            // Declare an instance of the model for holding print string record values
            LMC_PrintStringLog_GetPrintStringsByPallet_Model oPrintStringsByPallet;
            // Declare a SqlDataReader to read the results from the SQL query execution
            SqlDataReader rdr;

            try
            {
                //Setup the using command with stored procedure to connect to sql server
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetPrintStringsByPallet", (SqlConnection)_conn))
                {
                    //Open the sql connection
                    _conn.Open();

                    // Set the command type to stored procedure
                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    //Setup the SQL parameter
                    oSQLCmd.Parameters.Add(new SqlParameter("@PalletNumber", SqlDbType.VarChar, 18) { Value = palletNumber });

                    //Execute the command to fill the reader
                    rdr = oSQLCmd.ExecuteReader();

                    //Check if the reader has rows
                    if (rdr.HasRows)
                    {
                        //Loop through all records in data reader
                        while (rdr.Read())
                        {
                            //Instantiate new print string record object
                            oPrintStringsByPallet = new LMC_PrintStringLog_GetPrintStringsByPallet_Model();

                            //Load all record data into print string record object
                            oPrintStringsByPallet.pkPrintStringLogID = (int)rdr["pkPrintStringLogID"];
                            oPrintStringsByPallet.SerialNumber = rdr["SerialNumber"].ToString();
                            oPrintStringsByPallet.ProductNumber = rdr["ProductNumber"].ToString();
                            oPrintStringsByPallet.PalletNumber = rdr["PalletNumber"].ToString();
                            oPrintStringsByPallet.PrintString = rdr["PrintString"].ToString();
                            oPrintStringsByPallet.ScaleName = rdr["ScaleName"].ToString();
                            oPrintStringsByPallet.ScaleID = rdr["ScaleID"].ToString();
                            oPrintStringsByPallet.InsertDate = (DateTime)rdr["InsertDate"];

                            //Add label print string record object to list of print string record objects
                            lPrintStringsByPallet.Add(oPrintStringsByPallet);

                            //Clear the print string record object
                            oPrintStringsByPallet = null;
                        }
                    }
                }
                //Cleanup the sql reader
                rdr.Close();

                //Return the list of print string records values
                return lPrintStringsByPallet;
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
