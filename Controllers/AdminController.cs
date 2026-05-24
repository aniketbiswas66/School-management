using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NalandaSchool.Models;
using NalandaSchool.Data;

namespace NalandaSchool.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check Admin Login
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("Role") == "admin";
        }

        // Dashboard
        public IActionResult Dashboard()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.StudentCount = _context.Students.Count();
            ViewBag.TeacherCount = _context.Teachers.Count();

            return View();
        }

        // STUDENTS

        // Show Students
        public IActionResult Students()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            var students = _context.Students.ToList();

            return View(students);
        }

        // Add Student Page
        public IActionResult AddStudent()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // Save Student
        [HttpPost]
        public IActionResult AddStudent(Student s)
        {
            s.FeeStatus = "Pending";

            
            _context.Students.Add(s);

            
            _context.SaveChanges();

            return RedirectToAction("Students");
        }

        public IActionResult EditStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(Student s)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == s.Id);

            if (student != null)
            {
                student.Name = s.Name;
                student.Class = s.Class;
                student.Phone = s.Phone;
                student.Email = s.Email;
                student.FeeStatus = s.FeeStatus;

                _context.SaveChanges();
            }

            return RedirectToAction("Students");
        }

        // Delete Student
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return RedirectToAction("Students");
        }

        // TEACHERS

        // Show Teachers
        public IActionResult Teachers()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            var teachers = _context.Teachers.ToList();

            return View(teachers);
        }

        // Add Teacher Page
        public IActionResult AddTeacher()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // Save Teacher
        [HttpPost]
        public IActionResult AddTeacher(Teacher t)
        {
            
            _context.Teachers.Add(t);

            
            _context.SaveChanges();

            return RedirectToAction("Teachers");
        }

        // Delete Teacher
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.Find(id);

            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
            }

            return RedirectToAction("Teachers");
        }

        // ATTENDANCE

        public IActionResult Attendance()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            var attendanceList = _context.Attendance.ToList();

            return View(attendanceList);
        }
    }
}