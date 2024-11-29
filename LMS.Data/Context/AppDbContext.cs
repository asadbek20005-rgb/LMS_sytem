using LMS.Common.Constants;
using LMS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User_Course> User_Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<User_Course_Payment> User_Course_Payments { get; set; }
        public DbSet<User_Course_Report> User_Course_Reports { get; set; }
        public DbSet<CardInfo> CardInfos { get; set; }
        public DbSet<OTP> OTP { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = new List<User>();
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Asadbek",
                LastName = "Shermatov",
                PhoneNumber = "+998945631282",
                Username = "spawn",
                Role = Constants.Admin
            };
            string password = "spawn";
            var hashedPass = new PasswordHasher<User>().HashPassword(user, password);
            user.PasswordHash = hashedPass;
            users.Add(user);
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}