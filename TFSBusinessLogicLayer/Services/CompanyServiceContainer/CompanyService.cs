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

namespace TFSBusinessLogicLayer.Services.CompanyServiceContainer
{
    public class CompanyService : ICompanyService
    {
        private readonly GenericRepository<Company> _companyRepository;
        public CompanyService(GenericRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<OutputHandler> Create(CompanyDTO companyDTO)
        {
            try
            {
                bool isExist = await _companyRepository.AnyAsync(x => x.CompanyName == companyDTO.CompanyName);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"{companyDTO.CompanyName} already exist in the database"

                    };
                }
                //create folder first 

                var outputHandler = Helper.CreateFolder(companyDTO.CompanyName);

                Debug.WriteLine(outputHandler);
                var mapped = new AutoMapper<CompanyDTO, Company>().MapToObject(companyDTO);

                await _companyRepository.AddAsync(mapped);
                await _companyRepository.SaveChanges();
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

        public async Task<OutputHandler> Delete(int companyId)
        {

            try
            {
                
                var fileType = await _companyRepository.GetItem(x => x.CompanyId == companyId);
                var delete = Helper.DeleteFolder(fileType.CompanyName);
                if (delete.IsErrorOccured)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = delete.Message
                    };
                }
                await _companyRepository.DeleteAsync(fileType);
                await _companyRepository.SaveChanges();
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

        public async Task<CompanyDTO> GetCompany(int companyId)
        {
            var output = await _companyRepository.GetItem(x => x.CompanyId == companyId);

            return new AutoMapper<Company, CompanyDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompanies()
        {
            var fileTypes = await _companyRepository.GetAll();
            return new AutoMapper<Company, CompanyDTO>().MapToList(fileTypes);
        }

        public async Task<OutputHandler> Update(CompanyDTO companyDTO)
        {
            try
            {
               var mapped = new AutoMapper<CompanyDTO, Company>().MapToObject(companyDTO);
               var isDirectoryEmpty = Helper.IsFolderEmpty(companyDTO.CompanyName);
                if (!isDirectoryEmpty)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "This Company cannot be updated because it has files attached to it"
                    };
                }
                await _companyRepository.UpdateAsync(mapped);
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


        //public async Task<OutputHandler> ChangeStatus(int companyId)
        //{
        //    try
        //    {

        //        var fileType = await _companyRepository.GetItem(x => x.CompanyId == companyId);
        //        if (fileType.)
        //        {
        //            fileType.IsActive = false;
        //        }
        //        else
        //        {
        //            fileType.IsActive = true;
        //        }

        //        // var mapped = new AutoMapper<companyDTO, Company>().MapToObject(fileType);
        //        await _companyRepository.UpdateAsync(fileType);
        //        await _companyRepository.SaveChanges();

        //        return new OutputHandler
        //        {
        //            IsErrorOccured = false,
        //            Message = "Status updated Successfully"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return StandardMessages.getExceptionMessage(ex);
        //    }
        //}
    }
}
