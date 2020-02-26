using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeWatcher.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeWatcher.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Favoris> Favoris { get; set; }
        public DbSet<SignVelo> SignVelo { get; set; }

        public DbSet<User> User {get;set;}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SignVelo>().ToTable("SignVelo");

            modelBuilder.Entity<Favoris>().ToTable("Favoris");

            modelBuilder.Entity<User>().ToTable("User");

        }


    }
}
