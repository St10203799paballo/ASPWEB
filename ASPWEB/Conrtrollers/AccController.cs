using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using ASPWEB.Models;
using ServiceStack;

namespace ASPWEB.Controllers
{
    public class AccController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string txtUsername, string txtPassword)
        {
            // Example: ValidateUser method
            if (ValidateUser(txtUsername, txtPassword))
            {
                // Successful login logic
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password";
                return View();
            }
        }

        // Example ValidateUser method, replace with your actual logic
        private bool ValidateUser(string username, string password)
        {
            // Replace this with your actual user validation logic
            // For example, check against a database
            // This is a simple example using hardcoded values for demonstration purposes
            if (username == "demo" && password == "password")
            {
                return true;
            }
            return false;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterController model)
        {
            if (ModelState.IsValid)
            {
                // Example: Add registration logic
                // You might want to hash the password and save it to the database
                // For demonstration purposes, this example just echoes the provided email
                ViewBag.Message = $"Registration successful for {model.Email}";
                return View("Login");
            }
            else
            {
                // Model is not valid, return to registration view with validation errors
                return View();
            }
        }
    }
}
