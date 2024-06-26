﻿using AssetManagement.Application.Filter;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Reponses;
using AssetManagement.Application.Models.DTOs.ReturnRequests;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Request;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Interfaces.Services
{
    public interface IReturnRequestServiceAsync
    {
        Task<Response<ReturnRequestDto>> AddReturnRequestAsync(AddReturnRequestDto request);

        Task<Response<ReturnRequestDto>> GetReturnRequestByIdAsync(Guid assignmentId);

        Task<Response<ReturnRequestDto>> EditReturnRequestAsync(EditReturnRequestDto request);

        Task<Response<ReturnRequestDto>> DeleteReturnRequestAsync(Guid assignmentId);

        Task<PagedResponse<List<ReturnRequestResponseDto>>> GetAllReturnRequestAsync(PaginationFilter pagination, string? search, EnumReturnRequestStatus? status, DateTime? returnDate, EnumLocation location, string? orderBy, bool? isDescending, string? route);
    }
}