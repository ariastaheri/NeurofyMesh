using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NeurofyMesh.Models;
using NeurofyMesh.Services;

namespace NeurofyMesh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        // Get all devices
        [HttpGet]
        public ActionResult<IEnumerable<Device>> GetDevices()
        {
            var devices = _deviceService.GetDevices();
            return devices;
        }

        // Get a device by ID
        [HttpGet("{id}")]
        public ActionResult<Device> GetDevice([FromRoute] string id)
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest($"incorrect id provided");

            var result = _deviceService.GetDeviceById(id);
            return result != null ? Ok(result) : NotFound($"id not found in the database");
        }

        // Get a device by Name and VendorId
        [HttpGet("/vendor/{vendorId}/{name}")]
        public ActionResult<Device> GetDeviceByVendorAndName([FromRoute] int vendorId, [FromRoute] string name)
        {
            if (String.IsNullOrEmpty(name) || vendorId <= 0)
                return BadRequest($"incorrect vendor id or name provided");

            var result = _deviceService.GetDeviceByVendorIdAndDeviceName(vendorId, name);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("/vendor/{vendorId}")]
        public ActionResult<List<Device>> GetDevicesByVendorId([FromRoute] int vendorId)
        {
            if (vendorId <= 0)
                return BadRequest($"incorrect vendor id or name provided");

            var result = _deviceService.GetDevicesByVendorId(vendorId);
            return result != null ? Ok(result) : NotFound();
        }

        // Create a new device
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<Device>> CreateDevice([FromBody] Device device)
        {
            if (device == null || String.IsNullOrEmpty(device.DeviceId))
            {
                return BadRequest();
            }

            var result = await _deviceService.CreateDevice(device);
            return result != null ? Ok(result) : BadRequest();
        }

        // Update a device
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateDevice([FromRoute] string id, [FromBody] Device device)
        {
            if (device == null || String.IsNullOrEmpty(device.DeviceId) || String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var result = await _deviceService.UpdateDeviceById(id, device);
            return result ? Ok(result) : BadRequest();
        }

        // Delete a device
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var result = await _deviceService.DeleteDevice(id);
            return result ? Ok(result) : BadRequest(result);
        }
    }
}
