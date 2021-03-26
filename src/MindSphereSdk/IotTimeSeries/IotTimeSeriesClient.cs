using MindSphereSdk.Common;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.IotTimeSeries
{
    /// <summary>
    /// Client to create, read, update, and delete time series data
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
        public async Task<string> GetTimeSeriesAsync(GetTimeSeriesRequest request)
        {
            string queryString = "?";
            string pathString = $"/{request.EntityId}/{request.PropertySetName}";
            string uri = _baseUri + "/timeseries" + pathString + queryString;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            return response;
        }

        /// <summary>
        /// Create or update time series data
        /// </summary>
        /// <returns></returns>
        public async Task<string> PutTimeSeriesAsync(PutTimeSeriesRequest request)
        {
            string uri = _baseUri + "/timeseries";

            string test = JsonConvert.SerializeObject(request);

            StringContent body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            string response = await HttpActionAsync(HttpMethod.Put, uri, body);
            return response;
        }

    }
}
