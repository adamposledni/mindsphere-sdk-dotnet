using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Helpers;
using Newtonsoft.Json;
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
        /// Create new event.
        /// </summary>
        public async Task<T> AddEventAsync<T>(AddEventRequest request) where T : Event
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/events" + queryBuilder.ToString();

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Event,
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

        /// <summary>
        /// Query events. 
        /// </summary>
        public async Task<ResourceList<dynamic>> ListEventsAsync(ListEventsRequest request)
        {
            return await ListEventsLogicAsync<dynamic>(request);
        }

        /// <summary>
        /// Query events. 
        /// </summary>
        public async Task<ResourceList<T>> ListEventsAsync<T>(ListEventsRequest request) where T : Event
        {
            return await ListEventsLogicAsync<T>(request);
        }


        /// <summary>
        /// Executive method for querying events.
        /// </summary>
        private async Task<ResourceList<T>> ListEventsLogicAsync<T>(ListEventsRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("filter", request.Filter);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("history", request.History);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            queryBuilder.AddQuery("enablePageCache", request.EnablePageCache);
            string uri = _baseUri + "/events" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var eventListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedEventList<T>>>(response);

            // format output
            var output = new ResourceList<T>
            {
                Data = eventListWrapper.Embedded.Events,
                Page = eventListWrapper.Page
            };
            return output;
        }

        /// <summary>
        /// Get an event.
        /// </summary>
        public async Task<T> GetEventAsync<T>(GetEventRequest request) where T : Event
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/events/" + request.EventId + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var eventObj = JsonConvert.DeserializeObject<T>(response);

            return eventObj;
        }

        /// <summary>
        /// Update an event.
        /// </summary>
        public async Task<T> UpdateEventAsync<T>(UpdateEventRequest request) where T : Event
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/events/" + request.EventId + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Event,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var eventObj = JsonConvert.DeserializeObject<T>(response);

            return eventObj;
        }
    }
}
