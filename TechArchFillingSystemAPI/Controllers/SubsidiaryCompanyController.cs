using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSBusinessLogicLayer.Services.SubsidiaryCompanyServiceContainer;
 using TFSDataAccessLayer.DTO;

namespace TechArchFillingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubsidiaryCompanyController : ControllerBase
    {
        private readonly ISubsidiaryCompanyService _service;
        public SubsidiaryCompanyController(ISubsidiaryCompanyService service)
        {
            _service = service;
        }
       
        /// <summary>
        /// This is the API for creating File Types
        /// </summary>
        /// <param name="subsidiaryCompanyDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(SubsidiaryCompanyDTO subsidiaryCompanyDTO)
        {
            var outputHandler = await _service.Create(subsidiaryCompanyDTO);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating SubsidiaryCompany
        /// </summary>
        /// <param name="subsidiaryCompanyDTO"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(SubsidiaryCompanyDTO subsidiaryCompanyDTO)
        {
            var outputHandler = await _service.Update(subsidiaryCompanyDTO);
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

        [HttpGet("GetSubsidiaryCompanies")]
        public async Task<IActionResult> GetSubsidiaryCompanies()
        {
            var output = await _service.GetSubsidiaryCompanies();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a file type 
        /// </summary>
        /// <param name="subsidiaryCompanyId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int subsidiaryCompanyId)
        {
            var output = await _service.Delete(subsidiaryCompanyId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="subsidiaryCompanyId"></param>
        ///// <returns></returns>
        //[HttpPut("ChangeStatus")]
        //public async Task<IActionResult> ChangeStatus(int subsidiaryCompanyId)
        //{
        //    var output = await _service.ChangeStatus(subsidiaryCompanyId);
        //    if (output.IsErrorOccured)
        //    {
        //        return BadRequest(output);
        //    }
        //    return Ok(output);
        //}

        /// <summary>
        /// This is API that gets a File Type
        /// </summary>
        /// <param name="subsidiaryCompanyId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetSubsidiaryCompany")]
        public async Task<IActionResult> GetSubsidiaryCompany(int subsidiaryCompanyId)
        {
            var output = await _service.GetSubsidiaryCompany(subsidiaryCompanyId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
