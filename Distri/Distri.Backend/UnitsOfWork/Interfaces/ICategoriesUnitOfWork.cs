using Distri.Shared.DTOs;
using Distri.Shared.Entities;
using Distri.Shared.Responses;

namespace Distri.Backend.UnitsOfWork.Interfaces
{
    public interface ICategoriesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<IEnumerable<Category>> GetComboAsync();

    }
}
