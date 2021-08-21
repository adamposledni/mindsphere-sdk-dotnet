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

        public IotTimeSeriesClient(ICredentials credentials, ClientConfiguration configuration, HttpClient httpClient) 
            : base(credentials, configuration, httpClient)
        {
        }

        /// <summary>
        /// Retrieve time series data (generic function)
        /// </summary>
        public async Task<IEnumerable<T>> GetTimeSeriesAsync<T>(GetTimeSeriesRequest request)
        {
            string uri = GetUriForGetTimeSeries(request);

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var timeSeries = JsonConvert.DeserializeObject<IEnumerable<T>>(response);
            return timeSeries;
        }

        /// <summary>
        /// Retrieve time series data
        /// </summary>
        public async Task<IEnumerable<dynamic>> GetTimeSeriesAsync(GetTimeSeriesRequest request)
        {
            string uri = GetUriForGetTimeSeries(request);

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var timeSeries = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response);
            return timeSeries;
        }

        /// <summary>
        /// Create or update time series data
        /// </summary>

        public async Task PutTimeSeriesAsync(PutTimeSeriesRequest request)
        {
            string uri = _baseUri + "/timeseries";

            StringContent body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            await HttpActionAsync(HttpMethod.Put, uri, body);
        }

        /// <summary>
        /// Delete time series data
        /// </summary>
        public async Task DeleteTimeSeriesAsync(DeleteTimeSeriesRequest request)
        {
            string uri = GetUriForDeleteTimeSeries(request);
            await HttpActionAsync(HttpMethod.Delete, uri);
        }

        /// <summary>
        /// Generate specific URI for get request
        /// </summary>
        private string GetUriForGetTimeSeries(GetTimeSeriesRequest request)
        {
            // prepare query string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("from", request.From);
            queryBuilder.AddQuery("to", request.To);
            queryBuilder.AddQuery("limit", request.Limit);
            queryBuilder.AddQuery("select", request.Select);
            queryBuilder.AddQuery("sort", request.Sort);
            queryBuilder.AddQuery("latestValue", request.LatestValue);

            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryBuilder.ToString();
            return uri;
        }

        /// <summary>
        /// Generate specific URI for delete request
        /// </summary>
        private string GetUriForDeleteTimeSeries(DeleteTimeSeriesRequest request)
        {
            // prepare query string
            // prepare query string
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("from", request.From);
            queryBuilder.AddQuery("to", request.To);

            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryBuilder.ToString();
            return uri;
        }
    }
}
