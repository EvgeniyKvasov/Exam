using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.CORE.Models
{
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;
    }
}
