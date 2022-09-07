using Given.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Given.DataContext.Identity
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            try
            {
                Guid guid = Guid.NewGuid();

                modelBuilder.Entity<Role>().HasData(
                 new Role { RoleId = guid, RoleName = "Super Admin" },
                 new Role { RoleId = Guid.NewGuid(), RoleName = "Administrator" },
                 new Role { RoleId = Guid.NewGuid(), RoleName = "User", });

                Guid userId = Guid.NewGuid();
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("Password@12", out passwordHash, out passwordSalt);

                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Email = "admin@given.com",
                        EmailConfirmed = true,
                        FirstName = "admin",
                        LastName = "admin",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        EmailConfirmedOn = DateTime.Now,
                        CreatedOn = DateTime.Now,
                        InvitedBy = userId,
                        Id = userId,
                        UpdatedBy = userId,
                        InvitedOn = DateTime.Now,
                        Otp = null,
                        PhoneNumber = "1234567890",
                        UpdatedOn = DateTime.Now,
                        Photo = null
                    });

                modelBuilder.Entity<UserRole>().HasData(
                  new UserRole
                  {
                      RoleId = guid,
                      UserId = userId
                  });
            }
            catch
            {

            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");


            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
                                        