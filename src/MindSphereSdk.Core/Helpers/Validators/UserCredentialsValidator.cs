using FluentValidation;
using MindSphereSdk.Core.Authentication;

namespace MindSphereSdk.Core.Helpers.Validators
{
    internal class UserCredentialsValidator : AbstractValidator<UserCredentials>
    {
        public UserCredentialsValidator()
        {
            RuleFor(uc => uc.Token).NotEmpty();
        }
    }
}
