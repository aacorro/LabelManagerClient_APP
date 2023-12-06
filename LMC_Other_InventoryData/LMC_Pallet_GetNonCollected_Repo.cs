using LMC_Other_InventoryData.DB_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMC_Other_InventoryData
{
    public class LMC_Pallet_GetNonCollected_Repo
    {
        private readonly IDbConnection _conn;

        public LMC_Pallet_GetNonCollected_Repo(IDbConnection conn)
        {
            _conn = conn;
        }

        public List<LMC_Pallet_GetNonCollected_Model> GetNonCollected()
        {
            List<LMC_Pallet_GetNonCollected_Model> lGetNonCollected = new();
            LMC_Pallet_GetNonCollected_Model oGetNonCollected;
            SqlDataReader rdr;

            try
            {
                using (SqlCommand oSQLCmd = new SqlCommand("usp_Pallet_GetNonCollected", (SqlConnection)_conn))
                {
                    _conn.Open();

                    oSQLCmd.CommandType = CommandType.StoredProcedure;

                    rdr = oSQLCmd.ExecuteReader();

                    if(rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            oGetNonCollected = new LMC_Pallet_GetNonCollected_Model();

                            oGetNonCollected.PalletNumber = rdr["PalletNumber"].ToString();
                            oGetNonCollected.ProductNumber = rdr["ProductNumber"].ToString();
                            oGetNonCollected.LotNumber = rdr["LotNumber"].ToString();
                            oGetNonCollected.SerialNumber = rdr["SerialNumber"].ToString();
                            oGetNonCollected.InsertDate = (DateTime)rdr["InsertDate"];
                            oGetNonCollected.Description = rdr["Description"].ToString();
                            oGetNonCollected.Collected = (bool)rdr["Collected"];

                            lGetNonCollected.Add(oGetNonCollected);
                        }
                    }
                }
                rdr.Close();

                return lGetNonCollected;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
