using Microsoft.EntityFrameworkCore;
using NalandaSchool.Models;

namespace NalandaSchool.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Attendance> Attendance { get; set; }

        public DbSet<DiaryEntry> DiaryEntries { get; set; }
    }
}