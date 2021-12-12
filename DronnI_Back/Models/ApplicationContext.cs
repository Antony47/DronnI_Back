using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DronnI_Back.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DronnI_Back.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Drone> Drones { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Backup> Backups { get; set; }
        public DbSet<Category> Categories {get; set;}
        public object User { get; internal set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            
            // создаем бд с новой схемой
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(user => user.CustomerRent).WithOne(rent => rent.Customer).IsRequired();
            modelBuilder.Entity<User>().HasMany(user => user.OperatorRent).WithOne(rent => rent.Operator).IsRequired();
            modelBuilder.Entity<User>().HasMany(user => user.CustomerRent).WithOne(rent => rent.Customer).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(user => user.OperatorRent).WithOne(rent => rent.Operator).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
