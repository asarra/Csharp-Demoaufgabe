using Microsoft.EntityFrameworkCore;

namespace DataWorker.Models
{
    public class CustomerContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public CustomerContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {

            Configuration = configuration;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Customer> Customers { get; set;}

    }
}
