using AssetManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Application.Models.DTOs.Assets.Requests
{
    public class EditAssetRequestDto
    {
        public string AssetName { get; set; }
        public string Specification { get; set; }

        [DataType(DataType.Date)]
        public DateTime InstalledDate { get; set; }

        public AssetStateType State { get; set; }
    }
}