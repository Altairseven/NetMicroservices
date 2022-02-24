using AutoMapper;
using CommandModels.Data;
using CommandModels.Dtos;
using CommandModels.Models;
using System.Text.Json;

namespace CommandService.EventProcessing;

public enum EventType {
    PlatformPublished, Undetermined
}


public class EventProcessor : IEventProcessor {
    
    private readonly IServiceScopeFactory _scopeFactory;
    private IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper) {

        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public void ProcessEvent(string message) {
        var eventType = DetermineEvent(message);

        switch (eventType) {
            case EventType.PlatformPublished:
                AddPlatform(message);
                break;
            case EventType.Undetermined:
                break;
            default:
                break;
        }
    }

    private bool AddPlatform(string PlatformPublishedMessage) {
        using var scope = _scopeFactory.CreateScope();

        //Get repo inside singleton instance
        var _repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

        var platformPublishDto = JsonSerializer.Deserialize<PlatformPublishDto>(PlatformPublishedMessage);

        try {
            var plat = _mapper.Map<Platform>(platformPublishDto);
            if (_repo.ExternalPlatformExists(plat.ExternalID)) {
                Console.WriteLine($"--> Platform Already Exists");
            }
            else { 
                _repo.CreatePlatform(plat);
                _repo.SaveChanges();
                Console.WriteLine("--> Platform Added");
            }
            return true;
        }
        catch (Exception ex) {
            Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
            return false;
        }

    }


    private EventType DetermineEvent(string notificationMessage) {
        Console.WriteLine("--> Determining Eventy Type");

        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        EventType result;
        switch (eventType!.Event) {
            case "Platform_Published":
                Console.WriteLine("--> Platform Publish Event Detected");
                result = EventType.PlatformPublished;
                break;
            default:
                Console.WriteLine("--> Could not determinine event Type");
                result = EventType.Undetermined;
                break;
        }
        return result;
    }



   
}


