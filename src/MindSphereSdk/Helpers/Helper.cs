using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Helpers
{
    public static class Helper
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
}
