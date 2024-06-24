using AssetManagement.Application.Interfaces.Repositories;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assignment;
using AssetManagement.Application.Models.DTOs.Assignment.Request;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AutoMapper;

namespace AssetManagement.Application.Services
{
    public class AssignmentServiceAsync : IAssignmentServicesAsync
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentRepositoriesAsync _assignmentRepositoriesAsync;
        //private readonly validate 

        public AssignmentServiceAsync(IAssignmentRepositoriesAsync assignmentRepositoriesAsync,
             IMapper mapper
            )
        {
            _mapper = mapper;
            _assignmentRepositoriesAsync = assignmentRepositoriesAsync;
            
            
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

            try {
                var newAssigment = _mapper.Map<Assignment>(request);
                newAssigment.CreatedOn = DateTime.Now;

                var asignment = await _assignmentRepositoriesAsync.AddAsync(newAssigment);

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
