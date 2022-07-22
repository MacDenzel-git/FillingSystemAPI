using System;
using System.Collections.Generic;

#nullable disable

namespace TFSDataAccessLayer.Models
{
    public partial class SubsidiaryCompany
    {
        public SubsidiaryCompany()
        {
            Departments = new HashSet<Department>();
        }

        public int SubsidiaryCompanyId { get; set; }
        public string SubsidiaryCompanyName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
