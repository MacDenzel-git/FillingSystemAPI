using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;
using TFSDataAccessLayer.DTO;
using TFSDataAccessLayer.General;
using TFSDataAccessLayer.Models;

namespace TFSBusinessLogicLayer.Services.FileTypeCategoryServiceContainer
{
    public class FileTypeCategoryService : IFileTypeCategoryService
    {
        private readonly GenericRepository<FileTypeCategory> _fileTypeCategoryRepository;
        private readonly GenericRepository<FileType> _fileTypeRepository;
        public FileTypeCategoryService(GenericRepository<FileTypeCategory> fileTypeCategoryRepository,GenericRepository<FileType> fileTypeRepository)
        {
            _fileTypeCategoryRepository = fileTypeCategoryRepository;
            _fileTypeRepository = fileTypeRepository;
        }
        public async Task<OutputHandler> Create(FileTypeCategoryDTO fileTypeCategoryDTO)
        {
            try
            {
                bool isExist = await _fileTypeCategoryRepository.AnyAsync(x => x.FileTypeCategoryDescription == fileTypeCategoryDTO.FileTypeCategoryDescription && x.DepartmentId == fileTypeCategoryDTO.DepartmentId);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"{fileTypeCategoryDTO.FileTypeCategoryDescription} already exist in the database"

                    };
                }
                //create folder first 
                var fileType = await _fileTypeRepository.GetItem(x => x.FileTypeId == fileTypeCategoryDTO.FileTypeId);
                string rootFileTypeFolderName =$"{fileTypeCategoryDTO.folderUrl}\\{fileType.FileTypeDescription}";
                var outputHandler = Helper.CreateFolder(fileTypeCategoryDTO.FileTypeCategoryDescription,rootFileTypeFolderName);
                if (outputHandler.IsErrorOccured)
                {
                    return outputHandler; 
                }
                var mapped = new AutoMapper<FileTypeCategoryDTO, FileTypeCategory>().MapToObject(fileTypeCategoryDTO);

                await _fileTypeCategoryRepository.AddAsync(mapped);
                await _fileTypeCategoryRepository.SaveChanges();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Record Created Successfully"
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int fileTypeCategoryId)
        {

            try
            {
                
                var fileTypeCategory = await _fileTypeCategoryRepository.GetItem(x => x.FileTypeId == fileTypeCategoryId);
                var delete = Helper.DeleteFolder(fileTypeCategory.FileTypeCategoryDescription);
                if (delete.IsErrorOccured)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = delete.Message
                    };
                }
                await _fileTypeCategoryRepository.DeleteAsync(fileTypeCategory);
                await _fileTypeCategoryRepository.SaveChanges();
                return new OutputHandler
                    {
                        IsErrorOccured = false,
                        Message = "Record Deleted Successfully"
                    };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<FileTypeCategoryDTO> GetFileTypeCategory(int fileTypeCategoryId)
        {
            return new AutoMapper<FileTypeCategory, FileTypeCategoryDTO>().MapToObject(await _fileTypeCategoryRepository.GetItem(x => x.FileTypeCategoryId == fileTypeCategoryId));
        }

        public async Task<IEnumerable<FileTypeCategoryDTO>> GetFileTypeCategories(int departmentId)
        {
            var fileTypeCategory = await _fileTypeCategoryRepository.GetListAsync( x => x.DepartmentId == departmentId);
            return new AutoMapper<FileTypeCategory, FileTypeCategoryDTO>().MapToList(fileTypeCategory);
        }

        public async Task<OutputHandler> Update(FileTypeCategoryDTO fileTypeCategoryDTO)
        {
            try
            {
                bool isExist = await _fileTypeCategoryRepository.AnyAsync(x => x.FileTypeCategoryDescription == fileTypeCategoryDTO.FileTypeCategoryDescription);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"{fileTypeCategoryDTO.FileTypeCategoryDescription} already exist in the database"

                    };
                }
                var mapped = new AutoMapper<FileTypeCategoryDTO, FileTypeCategory>().MapToObject(fileTypeCategoryDTO);
               var isDirectoryEmpty = Helper.IsFolderEmpty(fileTypeCategoryDTO.FileTypeCategoryDescription);
                if (!isDirectoryEmpty)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "This FileTypeCategory cannot be updated because it has files attached to it"
                    };
                }
                await _fileTypeCategoryRepository.UpdateAsync(mapped);
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Record updated successfully"
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


        public async Task<OutputHandler> ChangeStatus(int fileTypeCategoryId)
        {
            try
            {

                var fileTypeCategory = await _fileTypeCategoryRepository.GetItem(x => x.FileTypeCategoryId == fileTypeCategoryId);
                if (fileTypeCategory.IsActive)
                {
                    fileTypeCategory.IsActive = false;
                }
                else
                {
                    fileTypeCategory.IsActive = true;
                }

                // var mapped = new AutoMapper<FileTypeCategoryDTO, FileTypeCategory>().MapToObject(fileTypeCategory);
                await _fileTypeCategoryRepository.UpdateAsync(fileTypeCategory);
                await _fileTypeCategoryRepository.SaveChanges();

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Status updated Successfully"
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }
    }
}
