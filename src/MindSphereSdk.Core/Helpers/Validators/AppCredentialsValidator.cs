using FluentValidation;
using MindSphereSdk.Core.Authentication;

namespace MindSphereSdk.Core.Helpers.Validators
{
    internal class AppCredentialsValidator : AbstractValidator<AppCredentials>
    {
        public AppCredentialsValidator()
        {
            RuleFor(ac => ac.AppName).NotEmpty();
            RuleFor(ac => ac.AppVersion).NotEmpty();
            RuleFor(ac => ac.HostTenant).NotEmpty();
            RuleFor(ac => ac.UserTenant).NotEmpty();
            RuleFor(ac => ac.KeyStoreClientId).NotEmpty();
            RuleFor(ac => ac.KeyStoreClientSecret).NotEmpty();
        }
    }
}
