using AssetManagement.Application.Models.DTOs.ReturnRequests;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Request;
using AssetManagement.Application.Wrappers;

namespace AssetManagement.Application.Interfaces.Services
{
    public interface IReturnRequestServiceAsync
    {
        Task<Response<ReturnRequestDto>> AddReturnRequestAsync(AddReturnRequestDto request);
        Task<Response<ReturnRequestDto>> GetReturnRequestByIdAsync(Guid assignmentId);
        Task<Response<ReturnRequestDto>> EditReturnRequestAsync(EditReturnRequestDto request);
        Task<Response<ReturnRequestDto>> DeleteReturnRequestAsync(Guid assignmentId);
        Task<Response<List<ReturnRequestDto>>> GetAllReturnRequestAsync();
    }
}
