using System;
using System.Collections.Generic;

#nullable disable

namespace TFSDataAccessLayer.Models
{
    public partial class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int DepartmentId { get; set; }
    }
}
