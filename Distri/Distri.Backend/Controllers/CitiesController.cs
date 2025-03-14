using Distri.Backend.UnitsOfWork.Interfaces;
using Distri.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Distri.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        public CitiesController(IGenericUnitOfWork<City> unitOfWork) : base(unitOfWork)
        {

        }
    }
}
