using Distri.Backend.UnitsOfWork.Interfaces;
using Distri.Shared.DTOs;
using Distri.Shared.Entities;
using Distri.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Distri.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : GenericController<Category>
    {
        private readonly ICategoriesUnitOfWork _categoriesUnitOfWork;
        public CategoriesController(IGenericUnitOfWork<Category> unit, ICategoriesUnitOfWork categoriesUnitOfWork) : base(unit)
        {
            _categoriesUnitOfWork = categoriesUnitOfWork;
        }   

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _categoriesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _categoriesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
