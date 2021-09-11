using System.Net.Http;

namespace MindSphereSdk.Core.Helpers
{
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
}
