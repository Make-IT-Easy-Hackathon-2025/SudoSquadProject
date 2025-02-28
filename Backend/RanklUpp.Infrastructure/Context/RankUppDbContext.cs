using Microsoft.EntityFrameworkCore;
using RankUpp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RanklUpp.Infrastructure.Context
{
    public class RankUppDbContext : DbContext
    {
        public RankUppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().HasIndex(a => a.Email).IsUnique();

            modelBuilder.Entity<User>().HasIndex(a => a.UserName).IsUnique();

        }
    }
}
