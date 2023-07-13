using Microsoft.EntityFrameworkCore;
using NeurofyMesh.Models;
using System.Numerics;

namespace NeurofyMesh.Services
{
    public class VendorUserService : IVendorUserService
    {
        private readonly MyDbContext _context;

        public VendorUserService(MyDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<VendorUser> CreateVendorUser(VendorUser vendorUser)
        {
            if (vendorUser.Id != 0)
            {
                var existing = GetVendorUserById(vendorUser.Id);

                if (existing != null)
                {
                    Console.WriteLine($"VendorUser ID {vendorUser.Id} already exists!");
                    return existing;
                }
            }

            var newVendorUser = _context.VendorUsers.Add(vendorUser);
            await _context.SaveChangesAsync();
            return newVendorUser.Entity;
        }

        public async Task<bool> DeleteVendorUser(int vendorUserId)
        {
            var vendor = _context.VendorUsers.Where(v => v.Id == vendorUserId).FirstOrDefault();

            if (vendor != null)
            {
                try
                {
                    _context.VendorUsers.Remove(vendor);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
            Console.WriteLine($"VendorUser ID {vendorUserId} does not exist in the database");
            return false;
        }

        public VendorUser? GetVendorUserById(int vendorUserId)
        {
            var vendor = _context.VendorUsers.Where(v => v.Id == vendorUserId).FirstOrDefault();

            return vendor;
        }

        public VendorUser? GetVendorUserByUserId(int userId)
        {
            var vendor = _context.VendorUsers.Where(v => v.UserId == userId).FirstOrDefault();

            return vendor;
        }

        public List<VendorUser> GetVendorUsers()
        {
            var vendor = _context.VendorUsers.ToList();

            return vendor;
        }

        public List<VendorUser>? GetVendorUsersByVendorId(int vendorId)
        {
            var vendor = _context.VendorUsers.Where(v => v.VendorId == vendorId).ToList();

            return vendor;
        }

        public async Task<bool> UpdateVendorUserById(int id, VendorUser vendorUser)
        {
            var existing = GetVendorUserById(id);
            if (existing != null)
            {
                existing.VendorId = vendorUser.VendorId;
                existing.UserId = vendorUser.UserId;
                _context.VendorUsers.Update(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            Console.WriteLine($"VendorUser ID {id} doesn't exist!");
            return false;
        }

        public async Task<bool> UpdateVendorUserByUserId(int userId, VendorUser vendorUser)
        {
            var existing = GetVendorUserByUserId(userId);
            if (existing != null)
            {
                existing.VendorId = vendorUser.VendorId;
                existing.UserId = vendorUser.UserId;
                _context.VendorUsers.Update(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            Console.WriteLine($"VendorUser ID with userId {userId} doesn't exist!");
            return false;
        }
    }
}
