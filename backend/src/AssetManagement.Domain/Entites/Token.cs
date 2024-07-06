﻿using AssetManagement.Domain.Common.Models;

namespace AssetManagement.Domain.Entites
{
    public class Token : BaseEntity
    {
        public Guid UserId { get; set; }
        public string? Value { get; set; }
        public User? User { get; set; }

    }
}
