using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
