using AssetManagement.Application.Common;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Assets.Responses;
using AssetManagement.Application.Models.DTOs.Category;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Validations.Asset;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Application.Services
{
    public class AssetServiceAsync : IAssetServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssetRepositoriesAsync _assetRepository;
        private readonly IUriService _uriService;
        private readonly IValidator<AddAssetRequestDto> _addAssetValidator;

        public AssetServiceAsync(IAssetRepositoriesAsync assetRepository,
             IMapper mapper,
              IUriService uriService,
             IValidator<AddAssetRequestDto> addAssetValidator)
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
            _uriService = uriService;
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

                return new Response<AssetDto> { Succeeded = true, Message = "Create New Asset Successfully." };
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

        public async Task<PagedResponse<List<AssetResponseDto>>> GetAllAseets(PaginationFilter pagination, string? search, Guid? categoryId, AssetStateType? assetStateType, EnumLocation enumLocation, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                var assetQuery = AssetSpecificationHelper.CreateAssetQuery(search, categoryId, assetStateType, enumLocation, orderBy, isDescending);

                var query = SpecificationEvaluator<Asset>.GetQuery(_assetRepository.Query(), assetQuery);
                var totalRecords = await query.CountAsync();

                var assetPaginationSpec = AssetSpecificationHelper.CreateAssetPagination(assetQuery, pagination);
                var paginatedQuery = SpecificationEvaluator<Asset>.GetQuery(query, assetPaginationSpec);

                var assets = await paginatedQuery.ToListAsync();

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