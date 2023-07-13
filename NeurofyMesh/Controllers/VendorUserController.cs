using Microsoft.AspNetCore.Mvc;
using NeurofyMesh.Models;
using NeurofyMesh.Services;

namespace NeurofyMesh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorUserController : ControllerBase
    {
        private readonly IVendorUserService _vendorUserService;

        public VendorUserController(IVendorUserService vendorUserService)
        {
            _vendorUserService = vendorUserService;
        }
        // Get all vendors
        [HttpGet]
        public ActionResult<IEnumerable<VendorUser>> GetVendorUsers()
        {
            var vendorUsers = _vendorUserService.GetVendorUsers();
            return vendorUsers;
        }

        // Get a vendor by ID
        [HttpGet("{id}")]
        public ActionResult<VendorUser> GetVendorUserById([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest($"incorrect id provided");

            var result = _vendorUserService.GetVendorUserById(id);
            return result != null ? Ok(result) : NotFound($"id not found in the database");
        }

        [HttpGet("/user/{id}")]
        public ActionResult<VendorUser> GetVendorUserByUserId([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest($"incorrect id provided");

            var result = _vendorUserService.GetVendorUserByUserId(id);
            return result != null ? Ok(result) : NotFound($"id not found in the database");
        }

        // Create a new vendor
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<VendorUser>> CreateVendorUser([FromBody] VendorUser vendorUser)
        {
            if (vendorUser == null || vendorUser.Id < 0 || vendorUser.UserId <= 0 || vendorUser.VendorId <= 0)
            {
                return BadRequest();
            }

            var result = await _vendorUserService.CreateVendorUser(vendorUser);
            return result != null ? Ok(result) : BadRequest();
        }

        // Update a vendor
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateVendorUserById([FromRoute] int id, [FromBody] VendorUser vendorUser)
        {
            if (vendorUser == null || vendorUser.Id < 0 || vendorUser.UserId <= 0 || vendorUser.VendorId <= 0)
            {
                return BadRequest();
            }
            var result = await _vendorUserService.UpdateVendorUserById(id, vendorUser);
            return result ? Ok(result) : BadRequest();
        }

        [HttpPut("/user/{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateVendorUserByUserId([FromRoute] int id, [FromBody] VendorUser vendorUser)
        {
            if (vendorUser == null || vendorUser.Id < 0 || vendorUser.UserId <= 0 || vendorUser.VendorId <= 0)
            {
                return BadRequest();
            }
            var result = await _vendorUserService.UpdateVendorUserByUserId(id, vendorUser);
            return result ? Ok(result) : BadRequest();
        }

        // Delete a vendor
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendorUser([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _vendorUserService.DeleteVendorUser(id);
            return result ? Ok(result) : BadRequest(result);
        }
    }
}
