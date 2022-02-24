using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformModels.Data;
using PlatformModels.Dtos;
using PlatformModels.Models;
using PlatformService.AsyncDataServices;
using PlatformService.SyncDataService.Http;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase {

    private readonly IRepository<Platform> _repo;
    private readonly IMapper _mapper;
    private ICommandDataClient _http;
    private readonly IMessageBusClient _bus;

    public PlatformController(
        IRepository<Platform> repo, 
        IMapper mapper, 
        ICommandDataClient http, 
        IMessageBusClient bus
    ) {
        _repo = repo;
        _mapper = mapper;
        _http = http;
        _bus = bus;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformDto>> Get() {
        var items = _repo.GetAll();
        return Ok(_mapper.Map<IEnumerable<PlatformDto>>(items));
    }

    [HttpGet("{id}", Name = "GetById")]
    [ActionName("GetById")]
    public ActionResult<PlatformDto> GetById(int id) {
        var en = _repo.GetById(id);
        if(en == null)
            return NotFound();

        return Ok(_mapper.Map<PlatformDto>(en));
    }


    [HttpPost]
    public async Task<ActionResult<PlatformDto>> Post([FromBody] PlatformCreateDto platform) {
        var en = _mapper.Map<Platform>(platform);
        _repo.Create(en);
        _repo.SaveChanges();

        var dto = _mapper.Map<PlatformDto>(en);


        //send sync message
        try {
            await _http.SendPlatformToCommand(dto);
        }
        catch (Exception ex) {
            Console.WriteLine($"--> Could not set syncronously: {ex.Message}");

        }

        //send Async Message
        try { 
            var publishDTo = _mapper.Map<PlatformPublishDto>(dto);
            publishDTo.Event = "Platform_Published";
            _bus.PublishNewPlatform(publishDTo);
        }
        catch (Exception ex) {
            Console.WriteLine($"--> Could not set asyncronously via the bus: {ex.Message}");
        }

        return CreatedAtAction(nameof(GetById), new { dto.Id }, dto);
        
    }

}
