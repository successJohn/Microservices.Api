using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("starting my controller getting actions");

            var platforms = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }


        [HttpGet("{id}")]
        public ActionResult<PlatformReadDto> GetPlatform(int id)
        {
            var platform = _repository.GetPlatformById(id);
            if (platform != null)
                return Ok(_mapper.Map<PlatformReadDto>(platform));

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform([FromQuery] PlatformCreateDto platformCreate)
        {
            var platformModel = _mapper.Map<Platform>(platformCreate);
            _repository.CreatePlatform(platformModel);

            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return Ok(platformReadDto);
        }
    }
}
