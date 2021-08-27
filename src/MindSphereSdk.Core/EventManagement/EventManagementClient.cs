using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Helpers;
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

        internal EventManagementClient(MindSphereConnector mindSphereConnector) 
            : base(mindSphereConnector)
        {
        }

        /// <summary>
        /// Create new event
        /// </summary>
        public async Task<T> AddEventAsync<T>(AddEventRequest request) where T : Event
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/events" + queryBuilder.ToString();

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var eventObj = JsonConvert.DeserializeObject<T>(response);

            return eventObj;
        }
    }
}
