using Distri.Shared.DTOs;
using Distri.Shared.Entities;
using Distri.Shared.Responses;

namespace Distri.Backend.Repositories.Interfaces
{
    public interface ICitiesRepository
    {
        Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<IEnumerable<City>> GetComboAsync(int stateId);
    }
}
