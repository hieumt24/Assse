using AssetManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Models.DTOs.Assets
{
    public class AssetDto
    {
        public Guid Id { get; set; }
        public string AssetCode { get; set; } = string.Empty;
        public string AssetName { get; set; } = string.Empty;
        public string Specification { get; set; } = string.Empty;
        public DateTime InstalledDate { get; set; }
        public bool State { get; set; } = true;
        public EnumLocation AssetLocation { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
