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

namespace TFSBusinessLogicLayer.Services.FileTypeServiceContainer
{
    public class FileTypeService : IFileTypeService
    {
        private readonly GenericRepository<FileType> _fileTypeRepository;
        public FileTypeService(GenericRepository<FileType> fileTypeRepository)
        {
            _fileTypeRepository = fileTypeRepository;
        }

        public async Task<OutputHandler> Create(FileTypeDTO fileTypeDTO)
        {
            try
            {
                bool isExist = await _fileTypeRepository.AnyAsync(x => x.FileTypeDescription == fileTypeDTO.FileTypeDescription && x.DepartmentId == fileTypeDTO.DepartmentId);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"{fileTypeDTO.FileTypeDescription} already exist in the database"

                    };
                }

                //create folder first 
                var outputHandler = Helper.CreateFolder(fileTypeDTO.FileTypeDescription,fileTypeDTO.folderUrl);
                if (outputHandler.IsErrorOccured)
                {
                    return outputHandler;
                }
                var mapped = new AutoMapper<FileTypeDTO, FileType>().MapToObject(fileTypeDTO);

                await _fileTypeRepository.AddAsync(mapped);
                await _fileTypeRepository.SaveChanges();
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

        public async Task<OutputHandler> Delete(int fileTypeId)
        {

            try
            {

                var fileType = await _fileTypeRepository.GetItem(x => x.FileTypeId == fileTypeId);
                var delete = Helper.DeleteFolder(fileType.FileTypeDescription);
                if (delete.IsErrorOccured)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = delete.Message
                    };
                }
                await _fileTypeRepository.DeleteAsync(fileType);
                await _fileTypeRepository.SaveChanges();
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

        public async Task<FileTypeDTO> GetFileType(int fileTypeId)
        {
            return new AutoMapper<FileType, FileTypeDTO>().MapToObject(await _fileTypeRepository.GetItem(x => x.FileTypeId == fileTypeId));
        }

        public async Task<IEnumerable<FileTypeDTO>> GetFileTypes(int departmentId)
        {
            var fileTypes = await _fileTypeRepository.GetListAsync(x => x.DepartmentId == departmentId);
            return new AutoMapper<FileType, FileTypeDTO>().MapToList(fileTypes);
        }

        public async Task<OutputHandler> Update(FileTypeDTO fileTypeDTO)
        {
            try
            {
                var fileType = await _fileTypeRepository.GetItemAsync(x=>x.FileTypeId == fileTypeDTO.FileTypeId);
                var isDirectoryEmpty = Helper.IsFolderEmpty(fileType.FileTypeDescription);
                //var isDirectoryEmpty = Helper.IsFolderEmpty(fileTypeDTO.FileTypeDescription);
                
                if (!isDirectoryEmpty)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "This FileType cannot be updated because it has files attached to it"
                    };
                }
                var mapped = new AutoMapper<FileTypeDTO, FileType>().MapToObject(fileTypeDTO);
                await _fileTypeRepository.UpdateAsync(mapped);
                await _fileTypeRepository.SaveChanges();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Record updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = ex.Message
                };
                //return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<OutputHandler> ChangeStatus(int fileTypeId)
        {
            try
            {
                var fileType = await _fileTypeRepository.GetItem(x => x.FileTypeId == fileTypeId);
                if (fileType.IsActive)
                {
                    fileType.IsActive = false;
                }
                else
                {
                    fileType.IsActive = true;
                }

                // var mapped = new AutoMapper<FileTypeDTO, FileType>().MapToObject(fileType);
                await _fileTypeRepository.UpdateAsync(fileType);
                await _fileTypeRepository.SaveChanges();

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
