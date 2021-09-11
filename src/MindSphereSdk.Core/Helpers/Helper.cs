using System;

namespace MindSphereSdk.Core.Helpers
{
    /// <summary>
    /// Helper.
    /// </summary>
    internal static class Helper
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
}
