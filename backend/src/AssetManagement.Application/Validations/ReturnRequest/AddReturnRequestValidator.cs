using AssetManagement.Application.Models.DTOs.ReturnRequests.Request;
using AssetManagement.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Validations.ReturnRequest
{
    public class AddReturnRequestValidator: AbstractValidator<AddReturnRequestDto>
    {
        public AddReturnRequestValidator() 
        {
            RuleFor(x => x.AssignmentId)
              .NotEmpty().WithMessage("AssignmentId can not be blank.")
              .Must(id => id != Guid.Empty).WithMessage("AssignmentId must be a valid GUID.");

            RuleFor(x => x.RequestedBy)
                .NotEmpty().WithMessage("RequestedBy can not be blank.")
                .Must(id => id != Guid.Empty).WithMessage("RequestedBy must be a valid GUID.");

            RuleFor(x => x.AcceptedBy)
                .NotEmpty().WithMessage("AcceptedBy can not be blank.")
                .Must(id => id != Guid.Empty).WithMessage("AcceptedBy must be a valid GUID.");

            RuleFor(x => x.ReturnedDate)
                .NotEmpty().WithMessage("ReturnedDate can not be blank.")
                .Must(date => date > new DateTime(2000, 1, 1)).WithMessage("ReturnedDate must be greater than the year 2000.");

            RuleFor(x => x.ReturnState)
               .NotEmpty().WithMessage("Please enter state")
               .Must(state => state == EnumReturnRequestState.WaitingForReturning).WithMessage("Invalid state.");
        }
    }
}
