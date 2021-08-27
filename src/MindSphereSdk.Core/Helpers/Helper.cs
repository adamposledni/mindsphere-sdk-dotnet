using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MindSphereSdk.Core.Helpers
{
    /// <summary>
    /// Helper
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// Generate date time UTC string
        /// </summary>
        public static string GetDateTimeUtcString(DateTime date)
        {
            string dateString = date.ToUniversalTime().ToString("s") + "Z";
            return dateString;
        }
    }

    internal static class MultipartFormDataContentExtension
    {
        /// <summary>
        /// Extension method for MultipartFormDataContent to add only not-null values
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
    /// URI query builder
    /// </summary>
    internal class QueryStringBuilder
    {
        private string _query = "?";

        /// <summary>
        /// Add new part to the URI query string
        /// </summary>
        /// <remarks>
        /// Adds value only if not empty
        /// </remarks>
        public void AddQuery(string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _query += $"{name}={value}&";
            }
        }

        /// <summary>
        /// Add new part to the URI query string
        /// </summary>
        /// <remarks>
        /// Adds value only if not empty
        /// </remarks>
        public void AddQuery(string name, int? value)
        {
            if (value != null)
            {
                _query += $"{name}={value.Value}&";
            }
        }

        /// <summary>
        /// Add new part to the URI query string
        /// </summary>
        /// <remarks>
        /// Adds value only if not empty
        /// </remarks>
        public void AddQuery(string name, DateTime? value)
        {
            if (value != null)
            {
                _query += $"{name}={Helper.GetDateTimeUtcString(value.Value)}&";
            }
        }

        /// <summary>
        /// Add new part to the URI query string
        /// </summary>
        /// <remarks>
        /// Adds value only if not empty
        /// </remarks>
        public void AddQuery(string name, bool? value)
        {
            if (value != null)
            {
                _query += $"{name}={value.Value}&";
            }
        }

        /// <summary>
        /// Build the URI query string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _query;
        }
    }
}
