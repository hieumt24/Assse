using AssetManagement.Application.Common;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assignments;
using AssetManagement.Application.Models.DTOs.Assignments.Reques;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Models.DTOs.Assignments.Requests;
using AssetManagement.Application.Models.DTOs.Assignments.Response;
using AssetManagement.Application.Models.DTOs.Category;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

namespace AssetManagement.Application.Services
{
    public class AssignmentServiceAsync : IAssignmentServicesAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentRepositoriesAsync _assignmentRepositoriesAsync;
        private readonly IUriService _uriService;
        private readonly IValidator<AddAssignmentRequestDto> _addAssignmentValidator;
        private readonly IValidator<EditAssignmentRequestDto> _editAssignmentValidator;

        private readonly IAssignmentRepositoriesAsync _assignmentRepository;
        private readonly IUserRepositoriesAsync _userRepository;
        private readonly IAssetRepositoriesAsync _assetRepository;

        public AssignmentServiceAsync(IAssignmentRepositoriesAsync assignmentRepositoriesAsync,
             IMapper mapper,
             IValidator<AddAssignmentRequestDto> addAssignmentValidator,
             IValidator<EditAssignmentRequestDto> editAssignmentValidator,
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
            _editAssignmentValidator = editAssignmentValidator;
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
            }
            if (existingAssignedIdBy.JoinedDate > request.AssignedDate)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Assigned Date must be greater than Joined Date." };
            }

            try
            {
                var newAssignment = _mapper.Map<Assignment>(request);
                newAssignment.CreatedOn = DateTime.Now;
                newAssignment.CreatedBy = request.AssignedIdBy.ToString();
                newAssignment.Note = request.Note.Trim();
                var asignment = await _assignmentRepository.AddAsync(newAssignment);
                existingAsset.State = AssetStateType.Assigned;
                await _assetRepository.UpdateAsync(existingAsset);
                var assetDto = _mapper.Map<AssignmentDto>(asignment);

                return new Response<AssignmentDto> { Succeeded = true, Message = " Create Assignment Successfully!" };
            }
            catch (Exception ex)
            {
                return new Response<AssignmentDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<AssignmentDto>> EditAssignmentAsync(EditAssignmentRequestDto request, Guid assignmentId)
        {
            var validationResult = await _editAssignmentValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new Response<AssignmentDto>
                {
                    Succeeded = false,
                    Errors = errors
                };
            }

            var existingAssignment = await _assignmentRepositoriesAsync.GetByIdAsync(assignmentId);
            if (existingAssignment == null)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Assignment not found." };
            }
            existingAssignment.AssetId = request.AssetId;
            existingAssignment.AssignedIdTo = request.AssignedIdTo;
            existingAssignment.AssignedIdBy = request.AssignedIdBy;
            existingAssignment.AssignedDate = request.AssignedDate;
            existingAssignment.Note = request.Note;

            await _assignmentRepository.UpdateAsync(existingAssignment);
            
            var updateAssignment = _mapper.Map<AssignmentDto>(existingAssignment);
            return new Response<AssignmentDto> { Succeeded = true, Message = "Update assignment successfully." };   
        }

        public async Task<PagedResponse<List<AssignmentResponseDto>>> GetAllAssignmentsAsync(PaginationFilter paginationFilter, string? search, EnumAssignmentState? assignmentState, DateTime? assignedDate, EnumLocation location, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                if (paginationFilter == null)
                {
                    paginationFilter = new PaginationFilter();
                }

                var filterAsset = await _assignmentRepositoriesAsync.FilterAssignmentAsync(location, search, assignmentState, assignedDate);

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

        public async Task<Response<AssignmentResponseDto>> GetAssignmentByIdAsync(Guid assignmentId)
        {
            var assignment = await _assignmentRepositoriesAsync.GetAssignemntByIdAsync(assignmentId);
            if (assignment == null)
            {
                return new Response<AssignmentResponseDto> { Succeeded = false, Message = "Assignment not found" };
            }
            var assignmentDto = _mapper.Map<AssignmentResponseDto>(assignment);
            return new Response<AssignmentResponseDto> { Succeeded = true, Data = assignmentDto };
        }

        public async Task<PagedResponse<List<AssignmentResponseDto>>> GetAssignmentsOfUser(PaginationFilter paginationFilter, Guid userId, string? search, EnumAssignmentState? assignmentState, DateTime? assignedDate, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                if (paginationFilter == null)
                {
                    paginationFilter = new PaginationFilter();
                }

                var filterAsset = await _assignmentRepositoriesAsync.FilterAssignmentOfUserAsync(userId, search, assignmentState, assignedDate);

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

        public async Task<Response<AssignmentDto>> ChangeAssignmentStateAsync(ChangeStateAssignmentDto request)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(request.AssignmentId);

            if (assignment == null)
            {
                return new Response<AssignmentDto>
                {
                    Succeeded = false,
                    Message = "Assignment not found."
                };
            }

            if (assignment.State == EnumAssignmentState.Accepted || assignment.State == EnumAssignmentState.Declined)
            {
                return new Response<AssignmentDto>
                {
                    Succeeded = false,
                    Message = "Assignment state cannot be changed."
                };
            }

            assignment.State = request.NewState;

            var assetResponse = await _assetRepository.GetByIdAsync(assignment.AssetId);

                if (assetResponse == null)
                {
                    return new Response<AssignmentDto>
                    {
                        Succeeded = false,
                        Message = "Asset not found."
                    };
                }

            if (request.NewState == EnumAssignmentState.Declined)
            {
                assetResponse.State = AssetStateType.Available;

                try
                {
                    await _assetRepository.UpdateAsync(assetResponse);
                }
                catch (Exception ex)
                {
                    return new Response<AssignmentDto>
                    {
                        Succeeded = false,
                        Message = "An error occurred while updating the asset state.",
                        Errors = new List<string> { ex.Message }
                    };
                }
            }

            try
            {
                await _assignmentRepository.UpdateAsync(assignment);
                var assignmentDto = _mapper.Map<AssignmentDto>(assignment);
                return new Response<AssignmentDto>
                {
                    Succeeded = true,
                    Message = "Assignment state changed successfully.",
                    Data = assignmentDto
                };
            }
            catch (Exception ex)
            {
                return new Response<AssignmentDto>
                {
                    Succeeded = false,
                    Message = "An error occurred while changing the assignment state.",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response<AssignmentDto>> DeleteAssignmentAsync(Guid assignmentId)
        {
            var exsittingAssigment = await _assignmentRepositoriesAsync.GetAssignemntByIdAsync(assignmentId);
            if (exsittingAssigment == null)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Assignment cannot be found." };
            }
            if (exsittingAssigment.State == EnumAssignmentState.Accepted)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Cannot delete assignment because this assignment has been approved." };
            }

            var assetResponse = await _assetRepository.GetByIdAsync(exsittingAssigment.AssetId);
            if(assetResponse == null)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Cannot delete this assignment because can not found the asset of this assignment." };
            }

            assetResponse.State = AssetStateType.Available;
            await _assignmentRepositoriesAsync.DeleteAsync(assignmentId); 

            return new Response<AssignmentDto> { Succeeded = true, Message = "Assignment deleted successfully." };
        }

  
    }
}