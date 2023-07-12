using NeurofyMesh.Controllers;
using NeurofyMesh.Models;
using System.Numerics;

namespace NeurofyMesh.Services
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _context;

        public UserService(MyDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<User> CreateUser(User User, bool isHashed = false)
        {
            if (User.UserId != 0)
            {
                var existing = GetUserById(User.UserId);

                if (existing != null)
                {
                    Console.WriteLine($"User ID {User.UserId} already exists!");
                    return existing;
                }
            }

            if (String.IsNullOrEmpty(User.Password))
            {
                User.Password = PasswordController.CreateTempPassword();
            }
            else if(!isHashed)
            {
                User.Password = PasswordController.HashPassword(User.Password);
            }

            User.CreatedDate = DateTime.Now;
            User.ModifiedDate = DateTime.Now;

            var newUser = _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return newUser.Entity;
        }

        public async Task<bool> DeleteUser(int UserId)
        {
            var User = _context.Users.Where(v => v.UserId == UserId).FirstOrDefault();

            if (User != null)
            {
                try
                {
                    _context.Users.Remove(User);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
            Console.WriteLine($"User ID {UserId} does not exist in the database");
            return false;
        }

        public User? GetUserById(int UserId)
        {
            var User = _context.Users.Where(v => v.UserId == UserId).FirstOrDefault();

            return User;
        }

        public List<User> GetUsers()
        {
            var Users = _context.Users.ToList();

            return Users;
        }

        public async Task<bool> UpdateUser(int id, User User)
        {
            var existing = GetUserById(id);
            if (existing != null)
            {
                existing.FirstName = User.FirstName;
                existing.LastName = User.LastName;
                existing.Email = User.Email;
                existing.PhoneNumber = User.PhoneNumber;
                existing.CreatedBy = User.CreatedBy;
                existing.IsAdmin = User.IsAdmin;
                existing.IsVendorUser = User.IsVendorUser;
                existing.ModifiedDate = DateTime.Now;
                _context.Users.Update(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            Console.WriteLine($"User ID {User.UserId} doesn't exist!");
            return false;
        }
    }
}
