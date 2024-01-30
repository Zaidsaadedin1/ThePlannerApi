using FluentValidation;
using ThePlannerAPI.DTOs.Task;

namespace ThePlannerAPI.Validators.Assignment
{
    public class UpdateAssignmentsValidator : AbstractValidator<UpdateAssignmentDTO>
    {
        public UpdateAssignmentsValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Must Be Not Empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description Must Be Not Empty");
            RuleFor(x => x)
                .Must(x => x.DueDate >= x.StartDate)
                .When(x => x.DueDate is not null)
                .WithMessage("DueDate must be greater than or equal to StartDate");
        }
    }
}


