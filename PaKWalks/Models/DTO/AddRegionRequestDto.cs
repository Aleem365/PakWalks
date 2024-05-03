using System.ComponentModel.DataAnnotations;

namespace PaKWalks.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 charaacters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 charaacters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Code has to be a maximum of 50 charaacters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
