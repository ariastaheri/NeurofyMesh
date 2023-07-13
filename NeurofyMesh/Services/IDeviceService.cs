using NeurofyMesh.Models;

namespace NeurofyMesh.Services
{
    public interface IDeviceService
    {
        List<Device> GetDevices();
        Device? GetDeviceById(string id);
        List<Device>? GetDevicesByVendorId(int vendorId);
        Task<bool> UpdateDeviceById(string id, Device device);
        Task<bool> DeleteDevice(string id);
        Task<Device> CreateDevice(Device device);
        Device? GetDeviceByVendorIdAndDeviceName(int vendorId, string name);
    }
}
