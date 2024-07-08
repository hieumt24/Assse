using AssetManagement.Application.Filter;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Assets.Responses;
using AssetManagement.Application.Services;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;
using Moq;

namespace AssetManagement.Application.Tests.Services
{
    public class AssetServiceTests
    {
        private readonly Mock<IAssetRepositoriesAsync> _assetRepositoryMock;
        private readonly Mock<IUriService> _uriServiceMock;
        private readonly Mock<IValidator<AddAssetRequestDto>> _addAssetValidatorMock;
        private readonly Mock<IValidator<EditAssetRequestDto>> _editAssetValidatorMock;
        private readonly Mock<IAssignmentRepositoriesAsync> _assignmentRepositoryMock;
        private readonly IMapper _mapper;
        private readonly AssetService _assetService;

        public AssetServiceTests()
        {
            _assetRepositoryMock = new Mock<IAssetRepositoriesAsync>();
            _uriServiceMock = new Mock<IUriService>();
            _addAssetValidatorMock = new Mock<IValidator<AddAssetRequestDto>>();
            _editAssetValidatorMock = new Mock<IValidator<EditAssetRequestDto>>();
            _assignmentRepositoryMock = new Mock<IAssignmentRepositoriesAsync>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddAssetRequestDto, Asset>();
                cfg.CreateMap<Asset, AssetDto>();
                cfg.CreateMap<Asset, AssetResponseDto>();
                cfg.CreateMap<EditAssetRequestDto, Asset>();
            });

            _mapper = config.CreateMapper();

            _assetService = new AssetService(
                _assetRepositoryMock.Object,
                _mapper,
                _uriServiceMock.Object,
                _addAssetValidatorMock.Object,
                _editAssetValidatorMock.Object,
                _assignmentRepositoryMock.Object
            );
        }

        [Fact]
        public async Task AddAssetAsync_InvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            var request = new AddAssetRequestDto();
            var validationResult = new FluentValidation.Results.ValidationResult(new[]
            {
                new FluentValidation.Results.ValidationFailure("AssetName", "Asset name is required")
            });

            _addAssetValidatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _assetService.AddAssetAsync(request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("Asset name is required", result.Errors);
        }

        //[Fact]
        //public async Task AddAssetAsync_ValidRequest_ReturnsSuccessResponse()
        //{
        //    // Arrange
        //    var request = new AddAssetRequestDto
        //    {
        //        AdminId = Guid.NewGuid().ToString(),
        //        AssetName = "Test Asset",
        //        CategoryId = Guid.NewGuid(),
        //        Specification = "Spec1 Spec2"
        //    };

        //    var asset = _mapper.Map<Asset>(request);
        //    asset.Id = Guid.NewGuid();
        //    asset.AssetCode = "ASSET123";

        //    _addAssetValidatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
        //    _assetRepositoryMock.Setup(r => r.GenerateAssetCodeAsync(request.CategoryId)).ReturnsAsync("ASSET123");
        //    _assetRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Asset>())).ReturnsAsync(asset);

        //    // Act
        //    var result = await _assetService.AddAssetAsync(request);

        //    // Assert
        //    Assert.True(result.Succeeded);
        //    Assert.Equal("Create New Asset Successfully.", result.Message);
        //    _assetRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Asset>()), Times.Once);
        //}

        [Fact]
        public async Task DeleteAssetAsync_AssetInUse_ReturnsErrorResponse()
        {
            // Arrange
            var assetId = Guid.NewGuid();

            _assignmentRepositoryMock.Setup(r => r.FindExitingAssignment(assetId)).ReturnsAsync(new Assignment());

            // Act
            var result = await _assetService.DeleteAssetAsync(assetId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("This asset cannot be deleted because it is being assigned in assignment.", result.Message);
        }

        [Fact]
        public async Task DeleteAssetAsync_AssetNotFound_ReturnsErrorResponse()
        {
            // Arrange
            var assetId = Guid.NewGuid();

            _assignmentRepositoryMock.Setup(r => r.FindExitingAssignment(assetId)).ReturnsAsync((Assignment)null);
            _assetRepositoryMock.Setup(r => r.DeleteAsync(assetId)).ReturnsAsync((Asset)null);

            // Act
            var result = await _assetService.DeleteAssetAsync(assetId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Asset not found.", result.Message);
        }

        [Fact]
        public async Task DeleteAssetAsync_Success_ReturnsSuccessResponse()
        {
            // Arrange
            var assetId = Guid.NewGuid();
            var asset = new Asset { Id = assetId };

            _assignmentRepositoryMock.Setup(r => r.FindExitingAssignment(assetId)).ReturnsAsync((Assignment)null);
            _assetRepositoryMock.Setup(r => r.DeleteAsync(assetId)).ReturnsAsync(asset);

            // Act
            var result = await _assetService.DeleteAssetAsync(assetId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal("Delete asset successfully!", result.Message);
        }

        [Fact]
        public async Task GetAssetByIdAsync_AssetNotFound_ReturnsErrorResponse()
        {
            // Arrange
            var assetId = Guid.NewGuid();

            _assetRepositoryMock.Setup(r => r.GetByIdAsync(assetId)).ReturnsAsync((Asset)null);

            // Act
            var result = await _assetService.GetAssetByIdAsync(assetId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Category not found.", result.Message);
        }

        [Fact]
        public async Task GetAssetByIdAsync_Success_ReturnsAssetDto()
        {
            // Arrange
            var assetId = Guid.NewGuid();
            var asset = new Asset { Id = assetId, AssetName = "Test Asset" };

            _assetRepositoryMock.Setup(r => r.GetByIdAsync(assetId)).ReturnsAsync(asset);

            // Act
            var result = await _assetService.GetAssetByIdAsync(assetId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal("Test Asset", result.Data.AssetName);
        }

        [Fact]
        public async Task EditAssetAsync_InvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            var assetId = Guid.NewGuid();
            var request = new EditAssetRequestDto();
            var validationResult = new FluentValidation.Results.ValidationResult(new[]
            {
                new FluentValidation.Results.ValidationFailure("AssetName", "Asset name is required")
            });

            _editAssetValidatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);
            _assetRepositoryMock.Setup(r => r.GetByIdAsync(assetId)).ReturnsAsync(new Asset());

            // Act
            var result = await _assetService.EditAssetAsync(assetId, request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("Asset name is required", result.Errors);
        }

        [Fact]
        public async Task EditAssetAsync_AssetNotFound_ReturnsErrorResponse()
        {
            // Arrange
            var assetId = Guid.NewGuid();
            var request = new EditAssetRequestDto();

            _editAssetValidatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _assetRepositoryMock.Setup(r => r.GetByIdAsync(assetId)).ReturnsAsync((Asset)null);

            // Act
            var result = await _assetService.EditAssetAsync(assetId, request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("Asset not found", result.Message);
        }

        [Fact]
        public async Task EditAssetAsync_Success_ReturnsSuccessResponse()
        {
            // Arrange
            var assetId = Guid.NewGuid();
            var request = new EditAssetRequestDto
            {
                AssetName = "Updated Asset",
                Specification = "Updated Spec",
                InstalledDate = DateTime.Now,
                State = AssetStateType.Available
            };
            var asset = new Asset { Id = assetId, AssetName = "Old Asset" };

            _editAssetValidatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _assetRepositoryMock.Setup(r => r.GetByIdAsync(assetId)).ReturnsAsync(asset);

            // Act
            var result = await _assetService.EditAssetAsync(assetId, request);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal("Updated Asset", result.Data.AssetName);
            _assetRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Asset>()), Times.Once);
        }

        //[Fact]
        //public async Task GetAllAseets_Success_ReturnsPagedResponse()
        //{
        //    // Arrange
        //    var pagination = new PaginationFilter { PageIndex = 1, PageSize = 10 };
        //    var assets = new List<Asset>
        //    {
        //        new Asset { Id = Guid.NewGuid(), AssetName = "Asset1" },
        //        new Asset { Id = Guid.NewGuid(), AssetName = "Asset2" }
        //    }.AsQueryable();

        //    _assetRepositoryMock.Setup(r => r.FilterAsset(It.IsAny<EnumLocation>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<AssetStateType>(), It.IsAny<Guid?>())).Returns(assets);

        //    // Act
        //    var result = await _assetService.GetAllAssets(pagination, EnumLocation.HaNoi, "", "", AssetStateType.Available, null);

        //    // Assert
        //    Assert.True(result.Succeeded);
        //    Assert.Equal(2, result.Data.TotalRecords);
        //    Assert.Equal("Asset1", result.Data.Data.First().AssetName);
        //}
    }
}
