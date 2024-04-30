using Application.Common;
using Application.JobTitleA.Contracts;
using Application.JobTitleA.DTOs;
using Domain.JobTitle;

namespace Application.JobTitleA
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository _jobTitleRepository;

        public JobTitleService(IJobTitleRepository jobTitleRepository)
        {
            _jobTitleRepository = jobTitleRepository;
        }

        public async Task<OperationResult<PagedResult<JobTitle>>> GetAllAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 0)
                return OperationResult<PagedResult<JobTitle>>.Fail("Page number must be greater than negative number.", 400);

            if (pageSize < 0)
                return OperationResult<PagedResult<JobTitle>>.Fail("Page size must be greater than negative number.", 400);

            var pagedResult = await _jobTitleRepository.GetAllPagedAsync(pageNumber, pageSize);

            return OperationResult<PagedResult<JobTitle>>.Ok(pagedResult);
        }

        public async Task<OperationResult<IEnumerable<JobTitle>>> GetAllWithDeletedAsync()
        {
            var result = await _jobTitleRepository.GetAllWithDeletedAsync();

            return OperationResult<IEnumerable<JobTitle>>.Ok(result);
        }

        public async Task<OperationResult<JobTitle?>> GetByIdAsync(Guid id)
        {
            var jobTitle = await _jobTitleRepository.GetByIdAsync(id);
            if (jobTitle == null)
                return OperationResult<JobTitle?>.Fail("Job title not found.", 404);

            return OperationResult<JobTitle?>.Ok(jobTitle);
        }

        public async Task<OperationResult<JobTitle>> AddAsync(CUJobTitleDto dto)
        {
            var jobTitle = new JobTitle(name: dto.Name);
            await _jobTitleRepository.AddAsync(jobTitle);

            return OperationResult<JobTitle>.Ok(jobTitle);
        }

        public async Task<OperationResult<JobTitle>> UpdateAsync(Guid id, CUJobTitleDto dto)
        {
            var jobTitle = await _jobTitleRepository.GetByIdAsync(id);
            if (jobTitle == null)
                return OperationResult<JobTitle>.Fail("Job title not found.", 404);

            jobTitle.Update(name: dto.Name);
            await _jobTitleRepository.UpdateAsync(jobTitle);

            return OperationResult<JobTitle>.Ok(jobTitle);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var jobTitle = await _jobTitleRepository.GetByIdAsync(id);
            if (jobTitle == null)
                return OperationResult.Fail("Job title not found.", 404);

            jobTitle.SoftDelete();
            await _jobTitleRepository.UpdateAsync(jobTitle);

            return OperationResult.Ok();
        }
    }
}
