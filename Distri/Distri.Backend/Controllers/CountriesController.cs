using Distri.Backend.Data;
using Distri.Backend.UnitsOfWork.Interfaces;
using Distri.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Distri.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>
    {
        //CUANDO SON CONSULTAS COMPLEJAS CON MUCHOS INNER SE PUEDE AGREGAR ASNOTRACKING() ANTES DEL TOLIST PARA QUE NO GUARDE LOS LOGS EF
        public CountriesController(IGenericUnitOfWork<Country> unitOfWork) : base(unitOfWork)
        {

        }
    }

}
