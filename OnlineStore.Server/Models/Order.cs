using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        [Required]
        public DateOnly OrderDate { get; set; }

        public DateOnly? ShipmentDate { get; set; }

        public int OrderNumber { get; set; }

        [Required]
        public Guid StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        public Customer? Customer { get; set; }
    }
}
