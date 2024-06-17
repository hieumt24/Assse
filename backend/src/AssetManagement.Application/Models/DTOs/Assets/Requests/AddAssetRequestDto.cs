using AssetManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Application.Models.DTOs.Assets.Requests
{
    public class AddAssetRequestDto
    {
        [Required]
        public string AssetName { get; set; } = string.Empty;

        [Required]
        public string Specification { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime InstalledDate { get; set; }

        [Required]
        // true = Available and false = Not Available
        public bool State { get; set; } = true;

        [Required]
        public EnumLocation AssetLocation { get; set; }

        // relationship
        public Guid? CategoryId { get; set; }
    }
}
