using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Category;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Validations.Asset;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AutoMapper;
using FluentValidation;

namespace AssetManagement.Application.Services
{
    public class AssetServiceAsync : IAssetServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssetRepositoriesAsync _assetRepository;
        private readonly IValidator<AddAssetRequestDto> _addAssetValidator;

        public AssetServiceAsync(IAssetRepositoriesAsync assetRepository,
             IMapper mapper, IValidator<AddAssetRequestDto> addAssetValidator)
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
            _addAssetValidator = addAssetValidator;
        }

        public async Task<Response<AssetDto>> AddAssetAsync(AddAssetRequestDto request)
        {

            var validationResult = await _addAssetValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new Response<AssetDto> { Succeeded = false, Errors = errors };
            }
            try
            {
                var newAsset = _mapper.Map<Asset>(request);
                newAsset.AssetCode = await _assetRepository.GenerateAssetCodeAsync(newAsset.CategoryId);

                newAsset.CreatedBy = request.AdminId;
                newAsset.CreatedOn = DateTime.Now;
                var asset = await _assetRepository.AddAsync(newAsset);

                var assetDto = _mapper.Map<AssetDto>(asset);

                return new Response<AssetDto> { Succeeded = true, Message = "Create New Asset Successfully."};
            }
            catch (Exception ex)
            {
                return new Response<AssetDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

   

        public async Task<Response<AssetDto>> DeleteAssetAsync(Guid assetId)
        {
            try
            {
                var assset = await _assetRepository.DeleteAsync(assetId);
                if (assset == null)
                {
                    return new Response<AssetDto> { Succeeded = false, Message = "Asset not found." };
                }

                return new Response<AssetDto> { Succeeded = true, Message = "Delete asset successfully!" };
            }
            catch (Exception ex)
            {
                return new Response<AssetDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<AssetDto>> GetAssetByIdAsync(Guid assetId)
        {
            try
            {
                var asset = await _assetRepository.GetByIdAsync(assetId);
                if (asset == null)
                {
                    return new Response<AssetDto> { Succeeded = false, Message = "Category not found." };
                }

                var categoryDto = _mapper.Map<AssetDto>(asset);
                return new Response<AssetDto> { Succeeded = true, Data = categoryDto };
            }
            catch (Exception ex)
            {
                return new Response<AssetDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }
    }
}