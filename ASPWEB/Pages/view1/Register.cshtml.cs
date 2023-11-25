using ASPWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ASPWEB.Pages.view1
{
    public class RegisterModel : PageModel
    {
        
         public List<RegisterController> listregisterController= new List<RegisterController>();


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
                                RegisterController registerController = new RegisterController();
                                registerController.Email = reader.GetString(1);
                                registerController.PasswordHash = reader.GetString(2);

                                listregisterController.Add(registerController);
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
  