using BoxingTire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.Persistence
{
    class BoxingTireDbContext : DbContext
    {
        public BoxingTireDbContext(DbContextOptions<BoxingTireDbContext> options)
                 : base(options)
        {
        }

        public DbSet<Token> Token { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Role> Role { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxingTireDbContext).Assembly);
        }

    }
}
