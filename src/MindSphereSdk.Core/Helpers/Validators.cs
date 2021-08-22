using FluentValidation;
using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MindSphereSdk.Core.Helpers
{
    /// <summary>
    /// Data validator
    /// </summary>
    public class Validator
    {
        public static void Validate(AppCredentials appCredentials)
        {
            var result = new AppCredentialsValidator().Validate(appCredentials);
            if (!result.IsValid)
            {
                throw new ArgumentException("Invalid data in the application credentials");
            }
        }

        public static void Validate(UserCredentials userCredentials)
        {
            var result = new UserCredentialsValidator().Validate(userCredentials);
            if (!result.IsValid)
            {
                throw new ArgumentException("Invalid data in the user credentials");
            }
        }

        public static void Validate(ClientConfiguration clientConfiguration)
        {
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
