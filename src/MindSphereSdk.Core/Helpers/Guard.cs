using FluentValidation;
using FluentValidation.Results;
using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Helpers.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Helpers
{
    /// <summary>
    /// Data guard.
    /// </summary>
    internal static class Guard
    {
        /// <summary>
        /// Ensure object is not null.
        /// </summary>
        public static void NotNull(object obj, string paramName = null)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Validate object.
        /// </summary>
        public static void Validate(object obj, string paramName = null)
        {
            NotNull(obj, paramName);

            var result = Validator.GetValidationResult(obj);
            if (!result.IsValid)
            {
                throw new ArgumentException("Invalid data", paramName);
            }
        }
    }
}
