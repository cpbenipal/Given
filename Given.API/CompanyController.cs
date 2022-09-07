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
    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private ICompanyRepository _CompanyService;
        private IMapper _mapper;
        public CompanyController(
            ICompanyRepository CompanyService, IMapper mapper)
        {
            _CompanyService = CompanyService;
            _mapper = mapper;
        }

        [HttpGet("GetAllCompany")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompanyModel>))]
        public async Task<IActionResult> GetAllCompany()
        {
            try
            {
                var model = await _CompanyService.GetAllCompanysAsync();
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }
        [HttpGet("GetCompanyById/{Companyid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompanyModel>))]
        public async Task<IActionResult> GetCompanyById(Guid Companyid)
        {                                                                     
            try
            {
                var model = await _CompanyService.GetCompanyByIdAsync(Companyid);   
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }
        [HttpGet("GetCompanyByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompanyModel>))]
        public async Task<IActionResult> GetCompanyByName(string name)
        {
            try
            {
                var model = await _CompanyService.GetCompanyByNameAsync(name);
                
                return Ok(model);
            }
            catch (Exception x)
            {
                return Ok(x.InnerException.Message);
            }
        }

        [HttpPut("PutCompany")]
        public async Task<IActionResult> Update(UpdateCompanyModel model)
        {                                         
            var result = new ApiResult();
            try
            {                   
                var response = await _CompanyService.UpdateSaveCompanyAsync(model);
                if (response == "200")
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Company information updated", true);
                }
                //else if (response == "404")
                //{
                //    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Duplicate company name!!", true);
                //}
                else  
                {
                    result = CommonMethods.CommonAPIResult("", StatusCodes.Status200OK, "Wrong detail. (Might be duplicate company name), Double check entries!", false);
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