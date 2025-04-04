using Distri.Backend.Data;
using Distri.Backend.Helpers;
using Distri.Backend.Repositories.Interfaces;
using Distri.Shared.DTOs;
using Distri.Shared.Entities;
using Distri.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Distri.Backend.Repositories.Implementations
{
     public class CountriesRepository : GenericRepository<Country>,ICountriesRepository
    {
        private readonly DataContext _context;
        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Countries
                .Include(c => c.States)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Countries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }


        public override async Task<ActionResponse<Country>> GetAsync(int id)
        {
            var country = await _context.Countries
                 .Include(c => c.States!)
                 .ThenInclude(s => s.Cities)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return new ActionResponse<Country>
                {
                    WasSuccess = false,
                    Message = "País no existe"
                };
            }

            return new ActionResponse<Country>
            {
                WasSuccess = true,
                Result = country
            };

        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
                .OrderBy(c => c.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = countries
            };
        }

        public async Task<IEnumerable<Country>> GetComboAsync()
        {
            return await _context.Countries
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}
