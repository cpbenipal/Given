using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Given.Repositories.Generic;
using AutoMapper;
using Given.Models;
using System.Threading.Tasks;
using Given.Models.Helpers;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Given.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private IContactRepository _contactService;
        private IMapper _mapper;
       
        public ContactsController(
            IContactRepository contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;  
        }


        [HttpPost]        
        public async Task<IActionResult> GetAll([FromBody] QueryParam contactListParam)
        {     
            try
            {
                var model = await _contactService.GetAllAsync(contactListParam);
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
            try
            {
                var model = await _contactService.GetByIdAsync(id);
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
                var model = await _contactService.GetByNameAsync(UserId);      
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }
        [HttpPost("PostContact")]
        public async Task<IActionResult> PostContact(ContactModel model)
        {
            model.IsAdd = true;
            return await SaveContact(model);
        }
        [HttpPut("PutContact")]
        public async Task<IActionResult> PutContact(ContactModel model)
        {
            model.IsAdd = false;
            return await SaveContact(model);
        }
        private async Task<IActionResult> SaveContact(ContactModel model)
        {
            var result = new ApiResult();
            try
            {    
                var response = await _contactService.SaveAsync(model);
                if (response == "200")
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Contact information saved", true);
                }
                else if (response == "403")
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Contact not found!", false);
                }
                else
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Please verify detail! May be duplicate informaion!", false);
                }
            }
            catch (AppException x)
            {
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, x.InnerException.Message, false);
            }
            return Ok(result);
        }

        [HttpDelete("{contactId}")]  
        public async Task<IActionResult> Delete(Guid contactId)
        {                                                      
            var result = new ApiResult();
            try
            {
                var response = await _contactService.DeleteSaveAsync(contactId);
                if (response == "200")      
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Contact Deleted", true);
                }
                else
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Delete unsuccessfull. Check again. !", false);
                }
            }
            catch (AppException x)
            {
                result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, x.InnerException.Message, false);
            }
            return Ok(result);
        }
    }
}