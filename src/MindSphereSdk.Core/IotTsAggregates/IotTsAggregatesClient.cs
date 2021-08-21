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

        public IotTsAggregatesClient(ICredentials credentials, ClientConfiguration configuration, HttpClient httpClient) 
           : base(credentials, configuration, httpClient)
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
            QueryStringBuilder queryBuilder = new QueryStringBuilder();
            queryBuilder.AddQuery("from", request.From);
            queryBuilder.AddQuery("to", request.To);
            queryBuilder.AddQuery("select", request.Select);
            queryBuilder.AddQuery("assetId", request.AssetId);
            queryBuilder.AddQuery("aspectName", request.AspectName);
            queryBuilder.AddQuery("intervalValue", request.IntervalValue);
            queryBuilder.AddQuery("intervalUnit", request.IntervalUnit);
            queryBuilder.AddQuery("count", request.Count);

            string uri = _baseUri + "/aggregates" + queryBuilder.ToString();
            return uri;
        }
    }
}
