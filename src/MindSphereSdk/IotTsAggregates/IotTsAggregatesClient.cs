using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.IotTsAggregates
{
    /// <summary>
    /// Querying aggregated time series data for performance assets based on pre-calculated aggregate values
    /// </summary>
    public class IotTsAggregatesClient : SdkClient
    {
        private readonly string _baseUri = "/api/iottsaggregates/v4";

        public IotTsAggregatesClient(ICredentials credentials, HttpClient httpClient) : base(credentials, httpClient)
        {
        }

        /// <summary>
        /// Get aggregated time series data for one aspect of an asset
        /// </summary>
        public async Task<IEnumerable<T>> GetAggregateTimeSeriesAsync<T>(GetAggregateTimeSeriesRequest request) where T : AggregateSet
        {
            string uri = GetUri(request);

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var tsAggregateWrapper = JsonConvert.DeserializeObject<AggregateWrapper<T>>(response);
            var tsAggregate = tsAggregateWrapper.Aggregates;
            return tsAggregate;
        }

        /// <summary>
        /// Generate URI for time series aggregation request
        /// </summary>
        private string GetUri(GetAggregateTimeSeriesRequest request)
        {
            // prepare query string
            string queryString = "?";

            queryString += request.AssetId != null ? $"assetId={request.AssetId}&" : "";
            queryString += request.AspectName != null ? $"aspectName={request.AspectName}&" : "";
            queryString += request.From != null ? $"from={Helper.GetDateTimeUtcString(request.From.Value)}& " : "";
            queryString += request.To != null ? $"to={Helper.GetDateTimeUtcString(request.To.Value)}&" : "";
            queryString += request.IntervalValue != null ? $"intervalValue={request.IntervalValue}&" : "";
            queryString += request.IntervalUnit != null ? $"intervalUnit={request.IntervalUnit}&" : "";
            queryString += request.Select != null ? $"select={request.Select}&" : "";
            queryString += request.Count != null ? $"count={request.Count}&" : "";

            string uri = _baseUri + "/aggregates" + queryString;
            return uri;
        }
    }
}
