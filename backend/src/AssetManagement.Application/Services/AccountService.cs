using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using Azure;

namespace AssetManagement.Application.Services
{
    public class AccountService : IAccountServicecs
    {
        public Task<Response<AuthenticationResponse>> LoginAsync(AuthenticationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}