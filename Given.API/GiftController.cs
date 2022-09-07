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
    public class GiftController : ControllerBase
    {
        private IGiftRepository _Giftervice;
        private IMapper _mapper;      
        public GiftController(
            IGiftRepository Giftervice, IMapper mapper)
        {
            _Giftervice = Giftervice;
            _mapper = mapper;  
        }
          

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] QueryParam contactListParam)
        {
            try
            {                               
                var model = await _Giftervice.GetAllAsync(contactListParam);
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
            var model = await _Giftervice.GetByIdAsync(id);
            return Ok(model);
        }

        /// <summary>
        /// Puts a Gift.
        /// </summary>
        /// <param name="uuid">The UUID of the Gift.</param>
        /// <param name="Gift">The Gift object.</param>
        /// <returns>204,400,404</returns>
        [HttpPut("PutGift")]
        public async Task<IActionResult> Put(GiftModel Gift)
        {
            Gift.IsAdd = false;
            return await SaveGift(Gift);
        }

        /// <summary>
        /// Posts a Gift.
        /// </summary>
        /// <param name="Gift">The Gift object.</param>
        /// <returns>201</returns>
        [HttpPost("PostGift")]
        public async Task<IActionResult> Post(GiftModel Gift)
        {
            Gift.IsAdd = true;
            return await SaveGift(Gift);
        }

        private async Task<IActionResult> SaveGift(GiftModel Gift)
        {
            var result = new ApiResult();
            try
            {
               var response =  await _Giftervice.SaveAsync(Gift);
                if (response == "200")
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Gift saved", true);
                }
                else if (response == "2601")
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Gift entered is already taken", false);
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
        /// Deletes a Gift.
        /// </summary>
        /// <param name="id">The ID of the Gift.</param>
        /// <returns>200,404</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = new ApiResult();
            var Gift = await _Giftervice.GetByIdAsync(id);
            if (Gift == null)
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "No Gift with this Id", false);
            try
            {
                await _Giftervice.DeleteSaveAsync(Gift);
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Gift deleted", true);
            }
            catch
            {
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Cannot delete due to internal error", false);
            }
            return Ok(result);
        }     
    }
}
