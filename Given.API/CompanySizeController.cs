using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Given.Repositories.Generic;
using AutoMapper;
using Given.Models;
using System.Threading.Tasks;
using Given.DataContext.Entities;
using Given.Models.Helpers;
using Microsoft.AspNetCore.Http;

namespace Given.Api.Controllers   
{               
    [ApiController]
    [Route("api/[controller]")]
    public class CompanySizeController : ControllerBase
    {
        private ICompanySizeRepository _CompanySizeService;  
        public CompanySizeController(
            ICompanySizeRepository CompanySizeService )
        {
            _CompanySizeService = CompanySizeService;  
        }                             

                          
        [HttpGet("GetAllCompanySizes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompanySizeModel>))]
        public async Task<IActionResult> GetAllCompanySizes()
        {
            try
            {
                var model = await _CompanySizeService.GetAllCompanySizesAsync();
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }
                          
        [HttpGet("GetCompanySizeByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompanySizeModel>))]
        public async Task<IActionResult> GetCompanySizeByName(string name)
        {
            try
            {
                var model = await _CompanySizeService.GetCompanySizeByNameAsync(name);
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }

        [HttpGet("GetCompanySizeById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCompanySizeById(Guid id)
        {
            var model = await _CompanySizeService.GetCompanySizeByIdAsync(id);
            return Ok(model);
        }
            
    }
}