using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSBusinessLogicLayer.Services.ClientServiceContainer;
  using TFSDataAccessLayer.DTO;

namespace TechArchFillingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;
        public ClientController(IClientService service)
        {
            _service = service;
        }
       
        /// <summary>
        /// This is the API for creating File Types
        /// </summary>
        /// <param name="clientDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ClientDTO clientDTO)
        {
            var outputHandler = await _service.Create(clientDTO);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating Client
        /// </summary>
        /// <param name="clientDTO"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(ClientDTO clientDTO)
        {
            var outputHandler = await _service.Update(clientDTO);
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

        [HttpGet("GetClients")]
        public async Task<IActionResult> GetClients(int departmentId)
        {
            var output = await _service.GetClients(departmentId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a file type 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int clientId)
        {
            var output = await _service.Delete(clientId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="clientId"></param>
        ///// <returns></returns>
        //[HttpPut("ChangeStatus")]
        //public async Task<IActionResult> ChangeStatus(int clientId)
        //{
        //    var output = await _service.ChangeStatus(clientId);
        //    if (output.IsErrorOccured)
        //    {
        //        return BadRequest(output);
        //    }
        //    return Ok(output);
        //}

        /// <summary>
        /// This is API that gets a File Type
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetClient")]
        public async Task<IActionResult> GetClient(int clientId)
        {
            var output = await _service.GetClient(clientId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
