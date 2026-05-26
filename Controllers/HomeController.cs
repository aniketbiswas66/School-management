using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NalandaSchool.Models;
using NalandaSchool.Data;

namespace NalandaSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Login Page
        public IActionResult Index()
        {
            return View();
        }

        // Login Check
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.LoginUsers
                .FirstOrDefault(x =>
                    x.Username == username &&
                    x.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("User", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }

                if (user.Role == "teacher")
                {
                    return RedirectToAction("Dashboard", "Teacher");
                }

                if (user.Role == "student")
                {
                    return RedirectToAction("Dashboard", "Student");
                }
            }

            ViewBag.Error = "Invalid Username or Password";

            return View("Index");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                ?? HttpContext.TraceIdentifier
            });
        }
    }
}