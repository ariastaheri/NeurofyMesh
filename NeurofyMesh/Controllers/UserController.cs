using Microsoft.AspNetCore.Mvc;
using NeurofyMesh.Models;
using NeurofyMesh.Services;

namespace NeurofyMesh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // Get all Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var Users = _userService.GetUsers();
            return Users;
        }

        // Get a User by ID
        [HttpGet("{id}")]
        public ActionResult<User> GetUser([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest($"incorrect id provided");

            var result = _userService.GetUserById(id);
            return result != null ? Ok(result) : NotFound($"id not found in the database");
        }

        // Create a new User
        [HttpPost()]
        [Consumes("application/json")]
        public async Task<ActionResult<User>> CreateUser([FromBody] User User, [FromQuery] bool isHashed = false)
        {
            if (User == null || User.UserId < 0)
            {
                return BadRequest();
            }

            var result = await _userService.CreateUser(User, isHashed);
            return result != null ? Ok(result) : BadRequest();
        }

        // Update a User
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] User User)
        {
            if (User == null || User.UserId <= 0 || id <= 0)
            {
                return BadRequest();
            }
            var result = await _userService.UpdateUser(id, User);
            return result ? Ok(result) : BadRequest();
        }

        // Delete a User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _userService.DeleteUser(id);
            return result ? Ok(result) : BadRequest(result);
        }
    }
}
