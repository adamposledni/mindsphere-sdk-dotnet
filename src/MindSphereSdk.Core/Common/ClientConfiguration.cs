using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.Common
{
    /// <summary>
    /// MindSphere SDK client configuration
    /// </summary>
    // TODO: timeout, proxy
    public class ClientConfiguration
    {
        public string Domain { get; set; } = "mindsphere.io";
        public string Region { get; set; } = "eu1";
        public ClientConfiguration(string region, string domain)
        {
            Domain = domain;
            Region = region;
        }
    }
}
