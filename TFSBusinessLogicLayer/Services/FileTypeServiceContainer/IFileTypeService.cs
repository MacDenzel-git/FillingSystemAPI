using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;
using TFSDataAccessLayer.DTO;

namespace TFSBusinessLogicLayer.Services.FileTypeServiceContainer
{
    public interface IFileTypeService
    {
        Task<OutputHandler> Create(FileTypeDTO fileTypeDTO);
        Task<OutputHandler> Update(FileTypeDTO fileTypeDTO);
        Task<OutputHandler> Delete(int fileTypeId);
        Task<OutputHandler> ChangeStatus(int fileTypeId);
        Task<IEnumerable<FileTypeDTO>> GetFileTypes(int departmentId);
        Task<FileTypeDTO> GetFileType(int fileTypeId);

    }
}
