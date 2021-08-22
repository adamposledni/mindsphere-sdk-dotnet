using MindSphereSdk.Core.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MindSphereSdk.Core.Authentication
{
    public abstract class Credentials
    {
        protected MindSphereConnector _mindSphereConnector;

        /// <summary>
        /// Create specified MindSphere connector based on provided credentials
        /// </summary>
        internal abstract MindSphereConnector GetConnector(ClientConfiguration configuration, HttpClient httpClient);
    }
}
