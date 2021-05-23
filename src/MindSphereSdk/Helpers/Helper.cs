using System;
using System.Collections.Generic;
using System.Net.Http;
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


}
