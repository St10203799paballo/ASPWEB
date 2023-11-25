using ASPWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ASPWEB.Pages.view1
{
    public class IndexModel : PageModel
    {
        public List<User> listuser = new List<User>();
        

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=lab000000\\SQLEXPRESS;Initial Catalog=myASP;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string SqlQuery = "SELECT * FROM LOGIN";
                    using (SqlCommand scom = new SqlCommand(SqlQuery, con))
                    {
                        using (SqlDataReader reader = scom.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = new User();
                                user.UserID = reader.GetInt32(0);
                                user.Username = reader.GetString(1);
                                user.PasswordHash = reader.GetString(2);

                                listuser.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Execption: " + ex.ToString());
            }
        }
    }
}
