using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSDataAccessLayer.DTO
{
   public class FileTypeCategoryDTO
    {
        public int FileTypeCategoryId { get; set; }
        public string FileTypeCategoryDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public int FileTypeId { get; set; }
        public string folderUrl { get; set; }
        public int DepartmentId { get; set; }

    }
}
