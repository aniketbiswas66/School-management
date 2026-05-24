using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NalandaSchool.Models;
using NalandaSchool.Data;

namespace NalandaSchool.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check Student Login
        private bool IsStudent()
        {
            return HttpContext.Session.GetString("Role") == "student";
        }

        // Dashboard Page
        public IActionResult Dashboard()
        {
            if (!IsStudent())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // Admission Form Page
        public IActionResult Admission()
        {
            if (!IsStudent())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // Save Admission Data
        [HttpPost]
        public IActionResult Admission(Student s)
        {
            s.FeeStatus = "Pending";

            // Save student into database
            _context.Students.Add(s);

            // Save permanently
            _context.SaveChanges();

            ViewBag.Success = "Application submitted successfully!";

            return View();
        }

        // Show Attendance
        public IActionResult Attendance()
        {
            if (!IsStudent())
            {
                return RedirectToAction("Index", "Home");
            }

            // Get attendance data from database
            var attendanceList = _context.Attendance.ToList();

            return View(attendanceList);
        }

        // Schedule Page
        public IActionResult Schedule()
        {
            if (!IsStudent())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}