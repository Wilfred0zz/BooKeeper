using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using BooKeeper.Models;
namespace BooKeeper.Models
{
    public class APIDBContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public APIDBContext(DbContextOptions<APIDBContext> options, IConfiguration configuration) 
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("BooKeeper");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Customer>? Customers { get; set; } = null;
        public DbSet<Author>? Authors { get; set; } = null;
        public DbSet<Book>? Books { get; set; } = null;
        public DbSet<Purchase>? Purchases { get; set; } = null!;
    }
}
