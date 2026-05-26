using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NalandaSchool.Models
{
    public class LoginUser
    {
        public int Id { get; set; }
        [Column("UserName")]
        public string Username { get; set; }
        
        public string Password { get; set; }

        public string Role { get; set; }
    }
}