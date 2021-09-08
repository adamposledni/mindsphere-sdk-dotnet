using FluentValidation;
using FluentValidation.Results;
using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Helpers.Validators
{
    /// <summary>
    /// General validator.
    /// </summary>
    internal static class Validator
    {
        /// <summary>
        /// Get validation result for the given object.
        /// </summary>
        public static ValidationResult GetValidationResult(object obj)
        {
            if (obj is AppCredentials appCredentials)
            {
                return new AppCredentialsValidator().Validate(appCredentials);
            }

            if (obj is UserCredentials userCredentials)
            {
                return new UserCredentialsValidator().Validate(userCredentials);
            }

            if (obj is ClientConfiguration clientConfiguration)
            {
                return new ClientConfigurationValidator().Validate(clientConfiguration);
            }

            throw new InvalidOperationException($"Can not validate instance of type {obj.GetType().Name}");

        }
    }
}
