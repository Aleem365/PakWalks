using AutoMapper;
using PaKWalks.CustomActionFilters;
using Domain_OverView.Models.Domain_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaKWalks.Models.DTO;
using PaKWalks.Repositories;
using System.Text.Json;

namespace PaKWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly ILogger<RegionController> logger;

        public RegionController(IMapper mapper, IRegionRepository regionRepository, ILogger<RegionController> logger)
        {
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.logger = logger;
        }

        // Get all Regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //throw new Exception("This custom exception");
                // Get Data from Database  -Domain Models
                var regionsDomain = await regionRepository.GetAllAsync();

                //Map Domain Models to DTOs
                var regionDtos = mapper.Map<List<RegionDto>>(regionsDomain);

                // return DTOs
                return Ok(regionDtos);
            }

            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

        }

        // get Region single data (get Region single by Id)
        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Data from Database -Domain Models 

            var region = await regionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            // Convert Region Domain Model to Region DTO and return to the Client
            return Ok(mapper.Map<RegionDto>(region));
        }

        // Post To Create  New Region
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Convert DTO to Region Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to Create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Converting Domain Model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // Return DTO to Client
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        // Update region
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            // Check if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // return DTO to the Client
            return Ok(regionDto);
        }

        // Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Get Region Domain Model From database
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            // Check if exit 
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // return deleted Region back 
            // Map Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);

        }
    }
}
