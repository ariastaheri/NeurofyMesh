using Microsoft.Extensions.Logging;
using NeurofyMesh.Models;

namespace NeurofyMesh.Services
{
    public class EventService : IEventService
    {
        private readonly MyDbContext _context;
        private readonly IDeviceService _deviceService;

        public EventService(MyDbContext dbContext, IDeviceService deviceService)
        {
            _context = dbContext;
            _deviceService = deviceService;
        }
        public async Task<Event> CreateEvent(Event eventData)
        {
            if (eventData.EventId < 0)
            {
                var existing = GetEventById(eventData.EventId);

                if (existing != null)
                {
                    Console.WriteLine($"EventId {eventData.EventId} already exists!");
                    return existing;
                }
            }

            var newEvent = _context.Events.Add(eventData);
            await _context.SaveChangesAsync();
            return newEvent.Entity;
        }

        public async Task<bool> DeleteEvent(int id)
        {
            var eventData = _context.Events.Where(v => v.EventId == id).FirstOrDefault();

            if (eventData != null)
            {
                try
                {
                    _context.Events.Remove(eventData);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
            Console.WriteLine($"Event ID {id} does not exist in the database");
            return false;
        }

        public Event? GetEventById(int eventId)
        {
            var eventData = _context.Events.Where(v => v.EventId == eventId).FirstOrDefault();

            return eventData;
        }

        public List<Event> GetEvents()
        {
            var eventDatas = _context.Events.ToList();

            return eventDatas;
        }

        public List<Event>? GetEventsByDeviceId(string deviceId)
        {
            var eventData = _context.Events.Where(v => v.DeviceId == deviceId).ToList();

            return eventData;
        }

        public List<Event>? GetEventsByVendorId(int vendorId)
        {
            List<Event> events = new List<Event>();

            var devices = _deviceService.GetDevicesByVendorId(vendorId);

            if (devices == null || devices.Count == 0) return null;

            foreach(var device in devices)
            {
                var eventsPerDevice = GetEventsByDeviceId(device.DeviceId);
                if(eventsPerDevice!= null && eventsPerDevice.Count > 0) 
                {
                    foreach (var eventPerDevice in eventsPerDevice)
                    {
                        events.Add(eventPerDevice);
                    }
                }
            }
            return events;
        }

        public async Task<bool> UpdateEventById(int id, Event eventData)
        {
            var existing = GetEventById(id);
            if (existing != null)
            {
                existing.DeviceId = eventData.DeviceId;
                existing.EventType = eventData.EventType;
                existing.Value = eventData.Value;
                existing.DateTime = eventData.DateTime;
                _context.Events.Update(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            Console.WriteLine($"Event with Id {id} doesn't exist!");
            return false;
        }
    }
}
