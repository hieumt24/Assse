using AssetManagement.Application.Common;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assignments;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Models.DTOs.Assignments.Response;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Application.Services
{
    public class AssignmentServiceAsync : IAssignmentServicesAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentRepositoriesAsync _assignmentRepositoriesAsync;
        private readonly IUriService _uriService;
        private readonly IValidator<AddAssignmentRequestDto> _addAssignmentValidator;
        private readonly IAssignmentRepositoriesAsync _assignmentRepository;
        private readonly IUserRepositoriesAsync _userRepository;
        private readonly IAssetRepositoriesAsync _assetRepository;

        public AssignmentServiceAsync(IAssignmentRepositoriesAsync assignmentRepositoriesAsync,
             IMapper mapper,
             IValidator<AddAssignmentRequestDto> addAssignmentValidator,
             IAssetRepositoriesAsync assetRepository,
             IUserRepositoriesAsync userRepository,
             IUriService uriService
            )
        {
            _mapper = mapper;
            _assignmentRepositoriesAsync = assignmentRepositoriesAsync;
            _uriService = uriService;
            _assignmentRepository = assignmentRepositoriesAsync;
            _addAssignmentValidator = addAssignmentValidator;
            _assetRepository = assetRepository;
            _userRepository = userRepository;
            _uriService = uriService;
        }

        public async Task<Response<AssignmentDto>> AddAssignmentAsync(AddAssignmentRequestDto request)
        {
            //validate data
            var validationResult = await _addAssignmentValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new Response<AssignmentDto>
                {
                    Succeeded = false,
                    Errors = errors
                };
            }

            //check null
            var existingAsset = await _assetRepository.GetByIdAsync(request.AssetId);
            if (existingAsset == null)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Asset not found." };
            }

            var existingAssignedIdBy = await _userRepository.GetByIdAsync(request.AssignedIdBy);
            if (existingAssignedIdBy == null)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "User assigned by not found." };

            if(existingAssignedIdBy.JoinedDate > request.AssignedDate) {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Assigned Date must be greater than Joined Date." };
            }

            try
            {
                var newAssigment = _mapper.Map<Assignment>(request);
                newAssigment.CreatedOn = DateTime.Now;
                newAssigment.CreatedBy = request.AssignedIdBy.ToString();
                var asignment = await _assignmentRepository.AddAsync(newAssigment);

                var assetDto = _mapper.Map<AssignmentDto>(asignment);

                return new Response<AssignmentDto> { Succeeded = true, Message = " Create Assignment Successfully!" };
            }
            catch (Exception ex)
            {
                return new Response<AssignmentDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public Task<Response<AssignmentDto>> DeleteAssignmentAsync(Guid assignmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<AssignmentDto>> EditAssignmentAsync(EditAssignmentRequestDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<AssignmentResponseDto>>> GetAllAssignmentsAsync(PaginationFilter paginationFilter, string? search, EnumAssignmentStatus? assignmentStatus, DateTime? assignedDate, EnumLocation adminLocation, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                if (paginationFilter == null)
                {
                    paginationFilter = new PaginationFilter();
                }

                var filterAsset = await _assignmentRepositoriesAsync.FilterAssignmentAsync(adminLocation, search, assignmentStatus, assignedDate);

                var totalRecords = filterAsset.Count();

                var specAssignment = AssignmentSpecificationHelper.AssignmentSpecificationWithAsset(paginationFilter, orderBy, isDescending);

                var assignments = await SpecificationEvaluator<Assignment>.GetQuery(filterAsset, specAssignment).ToListAsync();

                var responseAssignmentDtos = _mapper.Map<List<AssignmentResponseDto>>(assignments);

                var pagedResponse = PaginationHelper.CreatePagedReponse(responseAssignmentDtos, paginationFilter, totalRecords, _uriService, route);

                return pagedResponse;
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<AssignmentResponseDto>> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public Task<Response<AssignmentDto>> GetAssignmentByIdAsync(Guid assignmentId)
        {
            throw new NotImplementedException();
        }
    }
}