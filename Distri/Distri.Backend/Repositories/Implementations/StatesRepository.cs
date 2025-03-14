using Distri.Backend.Data;
using Distri.Backend.Repositories.Interfaces;
using Distri.Shared.Entities;
using Distri.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Distri.Backend.Repositories.Implementations
{
    public class StatesRepository : GenericRepository<State>, IStatesRepository
    {
        private readonly DataContext _context;

        public StatesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<State>> GetAsync(int id)
        {
            var state = await _context.States.Include(s=>s.Cities).FirstOrDefaultAsync(s=>s.Id==id);

            if (state is null)
            {
                return new ActionResponse<State>
                {
                    WasSuccess = false,
                    Message="Estado no existe"
                };
            }
            
            return new ActionResponse<State> 
            {
            WasSuccess=true,
            Result = state
            };
        }
        
        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
        {
            var states = await _context.States.Include(s=>s.Cities).ToListAsync();
            return new ActionResponse<IEnumerable<State>>()
            {
                WasSuccess = true,
                Result = states
            };
        }
    }
}
