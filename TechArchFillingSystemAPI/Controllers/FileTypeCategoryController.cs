using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSBusinessLogicLayer.Services.FileTypeCategoryServiceContainer;
 using TFSDataAccessLayer.DTO;

namespace TechArchFillingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileTypeCategoryController : ControllerBase
    {
        private readonly IFileTypeCategoryService _service;
        public FileTypeCategoryController(IFileTypeCategoryService service)
        {
            _service = service;
        }
       
        /// <summary>
        /// This is the API for creating File Types
        /// </summary>
        /// <param name="fileTypeCategoryDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(FileTypeCategoryDTO fileTypeCategoryDTO)
        {
            var outputHandler = await _service.Create(fileTypeCategoryDTO);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating FileType
        /// </summary>
        /// <param name="fileTypeCategoryDTO"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(FileTypeCategoryDTO fileTypeCategoryDTO)
        {
            var outputHandler = await _service.Update(fileTypeCategoryDTO);
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

        [HttpGet("GetFileTypeCategories")]
        public async Task<IActionResult> GetFileTypeCategories(int departmentId)
        {
            var output = await _service.GetFileTypeCategories(departmentId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a file type 
        /// </summary>
        /// <param name="fileTypeCategoryId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int fileTypeCategoryId)
        {
            var output = await _service.Delete(fileTypeCategoryId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileTypeCategoryId"></param>
        /// <returns></returns>
        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int fileTypeCategoryId)
        {
            var output = await _service.ChangeStatus(fileTypeCategoryId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        /// <summary>
        /// This is API that gets a File Type
        /// </summary>
        /// <param name="fileTypeCategoryId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetFileTypeCategory")]
        public async Task<IActionResult> GetFileTypeCategory(int fileTypeCategoryId)
        {
            var output = await _service.GetFileTypeCategory(fileTypeCategoryId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
