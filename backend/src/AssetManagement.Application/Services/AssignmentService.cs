using AssetManagement.Application.Common;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assignment;
using AssetManagement.Application.Models.DTOs.Assignment.Request;
using AssetManagement.Application.Models.DTOs.Assignment.Response;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Application.Services
{
    public class AssignmentServiceAsync : IAssignmentServicesAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentRepositoriesAsync _assignmentRepositoriesAsync;
        private readonly IUriService _uriService;

        public AssignmentServiceAsync(IAssignmentRepositoriesAsync assignmentRepositoriesAsync,
             IMapper mapper,
             IUriService uriService
            )
        {
            _mapper = mapper;
            _assignmentRepositoriesAsync = assignmentRepositoriesAsync;
            _uriService = uriService;
        }

        public async Task<Response<AssignmentDto>> AddAssignmentAsync(AddAssignmentRequestDto request)
        {
            //var validationResult = await _addCategoryValidator.ValidateAsync(request);
            //if (!validationResult.IsValid)
            //{
            //    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            //    return new Response<CategoryDto>
            //    {
            //        Succeeded = false,
            //        Message = string.Join("; ", errors)
            //    };
            //}

            try
            {
                var newAssigment = _mapper.Map<Assignment>(request);
                newAssigment.CreatedOn = DateTime.Now;

                var asignment = await _assignmentRepositoriesAsync.AddAsync(newAssigment);

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
                var filterAsset = await _assignmentRepositoriesAsync.FilterAssignment(adminLocation, search, assignmentStatus, assignedDate);

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