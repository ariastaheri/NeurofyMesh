using NeurofyMesh.Models;

namespace NeurofyMesh.Services
{
    public class VendorService : IVendorService
    {
        private readonly MyDbContext _context;

        public VendorService(MyDbContext dbContext) 
        { 
            _context = dbContext;
        }
        public async Task<bool> DeleteVendor(int vendorId)
        {
            var vendor = _context.Vendors.Where(v => v.VendorId == vendorId).FirstOrDefault();

            if(vendor != null) 
            {
                try
                {
                    _context.Vendors.Remove(vendor);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }                          
            }
            Console.WriteLine($"Vendor ID {vendorId} does not exist in the database");
            return false;
        }

        public Vendor? GetVendorById(int vendorId)
        {
            var vendor = _context.Vendors.Where(v => v.VendorId == vendorId).FirstOrDefault();

            return vendor;
        }

        public Vendor? GetVendorByName(string name)
        {
            var vendor = _context.Vendors.Where(v => v.Name == name).FirstOrDefault();

            return vendor;
        }

        public async Task<bool> UpdateVendor(int id, Vendor vendor)
        {
            var existing = GetVendorById(id);
            if (existing != null)
            {
                existing.Name = vendor.Name;
                _context.Vendors.Update(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            Console.WriteLine($"Vendor ID {vendor.VendorId} doesn't exist!");
            return false;
        }

        public List<Vendor> GetVendors()
        {
            var vendors =  _context.Vendors.ToList();

            return vendors;
        }

        public async Task<Vendor> CreateVendor(Vendor vendor)
        {
            if(vendor.VendorId != 0)
            {
                var existing = GetVendorById(vendor.VendorId);

                if (existing != null)
                {
                    Console.WriteLine($"Vendor ID {vendor.VendorId} already exists!");
                    return existing;
                }
            }
            
            var newVendor = _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return newVendor.Entity;
        }
    }
}
