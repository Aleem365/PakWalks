using AutoMapper;
using PaKWalks.CustomActionFilters;
using Domain_OverView.Models.Domain_Model;
using Microsoft.AspNetCore.Mvc;
using PaKWalks.Models.DTO;
using PaKWalks.Repositories;
using System.Net;

namespace PaKWalks.Controllers
{
    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;


        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;

        }

        // Create Walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map AddWalkRequestDto DTO to Walk Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            //create Domain Model
            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);


            // Map  Walk Domain Model to WalkDto DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Get Walks 
        // GET: /api/walks?filterOn=Name&filterQuery=Beach&sortBy=Name&isAscending=true&pageNumber=1&pageSize=100
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            var walkDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            // Map Domain Model to DTO and return DTO
            return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));

        }

        //Get walk by Id
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            // if Domain Model not exit return not found
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Converting Domain Model to DTO and return to Client
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        // Update Walk by Id
        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map DTo To Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            //update Walk Domain Model
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            // Map Domain Model to DTO and return DTO to the Client
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Delete Walk By Id
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model into DTO and Return DTO to the Client
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

    }
}
