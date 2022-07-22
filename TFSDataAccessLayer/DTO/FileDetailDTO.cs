using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSDataAccessLayer.DTO
{
    public class FileDetailDTO
    {
        public int FileDetailsId { get; set; }
        public string FileName { get; set; }
        public int FileTypeCategoryId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? Dated { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public string FileLocation { get; set; }
        public byte[] UploadedFile { get; set; }
        public bool IsArchived { get; set; }
        public string Month { get; set; }
        public string folderUrl { get; set; }

    }
}
