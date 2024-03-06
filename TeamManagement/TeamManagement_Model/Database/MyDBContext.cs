using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TeamManagement;
using TeamManagement.Models;

namespace TeamManagement_Models.Database

{
    public class MyDBContext : DbContext
    {
        private readonly IConfiguration configutation;
        public MyDBContext(DbContextOptions options,IConfiguration _config) : base(options)
        {
            configutation = _config;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Login> Login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Adding Coach details through Data seeding in Users Table
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    FirstName = "Aryan",
                    LastName = "Prajapati",
                    ContactNumber = "8989898989",                    
                    Password = PasswordHasher.HashPassword("team1234"),
                    Email = configutation["EmailSettings:SmtpUsername"],
                    DOB = new DateTime(2001, 11, 02),
                    RoleId = 3

                });

            // Adding Coach credentials through Data seeding in Login table
            modelBuilder.Entity<Login>().HasData(
                new Login()
                {
                    Email = configutation["EmailSettings:SmtpUsername"],
                    Password = PasswordHasher.HashPassword("team1234")
                });
        }
    }
}
