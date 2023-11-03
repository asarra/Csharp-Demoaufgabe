namespace CustomerDBCreator.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
        public required string Address { get; set; }
    }
}
