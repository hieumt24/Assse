﻿using AssetManagement.Application.Common;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Helper;
using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Reponses;
using AssetManagement.Application.Models.DTOs.ReturnRequests;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Request;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AssetManagement.Domain.Enums;
using AutoMapper;
using FluentValidation;

namespace AssetManagement.Application.Services
{
    public class ReturnRequestServiceAsync : IReturnRequestServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly IReturnRequestRepositoriesAsync _returnRequestRepository;
        private readonly IUserRepositoriesAsync _userRepository;
        private readonly IAssignmentRepositoriesAsync _assignmentRepository;
        private readonly IValidator<AddReturnRequestDto> _addReturnRequestValidator;
        private readonly IUriService _uriService;

        public ReturnRequestServiceAsync(
            IReturnRequestRepositoriesAsync returnRequestRepository,
            IMapper mapper,
            IValidator<AddReturnRequestDto> addReturnRequestValidator,
            IUserRepositoriesAsync userRepository,
            IAssignmentRepositoriesAsync assignmentRepository,
            IUriService uriService
            )
        {
            _mapper = mapper;
            _returnRequestRepository = returnRequestRepository;
            _addReturnRequestValidator = addReturnRequestValidator;
            _userRepository = userRepository;
            _assignmentRepository = assignmentRepository;
            _uriService = uriService;
        }

        public async Task<Response<ReturnRequestDto>> AddReturnRequestAsync(AddReturnRequestDto request)
        {
            var validatorRequest = await _addReturnRequestValidator.ValidateAsync(request);
            if (!validatorRequest.IsValid)
            {
                var errors = validatorRequest.Errors.Select(e => e.ErrorMessage).ToList();
                return new Response<ReturnRequestDto>
                {
                    Succeeded = false,
                    Errors = errors
                };
            }

            var existingAssignment = await _assignmentRepository.GetByIdAsync(request.AssignmentId);
            if (existingAssignment == null)
            {
                return new Response<ReturnRequestDto> { Succeeded = false, Message = "Assignment not found." };
            }

            var existingUserRequest = await _userRepository.GetByIdAsync(request.RequestedBy);
            if (existingUserRequest == null)
            {
                return new Response<ReturnRequestDto> { Succeeded = false, Message = "User request not found." };
            }

            var existingUserAccept = await _userRepository.GetByIdAsync(request.AcceptedBy);
            if (existingUserAccept == null)
            {
                return new Response<ReturnRequestDto> { Succeeded = false, Message = "User accept not found." };
            }

            try
            {
                var newReturnRequest = _mapper.Map<ReturnRequest>(request);
                newReturnRequest.ReturnedDate = DateTime.Now;
                newReturnRequest.CreatedOn = DateTime.Now;
                newReturnRequest.CreatedBy = request.AcceptedBy.ToString();
                var returnrequest = await _returnRequestRepository.AddAsync(newReturnRequest);

                var assetDto = _mapper.Map<ReturnRequestDto>(returnrequest);

                return new Response<ReturnRequestDto> { Succeeded = true, Message = "Create Return Request Successfully." };
            }
            catch (Exception ex)
            {
                return new Response<ReturnRequestDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public Task<Response<ReturnRequestDto>> DeleteReturnRequestAsync(Guid assignmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ReturnRequestDto>> EditReturnRequestAsync(EditReturnRequestDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<ReturnRequestResponseDto>>> GetAllReturnRequestAsync(PaginationFilter pagination, string? search, EnumReturnRequestStatus? status, DateTime? returnDate, EnumLocation location, string? orderBy, bool? isDescending, string? route)
        {
            try
            {
                if (pagination == null)
                {
                    pagination = new PaginationFilter();
                }
                var filterReturnRequest = await _returnRequestRepository.FilterReturnRequestAsync(location, search, status, returnDate);

                var totalRecords = filterReturnRequest.Count();

                var specRequestReturn = ReturnRequestSpecificationHelper.CreateSpecReturnRequest(pagination, orderBy, isDescending);

                var returnRequests = SpecificationEvaluator<ReturnRequest>.GetQuery(filterReturnRequest, specRequestReturn);

                var returnRequestResponseDtos = _mapper.Map<List<ReturnRequestResponseDto>>(returnRequests);

                var pagedResponsed = PaginationHelper.CreatePagedReponse(returnRequestResponseDtos, pagination, totalRecords, _uriService, route);
                return pagedResponsed;
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<ReturnRequestResponseDto>> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public Task<Response<ReturnRequestDto>> GetReturnRequestByIdAsync(Guid assignmentId)
        {
            throw new NotImplementedException();
        }
    }
}