using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooKeeper.Models
{
    public class Purchase
    {
        //[Key]
        public int PurchaseId { get; set; }
        public DateTime Date { get; set; }

        
        public int? BookId { get; set; }


        public int? CustomerId { get; set; }
      
        //public Customer? Customer { get; set; }

    }
}
