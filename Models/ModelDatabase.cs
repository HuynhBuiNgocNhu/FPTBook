using FptBook.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace FptBook.Models
{
    public class ModelDatabase : DbContext
    {
        public ModelDatabase()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelDatabase, FptBook.Migrations.Configuration>());
        }


        public DbSet<account> accounts { get; set; }
        public DbSet<author> authors { get; set; }
        public DbSet<book> books { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<orderDetail> orderDetails { get; set; }
        public DbSet<order> orders { get; set; }

    }
}