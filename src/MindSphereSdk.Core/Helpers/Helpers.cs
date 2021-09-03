using System;
using System.Net.Http;

namespace MindSphereSdk.Core.Helpers
{
    /// <summary>
    /// Helpers.
    /// </summary>
    internal static class Helpers
    {
        /// <summary>
        /// Generate date time UTC string.
        /// </summary>
        public static string GetDateTimeUtcString(DateTime date)
        {
            string dateString = date.ToUniversalTime().ToString("s") + "Z";
            return dateString;
        }
    }

    /// <summary>
    /// MultipartFormDataContent extensions.
    /// </summary>
    internal static class MultipartFormDataContentExtensions
    {
        /// <summary>
        /// Extension method for MultipartFormDataContent to add only not-null values.
        /// </summary>
        public static void AddStringContentIfNotNull(this MultipartFormDataContent content, string value, string name)
        {
            if (value != null)
            {
                content.Add(new StringContent(value), name);
            }
        }
    }

    /// <summary>
    /// URI query builder.
    /// </summary>
    internal class QueryStringBuilder
    {
        private string _query = "?";

        /// <summary>
        /// Add a new part to the URI query string.
        /// </summary>
        /// <remarks>
        /// Only if not empty.
        /// </remarks>
        public void AddQuery(string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _query += $"{name}={value}&";
            }
        }

        /// <summary>
        /// Add a new part to the URI query string.
        /// </summary>
        /// <remarks>
        /// Only if not empty.
        /// </remarks>
        public void AddQuery(string name, int? value)
        {
            if (value != null)
            {
                _query += $"{name}={value.Value}&";
            }
        }

        /// <summary>
        /// Add a new part to the URI query string.
        /// </summary>
        /// <remarks>
        /// Only if not empty.
        /// </remarks>
        public void AddQuery(string name, DateTime? value)
        {
            if (value != null)
            {
                _query += $"{name}={Helpers.GetDateTimeUtcString(value.Value)}&";
            }
        }

        /// <summary>
        /// Add a new part to the URI query string.
        /// </summary>
        /// <remarks>
        /// Only if not empty.
        /// </remarks>
        public void AddQuery(string name, bool? value)
        {
            if (value != null)
            {
                _query += $"{name}={value.Value}&";
            }
        }

        /// <summary>
        /// Build the URI query string.
        /// </summary>
        public override string ToString()
        {
            return _query;
        }
    }
}
