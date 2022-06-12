using Exam.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<User>
    {
        private const string userId = "11e87c16-bc89-4393-a2b0-eb4e2debbd08";
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();

            var admin = new User
            {
                Id = userId,
                FirstName = "Admin",
                LastName = "Adminov",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                CreatedOn = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            admin.PasswordHash = hasher.HashPassword(admin, "123456");
            builder.HasData(admin);
        }
    }
}
