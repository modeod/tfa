using Application.Common;
using Application.JobTitleA.Contracts;
using Domain.JobTitle;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositoies
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly ApplicationDbContext _context;

        public JobTitleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<JobTitle>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.JobTitles.AsQueryable();

            var totalRecords = await query.CountAsync();
            int size = pageSize <= 0 || pageSize > totalRecords ? totalRecords : pageSize;
            int number = pageNumber <= 0 ? 1 : pageNumber;
            int maxPageNumber = (int)Math.Ceiling(totalRecords / (double)size);
            if (number > maxPageNumber)
                number = maxPageNumber;

            var items = await query.Skip((number - 1) * size).Take(size).ToListAsync();

            return new PagedResult<JobTitle>(items, pageNumber, pageSize, totalRecords);
        }

        public async Task<IEnumerable<JobTitle>> GetAllWithDeletedAsync()
        {
            return await _context.JobTitles
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<JobTitle?> GetByIdAsync(Guid id)
        {
            return await _context.JobTitles.FindAsync(id);
        }

        public async Task<JobTitle> AddAsync(JobTitle jobTitle)
        {
            await _context.JobTitles.AddAsync(jobTitle);
            await _context.SaveChangesAsync();

            return jobTitle;
        }

        public async Task UpdateAsync(JobTitle jobTitle)
        {
            _context.Entry(jobTitle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
