using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;
using TFSBusinessLogicLayer.Services.SubsidiaryCompanyServiceContainer;
using TFSDataAccessLayer.DTO;
using TFSDataAccessLayer.General;
using TFSDataAccessLayer.Models;

namespace TFSBusinessLogicLayer.Services.CompanySubsidiaryServiceContainer
{
    public class SubsidiaryCompanyService : ISubsidiaryCompanyService
    {
        private readonly GenericRepository<SubsidiaryCompany> _companySubsidiaryRepository;
        private readonly GenericRepository<Company> _companyRepository;
        public SubsidiaryCompanyService(GenericRepository<Company> companyRepository, GenericRepository<SubsidiaryCompany> companySubsidiaryRepository)
        {
            _companySubsidiaryRepository = companySubsidiaryRepository;
            _companyRepository = companyRepository;
        }
        public async Task<OutputHandler> Create(SubsidiaryCompanyDTO subsidiaryCompanyDTO)
        {
            try
            {
                bool isExist = await _companySubsidiaryRepository.AnyAsync(x => x.SubsidiaryCompanyName == subsidiaryCompanyDTO.SubsidiaryCompanyName);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"{subsidiaryCompanyDTO.SubsidiaryCompanyName} already exist in the database"

                    };
                }
                //create folder first 
                var rootFolder = await _companyRepository.GetItem(x => x.CompanyId == subsidiaryCompanyDTO.CompanyId);

                var outputHandler = Helper.CreateFolder(subsidiaryCompanyDTO.SubsidiaryCompanyName, rootFolder.CompanyName);
                var mapped = new AutoMapper<SubsidiaryCompanyDTO, SubsidiaryCompany>().MapToObject(subsidiaryCompanyDTO);

                await _companySubsidiaryRepository.AddAsync(mapped);
                await _companySubsidiaryRepository.SaveChanges();

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
                
                var fileType = await _companySubsidiaryRepository.GetItem(x => x.SubsidiaryCompanyId == companyId);
                var delete = Helper.DeleteFolder(fileType.SubsidiaryCompanyName);
                if (delete.IsErrorOccured)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = delete.Message
                    };
                }
                await _companySubsidiaryRepository.DeleteAsync(fileType);
                await _companySubsidiaryRepository.SaveChanges();
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

        public async Task<SubsidiaryCompanyDTO> GetSubsidiaryCompany(int companyId)
        {
            var output = await _companySubsidiaryRepository.GetItem(x => x.SubsidiaryCompanyId == companyId);

            return new AutoMapper<SubsidiaryCompany, SubsidiaryCompanyDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<SubsidiaryCompanyDTO>> GetSubsidiaryCompanies()
        {
            var fileTypes = await _companySubsidiaryRepository.GetAll();
            return new AutoMapper<SubsidiaryCompany, SubsidiaryCompanyDTO>().MapToList(fileTypes);
        }

        public async Task<OutputHandler> Update(SubsidiaryCompanyDTO subsidiaryCompanyDTO)
        {
            try
            {
               var mapped = new AutoMapper<SubsidiaryCompanyDTO, SubsidiaryCompany>().MapToObject(subsidiaryCompanyDTO);
               var isDirectoryEmpty = Helper.IsFolderEmpty(subsidiaryCompanyDTO.SubsidiaryCompanyName);
                if (!isDirectoryEmpty)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "This Company cannot be updated because it has files attached to it"
                    };
                }
                await _companySubsidiaryRepository.UpdateAsync(mapped);
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

        //        // var mapped = new AutoMapper<subsidiaryCompanyDTO, Company>().MapToObject(fileType);
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
