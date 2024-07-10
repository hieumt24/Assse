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
        private readonly Mock<IPasswordHasher<User>> _mockPasswordHasher;
        public AccountServiceTests()
        {
            _userRepositoriesMock = new Mock<IUserRepositoriesAsync>();
            _tokenServiceMock = new Mock<ITokenService>();
            _changePasswordValidatorMock = new Mock<IValidator<ChangePasswordRequest>>();
            _tokenRepositoriesMock = new Mock<ITokenRepositoriesAsync>();
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
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
        public async Task ChangePasswordAsync_IncorrectCurrentPassword_ReturnsFailureResponse()
        {
            // Arrange
            var request = new ChangePasswordRequest
            {
                Username = "testuser",
                CurrentPassword = "incorrectpassword",
                NewPassword = "newpassword"
            };

            // Create a valid password hash
            var correctPassword = "correctpassword";
            var user = new User
            {
                Username = "testuser",
                PasswordHash = _passwordHasher.HashPassword(null, correctPassword),
                IsFirstTimeLogin = false
            };

            _changePasswordValidatorMock.Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _userRepositoriesMock.Setup(r => r.FindByUsernameAsync(request.Username))
                .ReturnsAsync(user);

            // Act
            var result = await _accountService.ChangePasswordAsync(request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Current password is incorrect", result.Message);
            _userRepositoriesMock.Verify(r => r.UpdateUserAysnc(It.IsAny<User>()), Times.Never);
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
        public async Task ChangePasswordAsync_NewPasswordSameAsCurrent_ReturnsFailureResponse()
        {
            // Arrange
            var currentPassword = "currentpassword";
            var request = new ChangePasswordRequest
            {
                Username = "testuser",
                CurrentPassword = currentPassword,
                NewPassword = currentPassword  // New password same as current
            };

            var user = new User
            {
                Username = "testuser",
                PasswordHash = _passwordHasher.HashPassword(null, currentPassword),
                IsFirstTimeLogin = false
            };

            _changePasswordValidatorMock.Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _userRepositoriesMock.Setup(r => r.FindByUsernameAsync(request.Username))
                .ReturnsAsync(user);

            // Act
            var result = await _accountService.ChangePasswordAsync(request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("New password cannot be the same as the current password", result.Message);
            _userRepositoriesMock.Verify(r => r.UpdateUserAysnc(It.IsAny<User>()), Times.Never);
        }

    }
}
