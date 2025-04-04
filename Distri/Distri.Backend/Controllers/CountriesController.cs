using Distri.Backend.Data;
using Distri.Backend.UnitsOfWork.Interfaces;
using Distri.Shared.DTOs;
using Distri.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Distri.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>
    {
        private readonly ICountriesUnitOfWork _countriesUnitOfWork;

        //CUANDO SON CONSULTAS COMPLEJAS CON MUCHOS INNER SE PUEDE AGREGAR ASNOTRACKING() ANTES DEL TOLIST PARA QUE NO GUARDE LOS LOGS EF
        public CountriesController(IGenericUnitOfWork<Country> unitOfWork,ICountriesUnitOfWork countriesUnitOfWork) : base(unitOfWork)
        {
            _countriesUnitOfWork = countriesUnitOfWork;
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _countriesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }


        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _countriesUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var response = await _countriesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }


        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        { 
            var response = await _countriesUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _countriesUnitOfWork.GetComboAsync());
        }

    }

}
