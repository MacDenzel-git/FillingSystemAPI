using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;
using TFSDataAccessLayer.DTO;

namespace TFSBusinessLogicLayer.Services.FileDetailServiceContainer
{
    public interface IFileDetailService
    {
        Task<OutputHandler> Create(FileDetailDTO fileDetailDTO);
        Task<OutputHandler> Update(FileDetailDTO fileDetailDTO);
        Task<OutputHandler> Delete(int fileDetailId);
        Task<OutputHandler> ChangeStatus(int fileDetailId);
        Task<IEnumerable<FileDetailDTO>> GetAllFiles(int departmentId);
        Task<OutputHandler> GetFileDetails(int fileTypeId);

    }
}
