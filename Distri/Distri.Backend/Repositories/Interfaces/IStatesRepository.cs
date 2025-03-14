using Distri.Shared.Entities;
using Distri.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Distri.Backend.Repositories.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<State>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<State>>> GetAsync();
    }
}
