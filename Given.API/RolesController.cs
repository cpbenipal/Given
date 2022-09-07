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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private IRoleRepository _roleService;
        private IMapper _mapper;
        public RolesController(
            IRoleRepository roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }                             

                          
        [HttpGet("GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleModel>))]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var model = await _roleService.GetAllRolesAsync();
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }
                          
        [HttpGet("GetRoleByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleModel>))]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            try
            {
                var model = await _roleService.GetRoleByNameAsync(name);
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }

        [HttpGet("GetRoleById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var model = await _roleService.GetRoleByIdAsync(id);
            return Ok(model);
        }
            
    }
}