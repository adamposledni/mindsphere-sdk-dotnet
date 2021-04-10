using MindSphereSdk.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.IotTimeSeries
{
    /// <summary>
    /// Create, read, update, and delete time series data
    /// </summary>
    public class IotTimeSeriesClient : SdkClient
    {
        private readonly string _baseUri = "/api/iottimeseries/v3";

        public IotTimeSeriesClient(ICredentials credentials, HttpClient httpClient) : base(credentials, httpClient)
        {

        }

        /// <summary>
        /// Retrieve time series data (generic function)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
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

            Debug.WriteLine(uri);

            await HttpActionAsync(HttpMethod.Delete, uri);
        }

        /// <summary>
        /// Generate specific URI for get request
        /// </summary>
        private string GetUriForGetTimeSeries(GetTimeSeriesRequest request)
        {
            // prepare query string
            string queryString = "?";

            queryString += request.From != null ? $"from={GetDateTimeUtcString(request.From.Value)}& " : "";
            queryString += request.To != null ? $"to={GetDateTimeUtcString(request.To.Value)}&" : "";
            queryString += request.Limit != null ? $"limit={request.Limit.Value}&" : "";
            queryString += request.Select != null ? $"select={request.Select}&" : "";
            queryString += request.Sort != null ? $"sort={request.Sort}&" : "";
            queryString += request.LatestValue != null ? $"latestValue={request.LatestValue.Value}&" : "";

            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryString;
            return uri;
        }

        /// <summary>
        /// Generate specific URI for delete request
        /// </summary>
        private string GetUriForDeleteTimeSeries(DeleteTimeSeriesRequest request)
        {
            // prepare query string
            string queryString = "?";

            queryString += request.From != null ? $"from={GetDateTimeUtcString(request.From.Value)}&" : "";
            queryString += request.To != null ? $"to={GetDateTimeUtcString(request.To.Value)}&" : "";

            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryString;
            return uri;
        }

        /// <summary>
        /// Generate date time UTC string
        /// </summary>
        private string GetDateTimeUtcString(DateTime date)
        {
            string dateString = date.ToUniversalTime().ToString("s") + "Z";
            return dateString;
        }

    }
}
