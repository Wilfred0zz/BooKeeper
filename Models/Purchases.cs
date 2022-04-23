using System.ComponentModel.DataAnnotations;

namespace BooKeeper.Models
{
    public class Purchases
    {
        [Key]
        public int PurchaseId { get; set; }
        public DateTime Date { get; set; }

        public Books? Book { get; set; }

        public Customer? Customer { get; set; }


    }
}
