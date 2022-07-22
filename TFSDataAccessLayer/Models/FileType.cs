using System;
using System.Collections.Generic;

#nullable disable

namespace TFSDataAccessLayer.Models
{
    public partial class FileType
    {
        public FileType()
        {
            FileTypeCategories = new HashSet<FileTypeCategory>();
        }

        public int FileTypeId { get; set; }
        public string FileTypeDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }

        public virtual ICollection<FileTypeCategory> FileTypeCategories { get; set; }
    }
}
