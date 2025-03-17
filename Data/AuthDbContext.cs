using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bloggy_MVC.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;

        public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles (User, Admin, SuperAdmin)
            var adminRoleId = "28904e5c-a4d9-4750-8db3-d44ad5e2dac7";
            var superAdminRoleId = "d285f860-2a8d-443e-9321-79bbd558ff13";
            var userRoleId = "af194555-ddc4-4481-95e2-ac5f6e3b296f";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdminUser using configuration settings
            var superAdminId = "8a77fd26-c964-4094-83af-7ed6ac1a9064";
            var superAdminEmail = _configuration["SuperAdmin:Email"];
            var superAdminPassword = _configuration["SuperAdmin:Password"];

            var superAdminUser = new IdentityUser
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                NormalizedEmail = superAdminEmail.ToUpper(),
                NormalizedUserName = superAdminEmail.ToUpper(),
                Id = superAdminId
            };

            // Hash and store the password
            var passwordHasher = new PasswordHasher<IdentityUser>();
            superAdminUser.PasswordHash = passwordHasher.HashPassword(superAdminUser, superAdminPassword);

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Assign All Roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = superAdminId },
                new IdentityUserRole<string> { RoleId = superAdminRoleId, UserId = superAdminId },
                new IdentityUserRole<string> { RoleId = userRoleId, UserId = superAdminId }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}


