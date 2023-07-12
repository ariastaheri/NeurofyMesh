using Microsoft.AspNetCore.Mvc;
using NeurofyMesh.Models;
using NeurofyMesh.Services;
using System.Numerics;

namespace NeurofyMesh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        // Get all vendors
        [HttpGet]
        public ActionResult<IEnumerable<Vendor>> GetVendors()
        {
            var vendors = _vendorService.GetVendors();
            return vendors;
        }

        // Get a vendor by ID
        [HttpGet("{id}")]
        public ActionResult<Vendor> GetVendor([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest($"incorrect id provided");

            var result = _vendorService.GetVendorById(id);
            return result != null ? Ok(result) : NotFound($"id not found in the database");
        }

        // Create a new vendor
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<Vendor>> CreateVendor([FromBody] Vendor vendor)
        {
            if(vendor == null || vendor.VendorId < 0)
            {
                return BadRequest();
            }

            var result = await _vendorService.CreateVendor(vendor);
            return result != null ? Ok(result) : BadRequest();
        }

        // Update a vendor
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateVendor([FromRoute]int id, [FromBody]Vendor vendor)
        {
            if (vendor == null || vendor.VendorId <= 0 || id <= 0)
            {
                return BadRequest();
            }
            var result = await _vendorService.UpdateVendor(id, vendor);
            return result ? Ok(result) : BadRequest();
        }

        // Delete a vendor
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _vendorService.DeleteVendor(id);
            return result ? Ok(result) : BadRequest(result);
        }
    }
}
