using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSBusinessLogicLayer.Services.CompanyServiceContainer;
 using TFSDataAccessLayer.DTO;

namespace TechArchFillingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        public CompanyController(ICompanyService service)
        {
            _service = service;
        }
       
        /// <summary>
        /// This is the API for creating File Types
        /// </summary>
        /// <param name="companyDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CompanyDTO companyDTO)
        {
            var outputHandler = await _service.Create(companyDTO);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Company
        /// </summary>
        /// <param name="companyDTO"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(CompanyDTO companyDTO)
        {
            var outputHandler = await _service.Update(companyDTO);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets File Types 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetCompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            var output = await _service.GetCompanies();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a file type 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int companyId)
        {
            var output = await _service.Delete(companyId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="companyId"></param>
        ///// <returns></returns>
        //[HttpPut("ChangeStatus")]
        //public async Task<IActionResult> ChangeStatus(int companyId)
        //{
        //    var output = await _service.ChangeStatus(companyId);
        //    if (output.IsErrorOccured)
        //    {
        //        return BadRequest(output);
        //    }
        //    return Ok(output);
        //}

        /// <summary>
        /// This is API that gets a File Type
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetCompany")]
        public async Task<IActionResult> GetCompany(int companyId)
        {
            var output = await _service.GetCompany(companyId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
