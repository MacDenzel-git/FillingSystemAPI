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

namespace TFSBusinessLogicLayer.Services.DepartmentServiceContainer
{
    public class DepartmentService : IDepartmentService
    {
        private readonly GenericRepository<Department> _departmentRepository;
        private readonly GenericRepository<SubsidiaryCompany> _subsidiaryCompanyRepository;
        private readonly GenericRepository<Company> _companyRepository;
        public DepartmentService(GenericRepository<Company> companyRepository,GenericRepository<Department> departmentRepository,GenericRepository<SubsidiaryCompany> subsidiaryCompanyRepository)
        {
            _departmentRepository = departmentRepository;
            _subsidiaryCompanyRepository = subsidiaryCompanyRepository;
            _companyRepository = companyRepository;
        }
        public async Task<OutputHandler> Create(DepartmentDTO departmentDTO)
        {
            try
            {
                bool isExist = await _departmentRepository.AnyAsync(x => x.DepartmentName == departmentDTO.DepartmentName);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"{departmentDTO.DepartmentName} already exist in the database"

                    };
                }
                //create folder first 
                var subsidiaryCompany = await _subsidiaryCompanyRepository.GetItem(x => x.SubsidiaryCompanyId == departmentDTO.SubsidiaryCompanyId);
                var company = await _companyRepository.GetItem(x => x.CompanyId == subsidiaryCompany.CompanyId);
                string rootfolder = $"{company.CompanyName}\\{subsidiaryCompany.SubsidiaryCompanyName}";

                var outputHandler = Helper.CreateFolder(departmentDTO.DepartmentName, rootfolder);
                var mapped = new AutoMapper<DepartmentDTO, Department>().MapToObject(departmentDTO);

                await _departmentRepository.AddAsync(mapped);
                await _departmentRepository.SaveChanges();
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

        public async Task<OutputHandler> Delete(int departmentId)
        {

            try
            {
                
                var fileType = await _departmentRepository.GetItem(x => x.DepartmentId == departmentId);
                var delete = Helper.DeleteFolder(fileType.DepartmentName);
                if (delete.IsErrorOccured)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = delete.Message
                    };
                }
                await _departmentRepository.DeleteAsync(fileType);
                await _departmentRepository.SaveChanges();
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

        public async Task<DepartmentDTO> GetDepartment(int departmentId)
        {
            var output = await _departmentRepository.GetItem(x => x.DepartmentId == departmentId);

            return new AutoMapper<Department, DepartmentDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<DepartmentDTO>> GetDepartments()
        {
            var fileTypes = await _departmentRepository.GetAll();
            return new AutoMapper<Department, DepartmentDTO>().MapToList(fileTypes);
        }

        public async Task<OutputHandler> Update(DepartmentDTO departmentDTO)
        {
            try
            {
               var mapped = new AutoMapper<DepartmentDTO, Department>().MapToObject(departmentDTO);
               var isDirectoryEmpty = Helper.IsFolderEmpty(departmentDTO.DepartmentName);
                if (!isDirectoryEmpty)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "This Department cannot be updated because it has files attached to it"
                    };
                }
                await _departmentRepository.UpdateAsync(mapped);
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


        //public async Task<OutputHandler> ChangeStatus(int departmentId)
        //{
        //    try
        //    {

        //        var fileType = await _departmentRepository.GetItem(x => x.DepartmentId == departmentId);
        //        if (fileType.)
        //        {
        //            fileType.IsActive = false;
        //        }
        //        else
        //        {
        //            fileType.IsActive = true;
        //        }

        //        // var mapped = new AutoMapper<departmentDTO, Department>().MapToObject(fileType);
        //        await _departmentRepository.UpdateAsync(fileType);
        //        await _departmentRepository.SaveChanges();

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
