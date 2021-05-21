using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.EventManagement;
using MindSphereSdk.Core.IotTimeSeries;
using MindSphereSdk.Core.IotTsAggregates;
using System;
using System.Net.Http;

namespace MindSphereSdk.AspNetCore
{
    public interface IMindSphereSdkService
    {
        AssetManagementClient GetAssetManagementClient();
        IotTimeSeriesClient GetIotTimeSeriesClient();
        IotTsAggregatesClient GetIotTsAggregateClient();
        EventManagementClient GetEventManagementClient();

    }

    /// <summary>
    /// Service for MindSphere SDK
    /// </summary>
    public class MindSphereSdkService : IMindSphereSdkService
    {
        private HttpClient _httpClient;

        private AssetManagementClient _assetManagementClient;
        private IotTimeSeriesClient _iotTimeSeriesClient;
        private IotTsAggregatesClient _iotTsAggregateClient;
        private EventManagementClient _eventManagementClient;

        private ICredentials _credentials;

        public MindSphereSdkService(IHttpClientFactory clientFactory, IOptions<MindSphereSdkServiceOptions> options)
        {
            _httpClient = clientFactory.CreateClient();
            _credentials = options.Value.Credentials;
        }

        /// <summary>
        /// Get Asset Management Client
        /// </summary>
        public AssetManagementClient GetAssetManagementClient()
        {
            if (_assetManagementClient == null)
            {
                _assetManagementClient = new AssetManagementClient(_credentials, _httpClient);
            }

            return _assetManagementClient;
        }
        
        /// <summary>
        /// Get IoT Time Series Client
        /// </summary>
        public IotTimeSeriesClient GetIotTimeSeriesClient()
        {
            if (_iotTimeSeriesClient == null)
            {
                _iotTimeSeriesClient = new IotTimeSeriesClient(_credentials, _httpClient);
            }

            return _iotTimeSeriesClient;
        }

        /// <summary>
        /// Get IoT Time Series Aggregate Client
        /// </summary>
        public IotTsAggregatesClient GetIotTsAggregateClient()
        {
            if (_iotTsAggregateClient == null)
            {
                _iotTsAggregateClient = new IotTsAggregatesClient(_credentials, _httpClient);
            }

            return _iotTsAggregateClient;
        }

        /// <summary>
        /// Get Event Management Client
        /// </summary>
        public EventManagementClient GetEventManagementClient()
        {
            if (_eventManagementClient == null)
            {
                _eventManagementClient = new EventManagementClient(_credentials, _httpClient);
            }

            return _eventManagementClient;
        }
    }

    /// <summary>
    /// Service Collection Extension for MindSphere SDK
    /// </summary>
    public static class MindSphereSdkServiceCollectionExtensions
    {
        /// <summary>
        /// Add MindSphere SDK service to the Service Collection
        /// </summary>
        public static IServiceCollection AddMindSphereSdkService(this IServiceCollection collection,
            Action<MindSphereSdkServiceOptions> setupAction)
        {
            collection.Configure(setupAction);
            return collection.AddSingleton<IMindSphereSdkService, MindSphereSdkService>();
        }
    }

    /// <summary>
    /// Options for MindSphere SDK service
    /// </summary>
    public class MindSphereSdkServiceOptions
    {   
        public ICredentials Credentials { get; set; }
    }
}
