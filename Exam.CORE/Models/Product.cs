using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.CORE.Models
{
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("price")]
        public decimal Price { get; set; }
    }
}
