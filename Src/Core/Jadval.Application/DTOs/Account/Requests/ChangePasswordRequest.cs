using FluentValidation;
using Jadval.Application.Behaviours;

namespace Jadval.Application.DTOs.Account.Requests
{
    public class ChangePasswordRequest
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().NotNull()
                .MinimumLength(6)
                .Matches(JadvalRegex.Password);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .Matches(JadvalRegex.Password);
        }
    }

}
