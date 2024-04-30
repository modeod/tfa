using Application.Common;
using Domain.JobTitle;

namespace Application.JobTitleA.Contracts
{
    public interface IJobTitleRepository
    {
        Task<PagedResult<JobTitle>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<JobTitle>> GetAllWithDeletedAsync();
        Task<JobTitle?> GetByIdAsync(Guid id);
        Task<JobTitle> AddAsync(JobTitle jobTitle);
        Task UpdateAsync(JobTitle jobTitle);
    }
}
