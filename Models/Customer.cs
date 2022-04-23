using System;
namespace BooKeeper.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public String PhoneNumber { get; set; }

        public String Email { get; set; }
    }
}
