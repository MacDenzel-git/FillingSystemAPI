using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArch.DataHandler;
using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;
using TFSBusinessLogicLayer.Services.FileTypeServiceContainer;
using TFSDataAccessLayer.DTO;
using TFSDataAccessLayer.General;
using TFSDataAccessLayer.Models;

namespace TFSBusinessLogicLayer.Services.FileDetailServiceContainer
{
    public class FileDetailService : IFileDetailService
    {
        private readonly GenericRepository<FileDetail> _fileDetailRepository;
        private readonly GenericRepository<FileType> _fileTypeRepository;
        private readonly GenericRepository<FileTypeCategory> _fileTypeCategoryRepository;
        public FileDetailService(GenericRepository<FileDetail> fileDetailRepository,
            GenericRepository<FileType> fileTypeRepository,
            GenericRepository<FileTypeCategory> fileTypeCategoryRepository)
        {
            _fileDetailRepository = fileDetailRepository;
            _fileTypeRepository = fileTypeRepository;
            _fileTypeCategoryRepository = fileTypeCategoryRepository;
        }
        public async Task<OutputHandler> Create(FileDetailDTO fileDetailDTO)
        {
            try
            {
                //Get Descriptions to formulate path for saving file 
                var fileTypeCategoryFolder = await _fileTypeCategoryRepository.GetItem(x => x.FileTypeCategoryId == fileDetailDTO.FileTypeCategoryId);
                var fileTypeFolder = await _fileTypeRepository.GetItem(x => x.FileTypeId == fileTypeCategoryFolder.FileTypeId);



                string path = Path.Combine("TFS",fileDetailDTO.folderUrl, fileTypeFolder.FileTypeDescription, fileTypeCategoryFolder.FileTypeCategoryDescription, fileDetailDTO.FileName);

                //check if file name already exist to avoid overwritting 
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $" The file name \"{fileDetailDTO.FileName}\" already exist, This operation will overwrite the existing file, please choose another name for the file"

                    };
                }

                var outputHandler = Helper.SaveFileFromByteByPath(fileDetailDTO.UploadedFile, fileDetailDTO.FileName, path);

                if (outputHandler.IsErrorOccured)
                {
                    return outputHandler;
                }

                fileDetailDTO.FileLocation = outputHandler.Result.ToString();
                var mapped = new AutoMapper<FileDetailDTO, FileDetail>().MapToObject(fileDetailDTO);
                await _fileDetailRepository.AddAsync(mapped);
                await _fileDetailRepository.SaveChanges();
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

        public async Task<OutputHandler> Delete(int fileDetailsId)
        {
            try
            {
                var fileDetail = await _fileDetailRepository.GetItem(x => x.FileDetailsId == fileDetailsId);
                await _fileDetailRepository.DeleteAsync(fileDetail);
                await _fileDetailRepository.SaveChanges();
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

        public async Task<OutputHandler> GetFileDetails(int fileDetailsId)
        {
            var outputHandler = new OutputHandler { };
            var mapped = new AutoMapper<FileDetail, FileDetailDTO>().MapToObject(await _fileDetailRepository.GetItem(x => x.FileDetailsId == fileDetailsId));
            try
            {
                outputHandler = await Helper.ConvertFileToByte(mapped.FileLocation);
                mapped.UploadedFile = (byte[])outputHandler.Result;
                outputHandler.Result = mapped;
            }
            catch (Exception ex)
            {

                return StandardMessages.getExceptionMessage(ex);
                
            };
                       
            return outputHandler;
        }

        public async Task<IEnumerable<FileDetailDTO>> GetAllFiles(int departmentId)
        {
            var fileDetails = await _fileDetailRepository.GetListAsync(x => x.DepartmentId == departmentId);
            return new AutoMapper<FileDetail, FileDetailDTO>().MapToList(fileDetails);
        }

        public async Task<OutputHandler> Update(FileDetailDTO fileDetailDTO)
        {
            try
            {
                var mapped = new AutoMapper<FileDetailDTO, FileDetail>().MapToObject(fileDetailDTO);
                await _fileDetailRepository.UpdateAsync(mapped);
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


        public async Task<OutputHandler> ChangeStatus(int fileDetailId)
        {
            try
            {

                var fileDetail = await _fileDetailRepository.GetItem(x => x.FileDetailsId == fileDetailId);
                if (fileDetail.IsArchived)
                {
                    fileDetail.IsArchived = false;
                }
                else
                {
                    fileDetail.IsArchived = true;
                }

                await _fileDetailRepository.UpdateAsync(fileDetail);
                await _fileDetailRepository.SaveChanges();

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
