using System.ComponentModel.DataAnnotations;

namespace DataWorker.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [MaxLength(20)]
        public required string FirstName { get; set; }

        [MaxLength(20)]
        public required string LastName { get; set; }

        public int Age { get; set; }

        [MaxLength(50)]
        public required string Address { get; set; }

    }
}
