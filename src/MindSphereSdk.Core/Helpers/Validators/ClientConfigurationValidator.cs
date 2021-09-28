using FluentValidation;
using MindSphereSdk.Core.Common;

namespace MindSphereSdk.Core.Helpers.Validators
{
    internal class ClientConfigurationValidator : AbstractValidator<ClientConfiguration>
    {
        public ClientConfigurationValidator()
        {
            RuleFor(cc => cc.Region).NotEmpty();
            RuleFor(cc => cc.Domain).NotEmpty();
            RuleFor(cc => cc.Timeout).GreaterThan(0);
        }
    }
}
