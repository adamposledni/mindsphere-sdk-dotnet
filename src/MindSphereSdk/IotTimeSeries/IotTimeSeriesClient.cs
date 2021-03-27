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
        /// Retrieve time series data
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<object>> GetTimeSeriesAsync(GetTimeSeriesRequest request)
        {
            string uri = GenerateUriForGetTimeSeries(request);

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var timeSeries = JsonConvert.DeserializeObject<IEnumerable<object>>(response);
            return timeSeries;
        }

        /// <summary>
        /// Retrieve time series data (generic function)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetTimeSeriesAsync<T>(GetTimeSeriesRequest request)
        {
            string uri = GenerateUriForGetTimeSeries(request);

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var timeSeries = JsonConvert.DeserializeObject<IEnumerable<T>>(response);
            return timeSeries;
        }

        /// <summary>
        /// Helper to generate spec uri for given request
        /// </summary>
        private string GenerateUriForGetTimeSeries(GetTimeSeriesRequest request)
        {
            // prepare query string
            string queryString = "?";        

            queryString += request.From != null ? $"from={GenerateDateTimeUtcString(request.From.Value)}& " : "";
            queryString += request.To != null ? $"to={GenerateDateTimeUtcString(request.To.Value)}&" : "";
            queryString += request.Limit != null ? $"limit={request.Limit.Value}&" : "";
            queryString += request.Select != null ? $"select={request.Select}&" : "";
            queryString += request.Sort != null ? $"sort={request.Sort}&" : "";
            queryString += request.LatestValue != null ? $"latestValue={request.LatestValue.Value}&" : "";

            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryString;
            Debug.WriteLine(uri);
            return uri;
        }

        /// <summary>
        /// Helper to generate date time UTC string
        /// </summary>
        private string GenerateDateTimeUtcString(DateTime date)
        {
            string dateString = date.ToUniversalTime().ToString("s") + "Z";
            return dateString;
        }


        /// <summary>
        /// Create or update time series data
        /// </summary>
        /// <returns></returns>
        public async Task PutTimeSeriesAsync(PutTimeSeriesRequest request)
        {
            string uri = _baseUri + "/timeseries";

            StringContent body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            await HttpActionAsync(HttpMethod.Put, uri, body);
        }

    }
}
