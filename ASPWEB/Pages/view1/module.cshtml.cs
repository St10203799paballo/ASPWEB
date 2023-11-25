using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using ASPWEB.Models;

namespace ASPWEB.Pages.view1
{
    public class module : PageModel
    {
        private IModuleRepository moduleRepository;
        private readonly ILogger<module> logger;

        public module(IModuleRepository repository, ILogger<module> logger)
        {
            moduleRepository = repository;
            this.logger = logger;
        }
        public module(IModuleRepository repository)
        {
            moduleRepository = repository;
        }

        public List<Module> ListModule { get; set; } = new List<Module>();

        public decimal CalculateRemainingSelfStudyHours(Module module)
        {
            decimal credits = module.NumOfCredits;
            int numberOfWeeks = module.NumOfWeeks;
            int classHoursPerWeek = module.ClassHours;

            decimal HoursRecorded = GetHoursRecordedForCurrentWeek(module);

            decimal remainingSelfStudyHours = (credits * 10 * numberOfWeeks - classHoursPerWeek * 5) - HoursRecorded;

            return remainingSelfStudyHours;
        }

        // Replace with your actual implementation to get recorded hours for the current week
        private decimal GetHoursRecordedForCurrentWeek(Module module)
        {
            // Example: Assuming you have a list of HoursRecorded objects
            List<HoursRecorded> recordedHoursList = GetHoursRecordedFromDatabase(module.ID);

            // Sum the recorded hours for the current week
            decimal recordedHoursForCurrentWeek = 0;
            DateTime currentDate = DateTime.Now;

            foreach (var recordedHour in recordedHoursList)
            {
                if (recordedHour.Date.Year == currentDate.Year &&
                    recordedHour.Date.Month == currentDate.Month &&
                    recordedHour.Date.DayOfWeek == currentDate.DayOfWeek)
                {
                    recordedHoursForCurrentWeek += recordedHour.Hours;
                }
            }

            return recordedHoursForCurrentWeek;
        }

        // Replace this with the provided code for simulating data access
        public List<HoursRecorded> GetHoursRecordedFromDatabase(int moduleId)
        {
            List<HoursRecorded> recordedHoursList = new List<HoursRecorded>();

            // Specify the number of days to simulate
            int numberOfDays = 7;

            for (int i = 0; i < numberOfDays; i++)
            {
                DateTime currentDate = DateTime.Now.AddDays(-i).Date;
                decimal hours = i + 2; // Replace this with your actual logic

                recordedHoursList.Add(new HoursRecorded
                {
                    ModuleID = moduleId,
                    Date = currentDate,
                    Hours = hours
                });
            }

            return recordedHoursList;
        }

        public void OnGet()
        {
            try
            {
                List<Module> modules = moduleRepository.GetAllModules();
                string connectionString = "Data Source=lab000000\\SQLEXPRESS;Initial Catalog=myASP;Integrated Security=True";

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

                                // Assuming you have a method named GetHoursRecordedForModule in your IModuleRespository
                                module.HoursRecorded = moduleRepository.GetHoursRecordedForModule(module.ID);
                                ListModule.Add(module);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Consider using logging here
                Console.WriteLine("Exception: " + ex.ToString());
                logger.LogError(ex, "An exception occurred while processing the request.");
            }
        }

        public IActionResult OnPost()
        {
            // Code to process the form data when submitted
            // For now, let's just return to the same page
            return Page();
        }
    }
}
