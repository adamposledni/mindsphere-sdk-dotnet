using System;

namespace MindSphereSdk.Core.Exceptions
{
    /// <summary>
    /// MindSphere API exception
    /// </summary>
    public class MindSphereApiException : Exception
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Create a new instance of MindSphereApiException
        /// </summary>
        public MindSphereApiException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
