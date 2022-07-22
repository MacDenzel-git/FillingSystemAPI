using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;
using TFSDataAccessLayer.DTO;

namespace TFSBusinessLogicLayer.Services.DepartmentServiceContainer
{
    public interface IDepartmentService
    {
        Task<OutputHandler> Create(DepartmentDTO departmentDTO);
        Task<OutputHandler> Update(DepartmentDTO departmentDTO);
        Task<OutputHandler> Delete(int departmentId);
        Task<IEnumerable<DepartmentDTO>> GetDepartments();
        Task<DepartmentDTO> GetDepartment(int departmentId);

    }
}
