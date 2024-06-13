using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace AssetManagement.Application.Services
{
    public class AccountService : IAccountServicecs
    {
        private readonly IUserRepositoriesAsync _userRepositoriesAsync;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public AccountService(IUserRepositoriesAsync userRepositoriesAsync, ITokenService tokenService)
        {
            _userRepositoriesAsync = userRepositoriesAsync;
            _tokenService = tokenService;
        }

        public async Task<Response<AuthenticationResponse>> LoginAsync(AuthenticationRequest request)
        {
            var user = await _userRepositoriesAsync.FindByUsernameAsync(request.Username);

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (user == null || verificationResult != PasswordVerificationResult.Success)
            {
                return new Response<AuthenticationResponse> { Succeeded = false, Message = "Invalid username or password" };
            }
            if (!user.IsFirstTimeLogin)
            {
                return new Response<AuthenticationResponse>
                {
                    Succeeded = false,
                    Message = "You need to change your password before login"
                };
            }

            var role = await _userRepositoriesAsync.GetRoleAsync(user.Id);
            var token = _tokenService.GenerateJwtToken(user, role);
            var response = new AuthenticationResponse
            {
                Username = user.Username,
                Role = role.ToString(),
                Token = token
            };
            return new Response<AuthenticationResponse>
            {
                Succeeded = true,
                Data = response
            };
        }
    }
}