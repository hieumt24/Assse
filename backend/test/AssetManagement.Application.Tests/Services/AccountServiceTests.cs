using System.Threading.Tasks;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Application.Services;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace AssetManagement.Application.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IUserRepositoriesAsync> _userRepositoriesMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IValidator<ChangePasswordRequest>> _changePasswordValidatorMock;
        private readonly Mock<ITokenRepositoriesAsync> _tokenRepositoriesMock;
        private readonly AccountService _accountService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AccountServiceTests()
        {
            _userRepositoriesMock = new Mock<IUserRepositoriesAsync>();
            _tokenServiceMock = new Mock<ITokenService>();
            _changePasswordValidatorMock = new Mock<IValidator<ChangePasswordRequest>>();
            _tokenRepositoriesMock = new Mock<ITokenRepositoriesAsync>();
            _passwordHasher = new PasswordHasher<User>();

            _accountService = new AccountService(
                _userRepositoriesMock.Object,
                _tokenServiceMock.Object,
                _changePasswordValidatorMock.Object,
                _tokenRepositoriesMock.Object);
        }

        [Fact]
        public async Task ChangePasswordAsync_InvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            var request = new ChangePasswordRequest();
            var validationResult = new FluentValidation.Results.ValidationResult(new[]
            {
                new FluentValidation.Results.ValidationFailure("Password", "Password is required")
            });

            _changePasswordValidatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _accountService.ChangePasswordAsync(request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("Password is required", result.Errors);
        }

        [Fact]
        public async Task ChangePasswordAsync_CurrentPasswordIsIncorrect_ReturnsErrorResponse()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                Username = "testuser",
                CurrentPassword = "wrongpassword",
                NewPassword = "newpassword"
            };
            var user = new User { Username = "testuser", PasswordHash = _passwordHasher.HashPassword(null, "correctpassword") };

            _changePasswordValidatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _userRepositoriesMock.Setup(r => r.FindByUsernameAsync(request.Username)).ReturnsAsync(user);

            // Act
            var result = await _accountService.ChangePasswordAsync(request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Current password is incorrect", result.Message);
        }

        [Fact]
        public async Task ChangePasswordAsync_Success_ReturnsSuccessResponse()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                Username = "testuser",
                CurrentPassword = "correctpassword",
                NewPassword = "newpassword"
            };
            var user = new User { Username = "testuser", PasswordHash = _passwordHasher.HashPassword(null, "correctpassword") };

            _changePasswordValidatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _userRepositoriesMock.Setup(r => r.FindByUsernameAsync(request.Username)).ReturnsAsync(user);

            // Act
            var result = await _accountService.ChangePasswordAsync(request);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal("Password changed successfully", result.Message);
            _userRepositoriesMock.Verify(r => r.UpdateUserAysnc(user), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_InvalidUsernameOrPassword_ReturnsErrorResponse()
        {
            // Arrange
            var request = new AuthenticationRequest { Username = "testuser", Password = "wrongpassword" };

            _userRepositoriesMock.Setup(r => r.FindByUsernameAsync(request.Username)).ReturnsAsync((User)null);

            // Act
            var result = await _accountService.LoginAsync(request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Invalid username or password", result.Message);
        }

        [Fact]
        public async Task LoginAsync_Success_ReturnsSuccessResponse()
        {
            // Arrange
            var request = new AuthenticationRequest { Username = "testuser", Password = "correctpassword" };
            var user = new User { Username = "testuser", PasswordHash = _passwordHasher.HashPassword(null, "correctpassword") };
            var token = "generated_jwt_token";

            _userRepositoriesMock.Setup(r => r.FindByUsernameAsync(request.Username)).ReturnsAsync(user);
            _userRepositoriesMock.Setup(r => r.GetRoleAsync(user.Id)).ReturnsAsync(RoleType.Staff); 
            _tokenServiceMock.Setup(t => t.GenerateJwtToken(user, RoleType.Staff)).Returns(token);

            // Act
            var result = await _accountService.LoginAsync(request);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal(user.Username, result.Data.Username);
            Assert.Equal(RoleType.Staff.ToString(), result.Data.Role);
            Assert.Equal(token, result.Data.Token);
        }
    }
}
