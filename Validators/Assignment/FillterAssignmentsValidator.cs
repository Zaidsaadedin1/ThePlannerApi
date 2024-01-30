using FluentValidation;
using ThePlannerAPI.DTOs.Assignment;
using ThePlannerAPI.Enums;

namespace ThePlannerAPI.Validators.Assignment
{
    public class FillterAssignmentsValidator : AbstractValidator<AssignmentFilterDTO>
    {
        public FillterAssignmentsValidator()
        {

            RuleFor(x => x.Priority)
            .Must(value => value == null || Enum.IsDefined(typeof(AssignmentProiority), value))
            .WithMessage("Invalid Priority");

            RuleFor(x => x.IsComplete)
            .Must(value => value == null || bool.TryParse(value.ToString(), out _))
            .WithMessage("IsComplete must be either true, false, or null");


        }
    }
}
