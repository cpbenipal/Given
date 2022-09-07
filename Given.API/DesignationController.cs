using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Given.Repositories.Generic;
using AutoMapper;
using System.Threading.Tasks;
using Given.DataContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Given.Models.Helpers;
using Given.Models;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Security.Claims;

namespace Given.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DesignationController : ControllerBase
    {
        private IDesignationRepository _designationervice;
        private IMapper _mapper;      
        public DesignationController(
            IDesignationRepository Designationervice, IMapper mapper)
        {
            _designationervice = Designationervice;
            _mapper = mapper;  
        }


        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] QueryParam contactListParam)
        {
            try
            {
                var model = await _designationervice.GetAllAsync(contactListParam);
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName()
        {
            try
            {
                var UserId = new Guid(HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name).Value);
                var model = await _designationervice.GetByNameAsync(UserId);
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _designationervice.GetByIdAsync(id);
            return Ok(model);
        }

        /// <summary>
        /// Puts a designation.
        /// </summary>
        /// <param name="uuid">The UUID of the designation.</param>
        /// <param name="designation">The designation object.</param>
        /// <returns>204,400,404</returns>
        [HttpPut("PutDesignation")]
        public async Task<IActionResult> Put(DesignationModel designation)
        {
            designation.IsAdd = false;
            return await SaveDesignation(designation);
        }

        /// <summary>
        /// Posts a designation.
        /// </summary>
        /// <param name="designation">The designation object.</param>
        /// <returns>201</returns>
        [HttpPost("PostDesignation")]
        public async Task<IActionResult> Post(DesignationModel designation)
        {
            designation.IsAdd = true;
            return await SaveDesignation(designation);
        }

        private async Task<IActionResult> SaveDesignation(DesignationModel designation)
        {
            var result = new ApiResult();
            try
            {
               var response =  await _designationervice.SaveAsync(designation);
                if (response == "200")
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Designation saved", true);
                }
                else if (response == "2601")
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Designation entered is already taken", false);
                else
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Error on save", false);
            }
            catch (DbUpdateConcurrencyException x)
            {
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Error on save", false);

            }
            return Ok(result);
        }

        /// <summary>
        /// Deletes a designation.
        /// </summary>
        /// <param name="id">The ID of the designation.</param>
        /// <returns>200,404</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = new ApiResult();
            var designation = await _designationervice.GetByIdAsync(id);
            if (designation == null)
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "No designation with this Id", false);
            try
            {
                await _designationervice.DeleteSaveAsync(designation);
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Designation deleted", true);
            }
            catch
            {
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Cannot delete due to internal error", false);
            }
            return Ok(result);
        }
        /// <summary>
        /// Checks whether the designation with given id exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> DesignationExists(Guid id)
        {
            var designation = await _designationervice.GetByIdAsync(id);
            return designation != null;
        }

    }
}
