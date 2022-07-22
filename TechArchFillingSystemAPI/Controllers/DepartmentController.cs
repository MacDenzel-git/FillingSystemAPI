using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSBusinessLogicLayer.Services.DepartmentServiceContainer;
  using TFSDataAccessLayer.DTO;

namespace TechArchFillingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }
       
        /// <summary>
        /// This is the API for creating File Types
        /// </summary>
        /// <param name="departmentDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(DepartmentDTO departmentDTO)
        {
            var outputHandler = await _service.Create(departmentDTO);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Department
        /// </summary>
        /// <param name="departmentDTO"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(DepartmentDTO departmentDTO)
        {
            var outputHandler = await _service.Update(departmentDTO);
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

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            var output = await _service.GetDepartments();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a file type 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int departmentId)
        {
            var output = await _service.Delete(departmentId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="departmentId"></param>
        ///// <returns></returns>
        //[HttpPut("ChangeStatus")]
        //public async Task<IActionResult> ChangeStatus(int departmentId)
        //{
        //    var output = await _service.ChangeStatus(departmentId);
        //    if (output.IsErrorOccured)
        //    {
        //        return BadRequest(output);
        //    }
        //    return Ok(output);
        //}

        /// <summary>
        /// This is API that gets a File Type
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetDepartment")]
        public async Task<IActionResult> GetDepartment(int departmentId)
        {
            var output = await _service.GetDepartment(departmentId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
