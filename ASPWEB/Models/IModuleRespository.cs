using System.Collections.Generic;
using System.Data.SqlClient;

namespace ASPWEB.Models
{
    public class IModuleRepository
    {
        private string connectionString;

        public IModuleRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Module> GetAllModules()
        {
            List<Module> modules = new List<Module>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string SqlQuery = "SELECT * FROM Module";
                using (SqlCommand scom = new SqlCommand(SqlQuery, con))
                {
                    using (SqlDataReader reader = scom.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Module module = new Module();
                            module.ID = reader.GetInt32(0);
                            module.Code = reader.GetString(1);
                            module.Name = reader.GetString(2);
                            module.NumOfCredits = reader.GetDecimal(3);
                            module.ClassHours = reader.GetInt32(4);
                            module.NumOfWeeks = reader.GetInt32(5);
                            module.StartDate = reader.GetDateTime(6);

                            modules.Add(module);
                        }
                    }
                }
            }

            return modules;
        }

       
    }

}
    

