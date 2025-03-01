using Microsoft.EntityFrameworkCore;
using RankUpp.Application.Models;
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

        public DbSet<UserMemory> Memories { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<QuizQuestion> QuizQuestions { get; set; }

        public DbSet<QuizOption> QuizOptions { get; set; }

        public DbSet<QuizAttempt> QuizAttempts { get; set; }

        public DbSet<RoadMap> RoadMaps { get; set; }

        public DbSet<RoadMapItem> RoadMapItems { get; set; }

        public DbSet<UserRoadMapItems> UserRoadMapItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().HasIndex(a => a.Email).IsUnique();

            modelBuilder.Entity<User>().HasIndex(a => a.UserName).IsUnique();

        }
    }
}
