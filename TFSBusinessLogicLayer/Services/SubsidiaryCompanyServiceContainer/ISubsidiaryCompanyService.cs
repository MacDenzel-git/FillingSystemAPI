using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;
using TFSDataAccessLayer.DTO;

namespace TFSBusinessLogicLayer.Services.SubsidiaryCompanyServiceContainer
{
    public interface ISubsidiaryCompanyService { 
        Task<OutputHandler> Create(SubsidiaryCompanyDTO companySubsidiaryDTO);
        Task<OutputHandler> Update(SubsidiaryCompanyDTO companySubsidiaryDTO);
        Task<OutputHandler> Delete(int companySubsidiaryId);
        //Task<OutputHandler> ChangeStatus(int companySubsidiaryId);
        Task<IEnumerable<SubsidiaryCompanyDTO>> GetSubsidiaryCompanies();
        Task<SubsidiaryCompanyDTO> GetSubsidiaryCompany(int companySubsidiaryId);

    }
}
