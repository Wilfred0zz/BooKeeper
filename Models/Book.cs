using System.ComponentModel.DataAnnotations;
namespace BooKeeper.Models
{
    public class Book
    {
        public int BookId { get; set; }

        public String? Title { get; set; }

        public int? AuthorId {get; set; }

        public float Price { get; set; }



    }
}
