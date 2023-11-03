using Microsoft.EntityFrameworkCore;

namespace CustomerDBCreator.Models
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
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"));
        }

        // protected override OnConfi

        DbSet<Customer> Customers { get; set;}

    }
}
