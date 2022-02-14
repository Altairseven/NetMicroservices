using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformModels.Data;
using PlatformModels.Dtos;
using PlatformModels.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase {

    private readonly IRepository<Platform> _repo;
    private readonly IMapper _mapper;

    public PlatformController(IRepository<Platform> repo, IMapper mapper) {
        _repo = repo;
        _mapper = mapper;
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
    public ActionResult Post([FromBody] PlatformCreateDto platform) {
        var en = _mapper.Map<Platform>(platform);
        _repo.Create(en);
        _repo.SaveChanges();

        var dto = _mapper.Map<PlatformDto>(en);

        return CreatedAtAction(nameof(GetById), new { Id = dto.Id }, dto);
        /*return Ok(dto);*/
    }

}
