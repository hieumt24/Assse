using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Application.Wrappers;

namespace AssetManagement.Application.Interfaces
{
    public interface IAccountServicecs
    {
        Task<Response<AuthenticationResponse>> LoginAsync(AuthenticationRequest request);
    }
}