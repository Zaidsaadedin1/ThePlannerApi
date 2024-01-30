using FluentValidation;
using ThePlannerAPI.DTOs.User;

namespace ThePlannerAPI.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(user => user.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(user => user.Username)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);

            RuleFor(user => user.HasImage)
                 .NotNull()
                 .Equal(true)
                 .When(user => !string.IsNullOrEmpty(user.ImageUrl));


            RuleFor(user => user.ImageUrl)
                .Empty().When(user => !user.HasImage)
                .MaximumLength(255).When(user => user.HasImage);

            //RuleFor(user => user.Email)
            //     .NotNull()
            //     .NotEmpty()
            //     .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            //     .WithMessage("Invalid email format. It should contain @ and end with .com");


            //RuleFor(user => user.Password)
            //        .NotNull()
            //        .NotEmpty()
            //        .MinimumLength(8)
            //        .MaximumLength(50);
        }
    }
}
