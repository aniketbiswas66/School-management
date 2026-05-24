namespace NalandaSchool.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FeeStatus { get; set; }  // Paid / Pending
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Class { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }  // Present / Absent / Late
    }

    public class DiaryEntry
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string Class { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }
        public string Homework { get; set; }
        public string Date { get; set; }
    }
}
