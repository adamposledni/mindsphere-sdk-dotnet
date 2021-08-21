using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Exceptions
{
    public class MindSphereApiException : Exception
    {
        public int StatusCode { get; set; }

        public MindSphereApiException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
