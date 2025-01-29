using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public Role? Role { get; set; }

        public Customer? Customer { get; set; }

    }
}
