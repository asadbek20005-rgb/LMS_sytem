using LMS.Common.Constants;
using LMS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User_Course> User_Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<User_Course_Payment> User_Course_Payments { get; set; }
        public DbSet<User_Course_Report> User_Course_Reports { get; set; }
        public DbSet<CardInfo> CardInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = new List<User>();
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Asadbek",
                LastName = "Shermatov",
                PhoneNumber = "+998945631282",
                Role = Constants.Admin
            };


            string password = "spawn";
            var hashedPass = new PasswordHasher<User>().HashPassword(user, password);
            user.PasswordHash = hashedPass;
            var confirmPass = new PasswordHasher<User>().VerifyHashedPassword(user, hashedPass, password).ToString();
            user.ConfirmPasswod = confirmPass;
            users.Add(user);
            modelBuilder.Entity<User>().HasData(users);
        }
    }




}

