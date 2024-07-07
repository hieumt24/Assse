using AssetManagement.Application.Common;
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
        private readonly IValidator<EditAssetRequestDto> _editAssetValidator;
        private readonly IAssignmentRepositoriesAsync _assignmentRepository;

        public AssetServiceAsync(IAssetRepositoriesAsync assetRepository,
             IMapper mapper,
              IUriService uriService,
             IValidator<AddAssetRequestDto> addAssetValidator,
             IValidator<EditAssetRequestDto> editAssetValidator,
             IAssignmentRepositoriesAsync assignmentRepository
            )
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
            _uriService = uriService;
            _addAssetValidator = addAssetValidator;
            _editAssetValidator = editAssetValidator;
            _assignmentRepository = assignmentRepository;
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
                newAsset.LastModifiedOn = DateTime.Now;

                newAsset.Specification = string.Join(" ", request.Specification.Split(' ', StringSplitOptions.RemoveEmptyEntries));

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
            var exsitingAssignment = _assignmentRepository.FindExitingAssignment(assetId);
            if (exsitingAssignment != null)
            {
                return new Response<AssetDto> { Succeeded = true, Message = "This asset cannot be deleted because it is being assigned in assignment." };
            }
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

        public async Task<PagedResponse<List<AssetResponseDto>>> GetAllAseets(PaginationFilter pagination, string? search, Guid? categoryId, ICollection<AssetStateType?>? assetStateType, EnumLocation adminLocation, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                if (pagination == null)
                {
                    pagination = new PaginationFilter();
                }
                //var filterAsset = await _assetRepository.FilterAsset(adminLocation, search, categoryId, assetStateType);

                //var totalRecords = await filterAsset.CountAsync();
                //var specAsset = AssetSpecificationHelper.AssetSpecificationWithCategory(pagination, orderBy, isDescending);

                //var assets = await SpecificationEvaluator<Asset>.GetQuery(filterAsset, specAsset).ToListAsync();

                var assets = await _assetRepository.GetAllMatchingAssetAsync(adminLocation, search, categoryId, assetStateType, orderBy, isDescending, pagination);

                //Map to asset reponse
                var responseAssetDtos = _mapper.Map<List<AssetResponseDto>>(assets.Data);

                var pagedResponse = PaginationHelper.CreatePagedReponse(responseAssetDtos, pagination, assets.TotalRecords, _uriService, route);
                return pagedResponse;
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<AssetResponseDto>> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<AssetDto>> EditAssetAsync(Guid assetId, EditAssetRequestDto request)
        {
            try
            {
                //Map Dto to Domain
                var assetDomain = _mapper.Map<Asset>(request);

                //Check asset is exist
                var exsitingAsset = await _assetRepository.GetByIdAsync(assetId);
                if (exsitingAsset == null)
                {
                    return new Response<AssetDto> { Succeeded = false, Message = "Asset not found" };
                }

                //Check validation
                var validationResult = await _editAssetValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return new Response<AssetDto> { Succeeded = false, Errors = errors };
                }
                exsitingAsset.AssetName = assetDomain.AssetName;
                exsitingAsset.Specification = assetDomain.Specification;
                exsitingAsset.InstalledDate = assetDomain.InstalledDate;
                exsitingAsset.State = assetDomain.State;
                exsitingAsset.LastModifiedOn = DateTime.Now;

                //Update to database
                await _assetRepository.UpdateAsync(exsitingAsset);

                //Map to Dto
                var assetEditDto = _mapper.Map<AssetDto>(exsitingAsset);
                return new Response<AssetDto> { Succeeded = true, Data = assetEditDto };
            }
            catch (Exception ex)
            {
                return new Response<AssetDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<AssetDto>> GetAssetByAssetCode(string assetCode)
        {
            try
            {
                var exsittingAsset = AssetSpecificationHelper.GetAssetByAssetCode(assetCode);
                if (exsittingAsset == null)
                {
                    return new Response<AssetDto>("User not found");
                }
                var asset = await _assetRepository.FirstOrDefaultAsync(exsittingAsset);
                var userDto = _mapper.Map<AssetDto>(asset);
                return new Response<AssetDto> { Succeeded = true, Data = userDto };
            }
            catch (Exception ex)
            {
                return new Response<AssetDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<bool>> IsValidDeleteAsset(Guid assetId)
        {
            try
            {
                var exsitingAsset = await _assetRepository.GetByIdAsync(assetId);
                if (exsitingAsset == null)
                {
                    return new Response<bool> { Succeeded = false, Message = "Asset not found" };
                }
                var assignment = await _assignmentRepository.GetAssignmentsByAssetId(exsitingAsset.Id);
                if (assignment.Any())
                {
                    return new Response<bool> { Succeeded = false, Message = "There are valid assignments belonging to this user. Please close all assignments before disabling user." };
                }
                return new Response<bool> { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new Response<bool> { Succeeded = false, Errors = { ex.Message } };
            }
        }
    }
}