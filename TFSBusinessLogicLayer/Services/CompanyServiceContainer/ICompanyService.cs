using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;
using TFSDataAccessLayer.DTO;

namespace TFSBusinessLogicLayer.Services.CompanyServiceContainer
{
    public interface ICompanyService
    {
        Task<OutputHandler> Create(CompanyDTO companyDTO);
        Task<OutputHandler> Update(CompanyDTO companyDTO);
        Task<OutputHandler> Delete(int companyId);
    
        Task<IEnumerable<CompanyDTO>> GetCompanies();
        Task<CompanyDTO> GetCompany(int companyId);

    }
}
