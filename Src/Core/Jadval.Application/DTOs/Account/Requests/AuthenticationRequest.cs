using FluentValidation;
using Jadval.Application.Behaviours;

namespace Jadval.Application.DTOs.Account.Requests
{
    public class AuthenticationRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().NotNull();

            RuleFor(x => x.Password)
                .NotEmpty().NotNull()
                .Matches(JadvalRegex.Password);
        }
    }
}
