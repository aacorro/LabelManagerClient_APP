using System.Data;
using System.Data.SqlClient;
using LMC_Other_InventoryData.DB_Models;
using Serilog;

namespace LMC_Other_InventoryData
{
    public class LMC_InventoryData_Repo
    {
        // Declaring a private field for IDbConnection
        private readonly IDbConnection _conn;

        public LMC_InventoryData_Repo(IDbConnection conn)
        {
            // Assigning the provided IDbConnection to the private field
            _conn = conn;
        }



        /// <summary>
        /// Get_InventoryData_Exported
        ///     -Gets the inventory data no exported based on @StartDate and @EndDate values.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>List of inventory data no exported</returns>
        public List<LMC_InventoryData_Model> Get_InventoryData_Exported(DateTime startDate, DateTime endDate)
        {
            // Create a list to store the model objects
            List<LMC_InventoryData_Model> lInvDataExp = new List<LMC_InventoryData_Model>();
            // Declare an instance of the model for holding inventory data no exported
            LMC_InventoryData_Model oInvDataExp;
            // Declare a SqlDataReader to read the results from the SQL query execution
            SqlDataReader rdr;

            try
            {
                //Setup the using command with stored procedure to connect to sql server
                using (SqlCommand oSQLCmd = new SqlCommand("usp_Get_All_No_Exported_Dynamic_W_ExportedColumn", (SqlConnection)_conn))
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
                            //Instantiate new inventory data no exported object
                            oInvDataExp = new LMC_InventoryData_Model();

                            //Load all record data into inventory data no exported object
                            oInvDataExp.ScaleName = rdr["ScaleName"].ToString();
                            oInvDataExp.ScaleID = (int)rdr["ScaleID"]; //setting ScaleId property from SqlDataReader...
                            oInvDataExp.PalletNo = rdr["PalletNo"].ToString();
                            oInvDataExp.LotNo = rdr["LotNo"].ToString();
                            oInvDataExp.PalletCount = (int)rdr["PalletCount"];
                            oInvDataExp.EarliestDate = (DateTime)rdr["EarliestDate"];
                            oInvDataExp.LastInventorySync = (DateTime)rdr["LastInventorySync"];
                            oInvDataExp.Exported = rdr["Exported"] as bool?;

                            //Add inventory records object to list of inventory data no exported objects
                            lInvDataExp.Add(oInvDataExp);

                            //Clear the inventory data no exported object
                            oInvDataExp = null;
                        }
                    }
                }
                //Cleanup the sql reader
                rdr.Close();

                //Return the list of inventory records detail objects
                return lInvDataExp;
            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("Get_InventoryData_Exported", "Error", ex.Message, ex.StackTrace);

                return null; // Return null in case of an exception
            }
            finally
            {
                _conn.Close(); // Ensure connection is closed, even if an exception occurs.
            }
        }
    }
}
