using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NalandaSchool.Models;
using NalandaSchool.Data;

namespace NalandaSchool.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor
        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check teacher login
        private bool IsTeacher()
        {
            return HttpContext.Session.GetString("Role") == "teacher";
        }

        // Dashboard Page
        public IActionResult Dashboard()
        {
            if (!IsTeacher())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // Attendance Page
        public IActionResult Attendance()
        {
            if (!IsTeacher())
            {
                return RedirectToAction("Index", "Home");
            }

            // Get students from database
            var students = _context.Students.ToList();

            return View(students);
        }

        // Save Attendance
        [HttpPost]
        public IActionResult SaveAttendance(string[] status, string date, string cls)
        {
            var students = _context.Students.ToList();

            for (int i = 0; i < students.Count; i++)
            {
                Attendance attendance = new Attendance();

                attendance.StudentId = students[i].Id;
                attendance.StudentName = students[i].Name;
                attendance.Class = cls;
                attendance.Date = date;

                if (status != null && i < status.Length)
                {
                    attendance.Status = status[i];
                }
                else
                {
                    attendance.Status = "Present";
                }

                // Save into database
                _context.Attendance.Add(attendance);
            }

            // Save changes permanently
            _context.SaveChanges();

            ViewBag.Success = "Attendance Saved Successfully";

            return RedirectToAction("Attendance");
        }

        // Diary Page
        public IActionResult Diary()
        {
            if (!IsTeacher())
            {
                return RedirectToAction("Index", "Home");
            }

            var diaryList = _context.DiaryEntries.ToList();

            return View(diaryList);
        }

        // Add Diary
        [HttpPost]
        public IActionResult AddDiary(DiaryEntry entry)
        {
            entry.TeacherName = HttpContext.Session.GetString("User") ?? "Teacher";

            // Save into database
            _context.DiaryEntries.Add(entry);

            // Save permanently
            _context.SaveChanges();

            return RedirectToAction("Diary");
        }
    }
}