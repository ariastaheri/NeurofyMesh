using NeurofyMesh.Models;

namespace NeurofyMesh.Services
{
    public interface IVendorService
    {
        List<Vendor> GetVendors();
        Vendor? GetVendorById(int vendorId);
        Vendor? GetVendorByName(string name);
        Task<bool> UpdateVendor(int id, Vendor vendor);
        Task<bool> DeleteVendor(int vendorId);

        Task<Vendor> CreateVendor(Vendor vendor);

    }
}
