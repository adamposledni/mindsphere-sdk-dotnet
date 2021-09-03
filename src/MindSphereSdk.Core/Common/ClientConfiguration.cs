using MindSphereSdk.Core.Helpers;
using System;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// MindSphere SDK client configuration.
    /// </summary>

    public class ClientConfiguration
    {
        private const string _domainDef = "mindsphere.io";
        private const string _regionDef = "eu1";
        private const double _timeoutDef = 100000.0;

        /// <summary>
        /// MindSphere domain.
        /// </summary>
        /// <remarks>
        /// Default: mindsphere.io.
        /// </remarks>
        public string Domain { get; private set; }

        /// <summary>
        /// MindSphere region.
        /// </summary>
        /// <remarks>
        /// Default: eu1.
        /// </remarks>
        public string Region { get; private set; }

        /// <summary>
        /// Connection timeout in miliseconds.
        /// </summary>
        /// <remarks>
        /// Default: 100000 ms.
        /// </remarks>
        public double Timeout { get; private set; }

        /// <summary>
        /// Proxy address.
        /// </summary>
        public string Proxy { get; private set; }

        public ClientConfiguration(string domain = _domainDef, string region = _regionDef, double timeout = _timeoutDef, string proxy = null)
        {
            Domain = domain;
            Region = region;
            Timeout = timeout;
            Proxy = proxy;

            Validator.Validate(this);
        }
    }
}
