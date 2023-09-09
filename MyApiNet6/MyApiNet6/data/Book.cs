using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNet6.data
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public int Price { get; set; }
        [Range(0, 100)]
        public int Quantity { get; set; }
        public string? Description { get; set; }
    }
}
