using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSBusinessLogicLayer.Services.FileDetailServiceContainer;
using TFSBusinessLogicLayer.Services.FileTypeServiceContainer;
using TFSDataAccessLayer.DTO;

namespace TechArchFillingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDetailController : ControllerBase
    {
        private readonly IFileDetailService _service;
        public FileDetailController(IFileDetailService service)
        {
            _service = service;
        }
       
        /// <summary>
        /// This is the API for creating File Types
        /// </summary>
        /// <param name="fileDetailDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(FileDetailDTO fileDetailDTO)
        {
            var outputHandler = await _service.Create(fileDetailDTO);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating FileDetail
        /// </summary>
        /// <param name="fileDetailDTO"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(FileDetailDTO fileDetailDTO)
        {
            var outputHandler = await _service.Update(fileDetailDTO);
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

        [HttpGet("GetAllFiles")]
        public async Task<IActionResult> GetAllFiles(int departmentId)
        {
            var output = await _service.GetAllFiles(departmentId);
            if (output != null)
            {
                return Ok(output);
            }
            return BadRequest();
        }

        /// <summary>
        /// This is the API that deletes a file type 
        /// </summary>
        /// <param name="fileDetailId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int fileDetailId)
        {
            var output = await _service.Delete(fileDetailId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileDetailId"></param>
        /// <returns></returns>
        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int fileDetailId)
        {
            var output = await _service.ChangeStatus(fileDetailId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        /// <summary>
        /// This is API that gets a File Type
        /// </summary>
        /// <param name="fileDetailId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetFileDetail")]
        public async Task<IActionResult> GetFileDetail(int fileDetailId)
        {
            var output = await _service.GetFileDetails(fileDetailId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }

            return Ok(output.Result);

        }

    }
}
