using System;

namespace MindSphereSdk.Core.Helpers
{
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
                _query += $"{name}={Helper.GetDateTimeUtcString(value.Value)}&";
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
