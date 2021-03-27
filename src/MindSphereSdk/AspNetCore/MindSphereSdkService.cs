using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MindSphereSdk.AssetManagement;
using MindSphereSdk.IotTimeSeries;
using MindSphereSdk.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MindSphereSdk.AspNetCore
{
    public interface IMindSphereSdkService
    {
        AssetManagementClient GetAssetManagementClient();
        IotTimeSeriesClient GetIotTimeSeriesClient();
    }

    /// <summary>
    /// Service for MindSphere SDK
    /// </summary>
    public class MindSphereSdkService : IMindSphereSdkService
    {
        private HttpClient _httpClient;

        private AssetManagementClient _assetManagementClient;
        private IotTimeSeriesClient _iotTimeSeriesClient;

        private ICredentials _credentials;

        public MindSphereSdkService(HttpClient httpClient, IOptions<MindSphereSdkServiceOptions> options)
        {
            _httpClient = httpClient;
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
