using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSDataAccessLayer.DTO
{
    public class SubsidiaryCompanyDTO
    {
        public int SubsidiaryCompanyId { get; set; }
        public string SubsidiaryCompanyName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CompanyId { get; set; }
    }
}
