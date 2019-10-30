using BoxingTire.App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.App.Models
{
   public class BoxingTireDbContext :DbContext
    {
        public BoxingTireDbContext()
        {
            Database.EnsureCreatedAsync();
        }

    public BoxingTireDbContext(DbContextOptions<BoxingTireDbContext> options)
                 : base(options)
    {
           
    }

    
    public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<UserScore> UserScore { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={App.DbPath}", x => x.SuppressForeignKeyEnforcement());

            // optionsBuilder.UseMySql("server=192.168.0.14;database=cart;user=root;password=hawk1234;SSL Mode=none");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            //  modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxingTireDbContext).Assembly);
            modelBuilder.Entity<UserAccount>().HasKey(x => x.Id);
            modelBuilder.Entity<UserScore>().HasKey(x => x.Id);
        }

}
}
