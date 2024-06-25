using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assignments;
using AssetManagement.Application.Models.DTOs.ReturnRequests;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Request;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
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

        public ReturnRequestServiceAsync(
            IReturnRequestRepositoriesAsync returnRequestRepository,
            IMapper mapper,
            IValidator<AddReturnRequestDto> addReturnRequestValidator,
            IUserRepositoriesAsync userRepository,
            IAssignmentRepositoriesAsync assignmentRepository

            )
        {
            _mapper = mapper;
            _returnRequestRepository = returnRequestRepository;
            _addReturnRequestValidator = addReturnRequestValidator;
            _userRepository = userRepository;
            _assignmentRepository = assignmentRepository;
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

        public Task<Response<List<ReturnRequestDto>>> GetAllReturnRequestAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<ReturnRequestDto>> GetReturnRequestByIdAsync(Guid assignmentId)
        {
            throw new NotImplementedException();
        }
    }
}
