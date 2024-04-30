using Application.JobTitleA.Contracts;
using Application.JobTitleA.DTOs;
using Domain.JobTitle;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobTitlesController : ControllerBase
    {
        private readonly IJobTitleService _jobTitleService;

        public JobTitlesController(IJobTitleService jobTitleService)
        {
            _jobTitleService = jobTitleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _jobTitleService.GetAllAsync(pageNumber, pageSize);
            if (!result.Success)
                return StatusCode(result.ErrorCode ?? 500, result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpGet("withDeleted")] // just for example, it's better to use ?includeDeleted=true and GetAllAsync with include delete argument
        public async Task<IActionResult> GetAllWithDeleted()
        {
            var result = await _jobTitleService.GetAllWithDeletedAsync();
            if (!result.Success)
                return StatusCode(result.ErrorCode ?? 500, result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _jobTitleService.GetByIdAsync(id);
            if (!result.Success)
                return StatusCode(result.ErrorCode ?? 500, result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CUJobTitleDto dto)
        {
            var result = await _jobTitleService.AddAsync(dto);
            if (!result.Success)
                return StatusCode(result.ErrorCode ?? 500, result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CUJobTitleDto dto)
        {
            var result = await _jobTitleService.UpdateAsync(id, dto);
            if (!result.Success)
                return StatusCode(result.ErrorCode ?? 500, result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _jobTitleService.DeleteAsync(id);
            if (!result.Success)
                return StatusCode(result.ErrorCode ?? 500, result.ErrorMessage);

            return NoContent();
        }
    }
}
