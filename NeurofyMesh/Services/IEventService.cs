using NeurofyMesh.Models;

namespace NeurofyMesh.Services
{
    public interface IEventService
    {
        List<Event> GetEvents();
        Event? GetEventById(int eventId);
        List<Event>? GetEventsByDeviceId(string deviceId);
        List<Event>? GetEventsByVendorId(int vendorId);
        Task<bool> UpdateEventById(int id, Event eventData);
        Task<bool> DeleteEvent(int id);
        Task<Event> CreateEvent(Event eventData);
        List<Event> DecodeUplinkData(TtnUplink uplinkData);
    }
}
