using System;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// MindSphere SDK client configuration
    /// </summary>

    public class ClientConfiguration
    {
        /// <summary>
        /// MindSphere domain
        /// </summary>
        /// <remarks>
        /// Default: mindsphere.io
        /// </remarks>
        public string Domain { get; set; } = "mindsphere.io";

        /// <summary>
        /// MindSphere region
        /// </summary>
        /// <remarks>
        /// Default: eu1
        /// </remarks>
        public string Region { get; set; } = "eu1";

        /// <summary>
        /// Connection timeout
        /// </summary>
        /// <remarks>
        /// Default: 100s
        /// </remarks>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMilliseconds(100000);

        /// <summary>
        /// Proxy address
        /// </summary>
        public string Proxy { get; set; }
    }
}
