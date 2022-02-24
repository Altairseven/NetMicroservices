using AutoMapper;
using CommandModels.Data;
using CommandModels.Dtos;
using CommandModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/platforms/{platformId}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase {


    private readonly ICommandRepo _repo;
    private IMapper _mapper;

    public CommandsController(ICommandRepo repository, IMapper mapper) {
        _repo = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsFroPlatform(int platformId) {
        Console.WriteLine("--> Getting Commands for Platform service");
        if (!_repo.PlatformExists(platformId)) {
            return NotFound();
        }

        var list = _repo.GetCommandsForPlatform(platformId);
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(list));
    }


    [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId) {

        Console.WriteLine($"--> Getting CommandForPlatform {platformId}/{commandId}");

        if (!_repo.PlatformExists(platformId)) {
            return NotFound();
        }

        var command = _repo.GetCommand(platformId, commandId);
        if(command == null)
            return NotFound();

        return Ok(_mapper.Map<CommandReadDto>(command));

    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto) {
        Console.WriteLine("--> Creating Command from Platform service");
        if (!_repo.PlatformExists(platformId)) {
            return NotFound();
        }

        var cmd = _mapper.Map<Command>(commandDto);

        _repo.CreateCommand(platformId, cmd);
        _repo.SaveChanges();

        var readDto = _mapper.Map<CommandReadDto>(cmd);

        return CreatedAtAction(nameof(GetCommandForPlatform), 
            new { platformId = platformId, commandId = readDto.Id }, 
            readDto
        );

    }



}







