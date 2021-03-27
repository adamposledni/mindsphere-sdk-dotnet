using MindSphereSdk.Common;
using Newtonsoft.Json;
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
            string queryString = "?";
            // prepare path (needed)
            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryString;
            
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
            string queryString = "?";
            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryString;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var timeSeries = JsonConvert.DeserializeObject<IEnumerable<T>>(response);
            return timeSeries;
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
