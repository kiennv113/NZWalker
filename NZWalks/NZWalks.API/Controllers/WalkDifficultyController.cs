using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Repositories.Implements;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await walkDifficultyRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result= await walkDifficultyRepository.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(WalkDifficultyVM walkDifficultyVM)
        {
            var result = new Models.Domain.WalkDifficulty()
            {
                Code = walkDifficultyVM.Code
            };
            await walkDifficultyRepository.AddAsync(result);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await walkDifficultyRepository.DeleteAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(result);
            return Ok(walkDifficultyDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id,[FromBody] WalkDifficultyVM walkDifficultyVM)
        {
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = walkDifficultyVM.Code
            };

            var result = await walkDifficultyRepository.UpdateAsync(id, walkDifficulty);

            if (result == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(result);

            return Ok(walkDifficultyDTO);
        }
    }
}
