using System;
using System.Collections.Generic;

#nullable disable

namespace TFSDataAccessLayer.Models
{
    public partial class Department
    {
        public Department()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            FileDetails = new HashSet<FileDetail>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int SubsidiaryCompanyId { get; set; }

        public virtual SubsidiaryCompany SubsidiaryCompany { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<FileDetail> FileDetails { get; set; }
    }
}
