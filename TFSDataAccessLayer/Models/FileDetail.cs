using System;
using System.Collections.Generic;

#nullable disable

namespace TFSDataAccessLayer.Models
{
    public partial class FileDetail
    {
        public int FileDetailsId { get; set; }
        public string FileName { get; set; }
        public int FileTypeCategoryId { get; set; }
        public DateTime? Dated { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsArchived { get; set; }
        public string FileLocation { get; set; }
        public string Month { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual FileTypeCategory FileTypeCategory { get; set; }
    }
}
