using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assignments;
using AssetManagement.Application.Models.DTOs.Assignments.Reques;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Models.DTOs.Assignments.Requests;
using AssetManagement.Application.Models.DTOs.Assignments.Response;
using AssetManagement.Application.Services;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace AssetManagement.Application.Tests.Services
{
    public class AssignmentServiceTests
    {
        private readonly Mock<IAssignmentRepositoriesAsync> _assignmentRepositoryMock;
        private readonly Mock<IUserRepositoriesAsync> _userRepositoryMock;
        private readonly Mock<IAssetRepositoriesAsync> _assetRepositoryMock;
        private readonly Mock<IUriService> _uriServiceMock;
        private readonly Mock<IValidator<AddAssignmentRequestDto>> _addAssignmentValidatorMock;
        private readonly Mock<IValidator<EditAssignmentRequestDto>> _editAssignmentValidatorMock;
        private readonly IMapper _mapper;
        private readonly AssignmentServiceAsync _assignmentService;

        public AssignmentServiceTests()
        {
            _assignmentRepositoryMock = new Mock<IAssignmentRepositoriesAsync>();
            _userRepositoryMock = new Mock<IUserRepositoriesAsync>();
            _assetRepositoryMock = new Mock<IAssetRepositoriesAsync>();
            _uriServiceMock = new Mock<IUriService>();
            _addAssignmentValidatorMock = new Mock<IValidator<AddAssignmentRequestDto>>();
            _editAssignmentValidatorMock = new Mock<IValidator<EditAssignmentRequestDto>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddAssignmentRequestDto, Assignment>();
                cfg.CreateMap<Assignment, AssignmentDto>();
                cfg.CreateMap<Assignment, AssignmentResponseDto>();
                cfg.CreateMap<EditAssignmentRequestDto, Assignment>();
            });

            _mapper = config.CreateMapper();

            _assignmentService = new AssignmentServiceAsync(
                _assignmentRepositoryMock.Object,
                _mapper,
                _addAssignmentValidatorMock.Object,
                _editAssignmentValidatorMock.Object,
                _assetRepositoryMock.Object,
                _userRepositoryMock.Object,
                _uriServiceMock.Object
            );
        }

        [Fact]
        public async Task AddAssignmentAsync_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var request = new AddAssignmentRequestDto
            {
                AssetId = Guid.NewGuid(),
                AssignedIdBy = Guid.NewGuid(),
                AssignedIdTo = Guid.NewGuid(),
                AssignedDate = DateTime.Now,
                Note = "Test Note"
            };

            var assignment = _mapper.Map<Assignment>(request);
            assignment.Id = Guid.NewGuid();

            _addAssignmentValidatorMock.Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _assetRepositoryMock.Setup(r => r.GetByIdAsync(request.AssetId))
                .ReturnsAsync(new Asset { Id = request.AssetId });

            _userRepositoryMock.Setup(r => r.GetByIdAsync(request.AssignedIdBy))
                .ReturnsAsync(new User { Id = request.AssignedIdBy, JoinedDate = DateTime.Now.AddDays(-1) });

            _assignmentRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Assignment>()))
                .ReturnsAsync(assignment);

            // Act
            var result = await _assignmentService.AddAssignmentAsync(request);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal(" Create Assignment Successfully!", result.Message);
        }

        [Fact]
        public async Task AddAssignmentAsync_InvalidRequest_ReturnsValidationErrors()
        {
            // Arrange
            var request = new AddAssignmentRequestDto
            {
                AssetId = Guid.NewGuid(),
                AssignedIdBy = Guid.NewGuid(),
                AssignedIdTo = Guid.NewGuid(),
                AssignedDate = DateTime.Now,
                Note = "Test Note"
            };

            var validationErrors = new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("AssetId", "AssetId is required.")
            };

            _addAssignmentValidatorMock.Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult(validationErrors));

            // Act
            var result = await _assignmentService.AddAssignmentAsync(request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("AssetId is required.", result.Errors);
        }

        [Fact]
        public async Task EditAssignmentAsync_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var assignmentId = Guid.NewGuid();
            var request = new EditAssignmentRequestDto
            {
                AssetId = Guid.NewGuid(),
                AssignedIdBy = Guid.NewGuid(),
                AssignedIdTo = Guid.NewGuid(),
                AssignedDate = DateTime.Now,
                Note = "Updated Note"
            };

            var existingAssignment = new Assignment
            {
                Id = assignmentId,
                AssetId = Guid.NewGuid(),
                AssignedIdBy = Guid.NewGuid(),
                AssignedIdTo = Guid.NewGuid(),
                AssignedDate = DateTime.Now,
                Note = "Original Note",
                State = EnumAssignmentState.WaitingForAcceptance
            };

            _editAssignmentValidatorMock.Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _assignmentRepositoryMock.Setup(r => r.GetByIdAsync(assignmentId))
                .ReturnsAsync(existingAssignment);

            _assetRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Asset { Id = request.AssetId });

            // Act
            var result = await _assignmentService.EditAssignmentAsync(request, assignmentId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal("Update assignment successfully.", result.Message);
        }

        [Fact]
        public async Task EditAssignmentAsync_AssignmentNotFound_ReturnsErrorResponse()
        {
            // Arrange
            var assignmentId = Guid.NewGuid();
            var request = new EditAssignmentRequestDto
            {
                AssetId = Guid.NewGuid(),
                AssignedIdBy = Guid.NewGuid(),
                AssignedIdTo = Guid.NewGuid(),
                AssignedDate = DateTime.Now,
                Note = "Updated Note"
            };

            _editAssignmentValidatorMock.Setup(v => v.ValidateAsync(request, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _assignmentRepositoryMock.Setup(r => r.GetByIdAsync(assignmentId))
                .ReturnsAsync((Assignment)null);

            // Act
            var result = await _assignmentService.EditAssignmentAsync(request, assignmentId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Assignment not found.", result.Message);
        }

        [Fact]
        public async Task GetAssignmentByIdAsync_AssignmentExists_ReturnsAssignment()
        {
            // Arrange
            var assignmentId = Guid.NewGuid();
            var existingAssignment = new Assignment
            {
                Id = assignmentId,
                AssetId = Guid.NewGuid(),
                AssignedIdBy = Guid.NewGuid(),
                AssignedIdTo = Guid.NewGuid(),
                AssignedDate = DateTime.Now,
                Note = "Test Note",
                State = EnumAssignmentState.WaitingForAcceptance
            };

            _assignmentRepositoryMock.Setup(r => r.GetAssignemntByIdAsync(assignmentId))
                .ReturnsAsync(existingAssignment);

            // Act
            var result = await _assignmentService.GetAssignmentByIdAsync(assignmentId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal(existingAssignment.Note, result.Data.Note);
        }

        [Fact]
        public async Task GetAssignmentByIdAsync_AssignmentNotFound_ReturnsErrorResponse()
        {
            // Arrange
            var assignmentId = Guid.NewGuid();

            _assignmentRepositoryMock.Setup(r => r.GetAssignemntByIdAsync(assignmentId))
                .ReturnsAsync((Assignment)null);

            // Act
            var result = await _assignmentService.GetAssignmentByIdAsync(assignmentId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Assignment not found", result.Message);
        }

        [Fact]
        public async Task DeleteAssignmentAsync_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var assignmentId = Guid.NewGuid();
            var existingAssignment = new Assignment
            {
                Id = assignmentId,
                AssetId = Guid.NewGuid(),
                State = EnumAssignmentState.WaitingForAcceptance
            };

            var existingAsset = new Asset
            {
                Id = existingAssignment.AssetId,
                State = AssetStateType.Assigned
            };

            _assignmentRepositoryMock.Setup(r => r.GetAssignemntByIdAsync(assignmentId))
                .ReturnsAsync(existingAssignment);

            _assetRepositoryMock.Setup(r => r.GetByIdAsync(existingAssignment.AssetId))
                .ReturnsAsync(existingAsset);

            //_assignmentRepositoryMock.Setup(r => r.DeleteAsync(assignmentId))
            //    .Returns(Task.CompletedTask);

            // Act
            var result = await _assignmentService.DeleteAssignmentAsync(assignmentId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal("Assignment deleted successfully.", result.Message);
        }

        [Fact]
        public async Task DeleteAssignmentAsync_AssignmentNotFound_ReturnsErrorResponse()
        {
            // Arrange
            var assignmentId = Guid.NewGuid();

            _assignmentRepositoryMock.Setup(r => r.GetAssignemntByIdAsync(assignmentId))
                .ReturnsAsync((Assignment)null);

            // Act
            var result = await _assignmentService.DeleteAssignmentAsync(assignmentId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Assignment cannot be found.", result.Message);
        }

        // Additional test methods can be added here as needed.
    }
}

    