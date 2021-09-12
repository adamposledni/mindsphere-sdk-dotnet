using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Helpers;
using MindSphereSdk.Core.Serialization;
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

        #region Events

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
            string jsonString = JsonConverter.Serialize(request.Event, utc: true);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var eventObj = JsonConverter.Deserialize<T>(response);

            return eventObj;
        }

        /// <summary>
        /// Query events. 
        /// </summary>
        public async Task<ResourceList<T>> ListEventsAsync<T>(ListEventsRequest request) where T : Event
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
            var eventList = JsonConverter.Deserialize<MindSphereApiResource<EmbeddedEventList<T>>>(response);

            // format output
            var output = new ResourceList<T>
            {
                Data = eventList.Embedded.Events,
                Page = eventList.Page
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
            var eventObj = JsonConverter.Deserialize<T>(response);

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
            string json = JsonConverter.Serialize(request.Event, ignoreNull: true);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var eventObj = JsonConverter.Deserialize<T>(response);

            return eventObj;
        }

        #endregion

        #region Event types

        /// <summary>
        /// Create new event type.
        /// </summary>
        public async Task<EventType> AddEventTypeAsync(AddEventTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/eventTypes" + queryBuilder.ToString();

            // prepare HTTP request body
            string json = JsonConverter.Serialize(request.EventType);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var eventObj = JsonConverter.Deserialize<EventType>(response);

            return eventObj;
        }

        /// <summary>
        /// Query event types.
        /// </summary>
        public async Task<ResourceList<EventType>> ListEventTypesAsync(ListEventTypesRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("size", request.Size);
            queryBuilder.AddQuery("filter", request.Filter);
            queryBuilder.AddQuery("page", request.Page);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/eventTypes" + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var eventTypeList = JsonConverter.Deserialize<MindSphereApiResource<EmbeddedEventTypeList>>(response);

            // format output
            var output = new ResourceList<EventType>
            {
                Data = eventTypeList.Embedded.EventTypes,
                Page = eventTypeList.Page
            };
            return output;
        }

        /// <summary>
        /// Update an event type.
        /// </summary>
        public async Task<EventType> UpdateEventTypeAsync(UpdateEventTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/eventTypes/" + request.EventTypeId + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // prepare HTTP request body
            string json = JsonConverter.Serialize(request.EventTypePatch, ignoreNull: true);

            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");

            // make request
            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var eventType = JsonConverter.Deserialize<EventType>(response);

            return eventType;
        }

        /// <summary>
        /// Get an event type.
        /// </summary>
        public async Task<EventType> GetEventTypeAsync(GetEventTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/eventTypes/" + request.EventTypeId + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch)
            };

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri, headers: headers);
            var eventObj = JsonConverter.Deserialize<EventType>(response);

            return eventObj;
        }

        /// <summary>
        /// Delete an event type.
        /// </summary>
        public async Task DeleteEventTypeAsync(DeleteEventTypeRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("includeShared", request.IncludeShared);
            string uri = _baseUri + "/eventTypes/" + request.EventTypeId + queryBuilder.ToString();

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("If-Match", request.IfMatch)
            };

            // make request
            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        #endregion
    }
}
