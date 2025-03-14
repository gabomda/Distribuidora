using Distri.Shared.Entities;
using Distri.Shared.Responses;

namespace Distri.Backend.UnitsOfWork.Interfaces
{
    public interface IStatesUnitOfWork
    {
        Task<ActionResponse<State>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<State>>> GetAsync();
    }
}
