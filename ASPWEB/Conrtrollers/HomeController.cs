using Microsoft.AspNetCore.Mvc;

using System;
 



namespace ASPWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IModuleService _moduleService;

        public HomeController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        public IActionResult Index()
        {
            try
            {
                // Get modules for the current user
                var modules = _moduleService.GetModulesForCurrentUser();

                // Get the current day of the week
                var currentDay = DateTime.Today.DayOfWeek;

                // Filter modules for the current day
                var modulesForToday = modules.Where(m => m.PlannedDay == currentDay).ToList();

                ViewBag.ModulesForToday = modulesForToday;

               
                return RedirectToAction("Success");
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., logging, displaying an error page)
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }
    }
}
