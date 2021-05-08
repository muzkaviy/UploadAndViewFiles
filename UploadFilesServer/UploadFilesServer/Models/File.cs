using System;
using System.ComponentModel.DataAnnotations;

namespace UploadFilesServer.Models
{
    public class File
    {
        [Key]
        public Guid Id { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public string FileExtension { get; set; }

        public int? FileSize { get; set; }

        public DateTime? UploadDate { get; set; }
    }
}
