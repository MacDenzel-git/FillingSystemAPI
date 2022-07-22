using System;
using System.Collections.Generic;

#nullable disable

namespace TFSDataAccessLayer.Models
{
    public partial class Company
    {
        public Company()
        {
            SubsidiaryCompanies = new HashSet<SubsidiaryCompany>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<SubsidiaryCompany> SubsidiaryCompanies { get; set; }
    }
}
