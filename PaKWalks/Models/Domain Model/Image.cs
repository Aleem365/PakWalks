using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_OverView.Models.Domain_Model
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped] 
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string FileDescription { get; set; } = string.Empty;

        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }

    }
}
