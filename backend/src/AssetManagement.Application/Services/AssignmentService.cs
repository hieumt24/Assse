using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assignments;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AutoMapper;
using FluentValidation;

namespace AssetManagement.Application.Services
{
    public class AssignmentServiceAsync : IAssignmentServicesAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentRepositoriesAsync _assignmentRepositories;
        private readonly IAssetRepositoriesAsync _assetRepository;
        private readonly IUserRepositoriesAsync _userRepository;
        private readonly IValidator<AddAssignmentRequestDto> _addAssignmentValidator;


        public AssignmentServiceAsync(
             IAssignmentRepositoriesAsync assignmentRepositories,
             IAssetRepositoriesAsync assetRepository,
             IUserRepositoriesAsync userRepository,
             IMapper mapper,
             IValidator<AddAssignmentRequestDto> addAssignmentValidator
            )
        {
            _mapper = mapper;
            _assignmentRepositories = assignmentRepositories;
            _addAssignmentValidator = addAssignmentValidator;
            _assetRepository = assetRepository;
            _userRepository = userRepository;
            
            
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
                return new Response<AssignmentDto> { Succeeded = false , Message = "Asset not found."};
            }

            var existingAssignedIdBy = await _userRepository.GetByIdAsync(request.AssignedIdBy);
            if (existingAssignedIdBy == null)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "User assigned by not found." };
            } 
            if(existingAssignedIdBy.JoinedDate < request.AssignedDate) {
                return new Response<AssignmentDto> { Succeeded = false, Message = "Assigned Date must be greater than Joined Date." };
            }


            var existingAssignedIdTo = await _userRepository.GetByIdAsync(request.AssignedIdTo);
            if (existingAssignedIdTo == null)
            {
                return new Response<AssignmentDto> { Succeeded = false, Message = "User assigned to not found." };
            }
            //add new assignment
            try {
                var newAssigment = _mapper.Map<Assignment>(request);
                newAssigment.CreatedOn = DateTime.Now;
                newAssigment.CreatedBy = request.AssignedIdBy.ToString();
                var asignment = await _assignmentRepositories.AddAsync(newAssigment);

                var assetDto = _mapper.Map<AssignmentDto>(asignment);

                return new Response<AssignmentDto> { Succeeded = true , Message=" Create Assignment Successfully!"};
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

        public Task<Response<List<AssignmentDto>>> GetAllAssignmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<AssignmentDto>> GetAssignmentByIdAsync(Guid assignmentId)
        {
            throw new NotImplementedException();
        }
    }
}
