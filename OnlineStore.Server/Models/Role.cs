using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
