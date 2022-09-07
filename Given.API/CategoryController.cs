using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Given.Repositories.Generic;
using AutoMapper;
using Given.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Given.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _Categoryservice;
        public CategoryController(
            ICategoryRepository Categoryservice)
        {
            _Categoryservice = Categoryservice;
        }


        [HttpGet("GetAllCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryModel>))]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var model = await _Categoryservice.GetAllCategorysAsync();
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }

        [HttpGet("GetCategoryByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryModel>))]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            try
            {
                var model = await _Categoryservice.GetCategoryByNameAsync(name);
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }

        [HttpGet("GetCategoryById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var model = await _Categoryservice.GetCategoryByIdAsync(id);
            return Ok(model);
        }

    }
}