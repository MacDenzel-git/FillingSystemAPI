using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;
using TFSDataAccessLayer.DTO;

namespace TFSBusinessLogicLayer.Services.FileTypeCategoryServiceContainer
{
    public interface IFileTypeCategoryService
    {
        Task<OutputHandler> Create(FileTypeCategoryDTO fileTypeDTO);
        Task<OutputHandler> Update(FileTypeCategoryDTO fileTypeDTO);
        Task<OutputHandler> Delete(int fileTypeId);
        Task<OutputHandler> ChangeStatus(int fileTypeId);
        Task<IEnumerable<FileTypeCategoryDTO>> GetFileTypeCategories(int departmentId);
        Task<FileTypeCategoryDTO> GetFileTypeCategory(int fileTypeId);

    }
}
