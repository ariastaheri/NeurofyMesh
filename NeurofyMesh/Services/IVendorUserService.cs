using NeurofyMesh.Models;

namespace NeurofyMesh.Services
{
    public interface IVendorUserService
    {
        List<VendorUser> GetVendorUsers();
        VendorUser? GetVendorUserById(int vendorUserId);
        VendorUser? GetVendorUserByUserId(int userId);
        List<VendorUser>? GetVendorUsersByVendorId(int vendorId);
        Task<bool> UpdateVendorUserById(int id, VendorUser vendorUser);
        Task<bool> UpdateVendorUserByUserId(int userId, VendorUser vendorUser);
        Task<bool> DeleteVendorUser(int vendorUserId);
        Task<VendorUser> CreateVendorUser(VendorUser vendorUser);
    }
}
