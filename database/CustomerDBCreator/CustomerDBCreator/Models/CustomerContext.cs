﻿using Microsoft.EntityFrameworkCore;
//using Pomelo.EntityFrameworkCore.MySql;

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
            var connectionString = Configuration.GetConnectionString("Default");
            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            optionsBuilder.UseSqlServer(connectionString);
        }

        // protected override OnConfi

        DbSet<Customer> Customers { get; set;}

    }
}
