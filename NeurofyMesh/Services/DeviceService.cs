using Microsoft.EntityFrameworkCore;
using NeurofyMesh.Models;

namespace NeurofyMesh.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly MyDbContext _context;

        public DeviceService(MyDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Device> CreateDevice(Device device)
        {
            if (!string.IsNullOrEmpty(device.DeviceId))
            {
                var existing = GetDeviceById(device.DeviceId);

                if (existing != null)
                {
                    Console.WriteLine($"DeviceId {device.DeviceId} already exists!");
                    return existing;
                }
            }

            var newDevice = _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return newDevice.Entity;
        }

        public async Task<bool> DeleteDevice(string id)
        {
            var device = _context.Devices.Where(v => v.DeviceId == id).FirstOrDefault();

            if (device != null)
            {
                try
                {
                    _context.Devices.Remove(device);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
            Console.WriteLine($"Device ID {id} does not exist in the database");
            return false;
        }

        public Device? GetDeviceById(string id)
        {
            var device = _context.Devices.Where(v => v.DeviceId == id).FirstOrDefault();

            return device;
        }

        public List<Device> GetDevices()
        {
            var devices = _context.Devices.ToList();

            return devices;
        }

        public List<Device>? GetDevicesByVendorId(int vendorId)
        {
            var devices = _context.Devices.Where(v => v.VendorId == vendorId).ToList();

            return devices;
        }

        public Device? GetDeviceByVendorIdAndDeviceName(int vendorId, string name)
        {
            var device = _context.Devices.Where(v => v.VendorId == vendorId && v.DeviceName == name).FirstOrDefault();

            return device;
        }

        public async Task<bool> UpdateDeviceById(string id, Device device)
        {
            var existing = GetDeviceById(id);
            if (existing != null)
            {
                existing.VendorId = device.VendorId;
                existing.DeviceName = device.DeviceName;
                _context.Devices.Update(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            Console.WriteLine($"Device with Id {id} doesn't exist!");
            return false;
        }
    }
}
