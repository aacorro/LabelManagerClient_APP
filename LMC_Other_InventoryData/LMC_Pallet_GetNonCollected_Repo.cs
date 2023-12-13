using LMC_Other_InventoryData.DB_Models;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace LMC_Other_InventoryData
{
    public class LMC_Pallet_GetNonCollected_Repo
    {
        // Declaring a private field for IDbConnection
        private readonly IDbConnection _conn;

        public LMC_Pallet_GetNonCollected_Repo(IDbConnection conn)
        {
            // Assigning the provided IDbConnection to the private field
            _conn = conn;
        }

        /// <summary>
        /// GetNonCollected
        ///     -Gets pallet records not collected
        /// </summary>
        /// <returns>list of pallet records not collected</returns>
        public List<LMC_Pallet_GetNonCollected_Model> GetNonCollected()
        {
            // Create a list to store the model objects
            List<LMC_Pallet_GetNonCollected_Model> lGetNonCollected = new();
            // Declare an instance of the model for holding pallet records not collected
            LMC_Pallet_GetNonCollected_Model oGetNonCollected;
            // Declare a SqlDataReader to read the results from the SQL query execution
            SqlDataReader rdr;

            try
            {
                //Setup the using command with stored procedure to connect to sql server
                using (SqlCommand oSQLCmd = new SqlCommand("usp_Pallet_GetNonCollected", (SqlConnection)_conn))
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
                            //Instantiate new pallet reco object
                            oGetNonCollected = new LMC_Pallet_GetNonCollected_Model();

                            //Load all record data into pallet records not collected object
                            oGetNonCollected.PalletNumber = rdr["PalletNumber"].ToString();
                            oGetNonCollected.ProductNumber = rdr["ProductNumber"].ToString();
                            oGetNonCollected.LotNumber = rdr["LotNumber"].ToString();
                            oGetNonCollected.SerialNumber = rdr["SerialNumber"].ToString();
                            oGetNonCollected.InsertDate = (DateTime)rdr["InsertDate"];
                            oGetNonCollected.Description = rdr["Description"].ToString();
                            oGetNonCollected.Collected = (bool)rdr["Collected"];

                            //Add label counter records object to list of pallet records not collected objects
                            lGetNonCollected.Add(oGetNonCollected);

                            //Clear the pallet records not collected object
                            oGetNonCollected = null;
                        }
                    }
                }
                //Cleanup the sql reader
                rdr.Close();

                //Return the list of pallet records not collected
                return lGetNonCollected;
            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("GetNonCollected", "Error", ex.Message, ex.StackTrace);

                return null; // Return null in case of an exception
            }
            finally
            {
                _conn.Close(); // Ensure connection is closed, even if an exception occurs.
            }
        }
    }
}
