using System;
using System.Collections.Generic;

#nullable disable

namespace TFSDataAccessLayer.Models
{
    public partial class FileTypeCategory
    {
        public FileTypeCategory()
        {
            FileDetails = new HashSet<FileDetail>();
        }

        public int FileTypeCategoryId { get; set; }
        public string FileTypeCategoryDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public int FileTypeId { get; set; }
        public int DepartmentId { get; set; }

        public virtual FileType FileType { get; set; }
        public virtual ICollection<FileDetail> FileDetails { get; set; }
    }
}
