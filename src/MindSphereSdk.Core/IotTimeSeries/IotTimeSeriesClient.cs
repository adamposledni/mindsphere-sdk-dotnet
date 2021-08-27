using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.IotTimeSeries
{
    /// <summary>
    /// Create, read, update, and delete time series data
    /// </summary>
    public class IotTimeSeriesClient : SdkClient
    {
        private readonly string _baseUri = "/api/iottimeseries/v3";

        internal IotTimeSeriesClient(MindSphereConnector mindSphereConnector)
            : base(mindSphereConnector)
        {
        }

        /// <summary>
        /// Create or update time series data for mutiple unique asset-aspect (entity-property set) combinations
        /// </summary>
        public async Task PutTimeSeriesMultipleAsync(PutTimeSeriesMultipleRequest request)
        {
            // prepare URI string
            string uri = _baseUri + "/timeseries";

            // prepare HTTP request body
            StringContent body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // make request
            await HttpActionAsync(HttpMethod.Put, uri, body);
        }

        /// <summary>
        /// Retrieve time series data
        /// </summary>
        public async Task<IEnumerable<T>> GetTimeSeriesAsync<T>(GetTimeSeriesRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("from", request.From);
            queryBuilder.AddQuery("to", request.To);
            queryBuilder.AddQuery("limit", request.Limit);
            queryBuilder.AddQuery("select", request.Select);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("latestValue", request.LatestValue);
            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryBuilder.ToString();

            // make request
            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var timeSeries = JsonConvert.DeserializeObject<IEnumerable<T>>(response);

            return timeSeries;
        }

        /// <summary>
        /// Retrieve time series data
        /// </summary>
        /// <remarks>
        /// If generic type is not specified the method returns dynamic type
        /// </remarks>
        public async Task<IEnumerable<dynamic>> GetTimeSeriesAsync(GetTimeSeriesRequest request)
        {
            return await GetTimeSeriesAsync<dynamic>(request);
        }

        /// <summary>
        /// Create or update time series data
        /// </summary>
        // TODO: docs example
        public async Task PutTimeSeriesAsync(PutTimeSeriesRequest request)
        {
            // prepare URI string
            string uri = _baseUri + $"/timeseries/{request.EntityId}/{request.PropertySetName}";

            // prepare HTTP request body
            StringContent body = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");

            // make request
            await HttpActionAsync(HttpMethod.Put, uri, body);
        }

        /// <summary>
        /// Delete time series data
        /// </summary>
        public async Task DeleteTimeSeriesAsync(DeleteTimeSeriesRequest request)
        {
            // prepare URI string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("from", request.From);
            queryBuilder.AddQuery("to", request.To);
            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryBuilder.ToString();

            // make request
            await HttpActionAsync(HttpMethod.Delete, uri);
        }
    }
}
