using MindSphereSdk.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.EventManagement
{
    /// <summary>
    /// Create, read, update and delete Events and Event Types
    /// </summary>
    public class EventManagementClient : SdkClient
    {
        private readonly string _baseUri = "/api/eventmanagement/v3";

        public EventManagementClient(ICredentials credentials, HttpClient httpClient) : base(credentials, httpClient)
        {
        }

        /// <summary>
        /// Create new event
        /// </summary>
        public async Task<T> AddEventAsync<T>(AddEventRequest request) where T : Event
        {
            string uri = _baseUri + "/events";

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var eventObj = JsonConvert.DeserializeObject<T>(response);

            return eventObj;
        }
    }
}
