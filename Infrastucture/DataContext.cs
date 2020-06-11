using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastucture
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // for seed data in thr tables
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PortfoioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");


            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    Id = Guid.NewGuid(),
                    Avatar = "avatar.jpg",
                    FullName = "Abanob Youssef",
                    Profil = ".NET developer"
                }
                );

        }

        public DbSet<Owner> Owner { get; set; }
        public DbSet<PortfoioItem> PortfoioItems { get; set; }
    }
}
