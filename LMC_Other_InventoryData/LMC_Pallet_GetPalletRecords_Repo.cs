using LMC_Other_InventoryData.DB_Models;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace LMC_Other_InventoryData
{
    public class LMC_Pallet_GetPalletRecords_Repo
    {
        // Declaring a private field for IDbConnection
        private readonly IDbConnection _conn;

        public LMC_Pallet_GetPalletRecords_Repo(IDbConnection conn)
        {
            // Assigning the provided IDbConnection to the private field
            _conn = conn;
        }


        /// <summary>
        /// GetPalletRecordDetails
        ///     - Gets the pallet record detail from the Pallet table based on @SerialNumber value.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="scaleID"></param>
        /// <returns>list of pallet record detail</returns>
        public List<LMC_Pallet_GetPalletRecords_Model> GetPalletRecordDetails(string serialNumber, string scaleID)
        {
            // Create a list to store the model objects 
            List<LMC_Pallet_GetPalletRecords_Model> lPalletRecordDetails = new();
            // Declare an instance of the model for holding pallet record detail
            LMC_Pallet_GetPalletRecords_Model oPalletRecordDetail;
            // Declare a SqlDataReader to read the results from the SQL query execution
            SqlDataReader rdr;

            try
            {
                //Setup the using command with stored procedure to connect to sql server
                using (SqlCommand oSQLCmd = new SqlCommand("usp_GetPalletRecordDetail", (SqlConnection)_conn))
                {
                    //Open the sql connection
                    _conn.Open();

                    // Set the command type to stored procedure
                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    //Setup the SQL parameters
                    oSQLCmd.Parameters.Add(new SqlParameter("@SerialNumber", SqlDbType.VarChar, 12) { Value = serialNumber });
                    oSQLCmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.VarChar, 2) { Value = scaleID });

                    //Execute the command to fill the reader
                    rdr = oSQLCmd.ExecuteReader();

                    //Check if the reader has rows
                    if (rdr.HasRows)
                    {
                        //Loop through all records in data reader
                        while (rdr.Read())
                        {
                            //Instantiate new pallet record detail object
                            oPalletRecordDetail = new LMC_Pallet_GetPalletRecords_Model();

                            //Load all record data into pallet record detail object
                            oPalletRecordDetail.SerialNumber = rdr["SerialNumber"].ToString() ?? string.Empty; //setting SerialNumber property from SqlDataReader...
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

                            //Add label counter records object to list of pallet record detail objects
                            lPalletRecordDetails.Add(oPalletRecordDetail);

                            //Clear the pallet record detail object
                            oPalletRecordDetail = null;
                        }
                    }
                }
                //Cleanup the sql reader
                rdr.Close();

                //Return the list of pallet record detail objects
                return lPalletRecordDetails;

            }
            catch (Exception ex)
            {
                //Log error message to text file
                Log.Error("GetPalletRecordDetails", "Error", ex.Message, ex.StackTrace);

                return null; // Return null in case of an exception
            }
            finally
            {
                _conn.Close(); // Ensure connection is closed, even if an exception occurs.
            }
        }
    }
}
