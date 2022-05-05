namespace BooKeeper.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public String StatusDescription { get; set; }
        public List<Author> AuthorResponse { get; set; } = new();
        public List<Purchase> PurchasesResponse { get; set; } = new();
        public List<Customer> CustomerResponse { get; set; } = new();
        public List<Book> BookResponse { get; set; } = new();



    }
}
