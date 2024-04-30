using Application.Common;
using Application.JobTitleA.DTOs;
using Domain.JobTitle;

namespace Application.JobTitleA.Contracts
{
    public interface IJobTitleService
    {
        Task<OperationResult<PagedResult<JobTitle>>> GetAllAsync(int pageNumber, int pageSize); // can be replaced with object
        Task<OperationResult<IEnumerable<JobTitle>>> GetAllWithDeletedAsync();
        Task<OperationResult<JobTitle>> GetByIdAsync(Guid id);
        Task<OperationResult<JobTitle>> AddAsync(CUJobTitleDto dto);
        Task<OperationResult<JobTitle>> UpdateAsync(Guid id, CUJobTitleDto dto);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}
