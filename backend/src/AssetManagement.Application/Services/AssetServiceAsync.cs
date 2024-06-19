using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Assets.Responses;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;

namespace AssetManagement.Application.Services
{
    public class AssetServiceAsync : IAssetServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssetRepositoriesAsync _assetRepository;
        private readonly IUriService _uriService;

        public AssetServiceAsync(IAssetRepositoriesAsync assetRepository,
             IMapper mapper,
              IUriService uriService)
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
            _uriService = uriService;
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

        public async Task<PagedResponse<List<AssetResponseDto>>> GetAllAseets(PaginationFilter pagination, string? search, Guid? categoryId, AssetStateType? assetStateType, EnumLocation enumLocation, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                var assetQuery = AssetSpecificationHelper.CreateAssetQuery(search, categoryId, assetStateType, enumLocation, orderBy, isDescending);
                var totalRecords = await _assetRepository.CountAsync(assetQuery);
                var assetPagaination = AssetSpecificationHelper.CreateAssetPagination(assetQuery, pagination);
                var assets = await _assetRepository.ListAsync(assetPagaination);

                //Map to asset reponse
                var responseAssetDtos = _mapper.Map<List<AssetResponseDto>>(assets);

                var pagedResponse = PaginationHelper.CreatePagedReponse(responseAssetDtos, pagination, totalRecords, _uriService, route);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<AssetResponseDto>> { Succeeded = false, Errors = { ex.Message } };
            }
        }
    }
}