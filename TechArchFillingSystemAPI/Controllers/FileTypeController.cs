using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSBusinessLogicLayer.Services.FileTypeServiceContainer;
using TFSDataAccessLayer.DTO;

namespace TechArchFillingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileTypeController : ControllerBase
    {
        private readonly IFileTypeService _service;
        public FileTypeController(IFileTypeService service)
        {
            _service = service;
        }
       
        /// <summary>
        /// This is the API for creating File Types
        /// </summary>
        /// <param name="fileTypeDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(FileTypeDTO fileTypeDTO)
        {
            var outputHandler = await _service.Create(fileTypeDTO);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating FileType
        /// </summary>
        /// <param name="fileTypeDTO"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(FileTypeDTO fileTypeDTO)
        {
            var outputHandler = await _service.Update(fileTypeDTO);
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

        [HttpGet("GetFileTypes")]
        public async Task<IActionResult> GetFileTypes(int departmentId)
        {
            var output = await _service.GetFileTypes(departmentId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a file type 
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int fileTypeId)
        {
            var output = await _service.Delete(fileTypeId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int fileTypeId)
        {
            var output = await _service.ChangeStatus(fileTypeId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        /// <summary>
        /// This is API that gets a File Type
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetFileType")]
        public async Task<IActionResult> GetFileType(int fileTypeId)
        {
            var output = await _service.GetFileType(fileTypeId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
