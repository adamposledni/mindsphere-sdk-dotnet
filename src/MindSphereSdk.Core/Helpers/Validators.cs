using FluentValidation;
using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using System;

namespace MindSphereSdk.Core.Helpers
{
    /// <summary>
    /// Data validator
    /// </summary>
    internal class Validator
    {
        /// <summary>
        /// Validate application credentials
        /// </summary>
        internal static void Validate(AppCredentials appCredentials)
        {
            if (appCredentials == null) throw new ArgumentNullException();

            var result = new AppCredentialsValidator().Validate(appCredentials);
            if (!result.IsValid)
            {
                throw new ArgumentException("Invalid data in the application credentials");
            }
        }

        /// <summary>
        /// Validate user credentials
        /// </summary>
        internal static void Validate(UserCredentials userCredentials)
        {
            if (userCredentials == null) throw new ArgumentNullException();

            var result = new UserCredentialsValidator().Validate(userCredentials);
            if (!result.IsValid)
            {
                throw new ArgumentException("Invalid data in the user credentials");
            }
        }

        /// <summary>
        /// Validate client configuration
        /// </summary>
        internal static void Validate(ClientConfiguration clientConfiguration)
        {
            if (clientConfiguration == null) throw new ArgumentNullException();

            var result = new ClientConfigurationValidator().Validate(clientConfiguration);
            if (!result.IsValid)
            {
                throw new ArgumentException("Invalid data in the client configuration");
            }
        }
    }

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

    internal class UserCredentialsValidator : AbstractValidator<UserCredentials>
    {
        public UserCredentialsValidator()
        {
            RuleFor(uc => uc.Token).NotEmpty();
        }
    }

    internal class ClientConfigurationValidator : AbstractValidator<ClientConfiguration>
    {
        public ClientConfigurationValidator()
        {
            RuleFor(cc => cc.Region).NotEmpty();
            RuleFor(cc => cc.Domain).NotEmpty();
        }
    }
}
