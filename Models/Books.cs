using System.ComponentModel.DataAnnotations;
namespace BooKeeper.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }

        public String? Title { get; set; }

        public Author? Author {get; set; }

        public float Price { get; set; }



    }
}
