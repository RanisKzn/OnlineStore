using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public double Price { get; set; }

        [Required]
        public Guid CategoryId{ get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
