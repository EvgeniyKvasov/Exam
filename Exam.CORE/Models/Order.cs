using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.CORE.Models
{
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; } = 1;

        [Column("orderdate")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Только для UI - не сохраняется в БД
        [NotMapped]
        public bool IsConfirmed { get; set; } = false;

        [NotMapped]
        public bool IsSelected { get; set; } = false;

        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}
