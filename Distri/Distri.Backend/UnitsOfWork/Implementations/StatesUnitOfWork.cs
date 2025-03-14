using Distri.Backend.Repositories.Interfaces;
using Distri.Backend.UnitsOfWork.Interfaces;
using Distri.Shared.Entities;
using Distri.Shared.Responses;

namespace Distri.Backend.UnitsOfWork.Implementations
{
    public class StatesUnitOfWork : GenericUnitOfWork<State>,IStatesUnitOfWork
    {
        private readonly IStatesRepository _statesRepository;

        public StatesUnitOfWork(IGenericRepository<State> repository, IStatesRepository statesRepository) : base(repository)
        {
            _statesRepository = statesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync() => await _statesRepository.GetAsync();

        public override async Task<ActionResponse<State>> GetAsync(int id) => await _statesRepository.GetAsync(id);
    }
}
