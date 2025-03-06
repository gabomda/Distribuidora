using Distri.Backend.UnitsOfWork.Interfaces;
using Distri.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Distri.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : GenericController<Category>
    {
        public CategoriesController(IGenericUnitOfWork<Category> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
