using CodePulse.Api.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Api.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "820fd61c-2151-4ef6-a85c-5f4f7918a3c1";
            var writerRoleId = "0d8f7b59-b6d1-4c6b-8074-51ed0e80f23a";
            var adminUserId = "0702b97b-c768-4b84-a9ff-a1987010ce91";
            //create role
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp=readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId
                }
            };
            //seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //create admin user
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "hri123.phapale@gmail.com",
                Email = "hri123.phapale@gmail.com",
                NormalizedEmail = "hri123.phapale@gmail.com".ToUpper(),
                NormalizedUserName = "hri123.phapale@gmail.com".ToUpper()
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin,"Test@123");

            builder.Entity<IdentityUser>().HasData(admin);

            //give the role to admin user

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId= adminUserId,
                    RoleId=readerRoleId
                },
                new()
                {
                    UserId= adminUserId,
                    RoleId=writerRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
