using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AutoMapper;

namespace AssetManagement.Application.Services
{
    public class AssetServiceAsync : IAssetServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssetRepositoriesAsync _assetRepository;

        public AssetServiceAsync(IAssetRepositoriesAsync assetRepository,
             IMapper mapper)
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
        }

        public async Task<Response<AssetDto>> AddAssetAsync(AddAssetRequestDto request)
        {
            try
            {
                var newAsset = _mapper.Map<Asset>(request);
                newAsset.AssetCode = await _assetRepository.GenerateAssetCodeAsync(newAsset.CategoryId);

                newAsset.CreatedBy = request.AdminId;
                newAsset.CreatedOn = DateTime.Now;
                var asset = await _assetRepository.AddAsync(newAsset);

                var assetDto = _mapper.Map<AssetDto>(asset);

                return new Response<AssetDto>();
            }
            catch (Exception ex)
            {
                return new Response<AssetDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }
    }
}