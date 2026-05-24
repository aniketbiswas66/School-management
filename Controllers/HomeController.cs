using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NalandaInstitute.Models;

namespace NalandaInstitute.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string role)
        {
            // Demo login only

            HttpContext.Session.SetString("User", username);
            HttpContext.Session.SetString("Role", role);

            if (role == "admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            if (role == "teacher")
            {
                return RedirectToAction("Dashboard", "Teacher");
            }

            if (role == "student")
            {
                return RedirectToAction("Dashboard", "Student");
            }

            ViewBag.Error = "Invalid Login";
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}