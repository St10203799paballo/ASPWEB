// Pages/view1/moduleModel.cshtml.cs
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ServiceStack.LicenseUtils;

namespace ASPWEB.Models
{
    public class Module
    {
        
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal NumOfCredits { get; set; }

        public int ClassHours { get; set; }

      
        public int NumOfWeeks { get; set; }

        
        public DateTime StartDate { get; set; }

        public decimal SelfStudyHours { get; set; }
        public List<HoursRecorded> HoursRecorded { get; set; }
        [Required(ErrorMessage = "Please select a day")]
        public DayOfWeek PlannedDay { get; set; }


    }
}
