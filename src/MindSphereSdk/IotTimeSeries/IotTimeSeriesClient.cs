using MindSphereSdk.Common;
using System;
using System.Collections.Generic;
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

        public IotTimeSeriesClient(ICredentials credentials, HttpClient httpClient) : base (credentials, httpClient)
        {

        }

        public async Task GetTimeSeriesAsync()
        {

        }
        public async Task PutTimeSeriesAsync()
        {

        }

    }
}
