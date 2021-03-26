using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereLibrary.Common
{
    public interface IMindSphereConnector
    {
        Task<string> HttpActionAsync(HttpMethod method, string specUri, string body);

        Task AcquireTokenAsync();

        bool ValidateToken();

        Task RenewTokenAsync();
    }
}
