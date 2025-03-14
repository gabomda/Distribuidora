using Distri.Shared.Entities;
using Distri.Shared.Responses;

namespace Distri.Backend.Repositories.Interfaces
{
    public interface ICountriesRepository
    {
        Task<ActionResponse<Country>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();
    }
}
