using AutoMapper;
using CommandModels.Data;
using CommandModels.Dtos;
using CommandModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase {
    private readonly ICommandRepo _repo;
    private IMapper _mapper;

    public PlatformsController(ICommandRepo repository, IMapper mapper) {
        _repo = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms() {
        Console.WriteLine("--> Getting platforms from Command service");
        var list = _repo.GetAllPlatforms() ?? new List<Platform>();
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(list));
    }

    [HttpPost]
    public ActionResult TestInboundConnection() {
        Console.WriteLine("--> Inbound POST # Command Service");
        return Ok("Inbound Test Response from PlatformsController");
    }



}
