using Microsoft.AspNetCore.Mvc;
using NeurofyMesh.Models;
using NeurofyMesh.Services;

namespace NeurofyMesh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventDataService;

        public EventController(IEventService eventDataService)
        {
            _eventDataService = eventDataService;
        }
        // Get all events
        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents()
        {
            var eventDatas = _eventDataService.GetEvents();
            return eventDatas;
        }

        // Get a event by ID
        [HttpGet("{id}")]
        public ActionResult<Event> GetEventById([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest($"incorrect id provided");

            var result = _eventDataService.GetEventById(id);
            return result != null ? Ok(result) : NotFound($"id not found in the database");
        }

        [HttpGet("/vendor/{id}")]
        public ActionResult<Event> GetEventByVendorId([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest($"incorrect id provided");

            var result = _eventDataService.GetEventsByVendorId(id);
            return result != null ? Ok(result) : NotFound($"id not found in the database");
        }

        // Create a new vendor
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<Event>> CreateEvent([FromBody] Event eventData)
        {
            if (eventData == null || eventData.EventId < 0)
            {
                return BadRequest();
            }

            var result = await _eventDataService.CreateEvent(eventData);
            return result != null ? Ok(result) : BadRequest();
        }

        // Update a vendor
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateEventById([FromRoute] int id, [FromBody] Event eventData)
        {
            if (eventData == null || eventData.EventId < 0)
            {
                return BadRequest();
            }
            var result = await _eventDataService.UpdateEventById(id, eventData);
            return result ? Ok(result) : BadRequest();
        }

        // Delete a vendor
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _eventDataService.DeleteEvent(id);
            return result ? Ok(result) : BadRequest(result);
        }
    }
}
