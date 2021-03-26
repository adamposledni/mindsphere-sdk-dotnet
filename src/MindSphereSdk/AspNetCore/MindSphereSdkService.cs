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

        public AssetManagementClient GetAssetManagementClient()
        {
            if (_assetManagementClient == null)
            {
                _assetManagementClient = new AssetManagementClient(_credentials, _httpClient);
            }

            return _assetManagementClient;
        }

        public IotTimeSeriesClient GetIotTimeSeriesClient()
        {
            if (_iotTimeSeriesClient == null)
            {
                _iotTimeSeriesClient = new IotTimeSeriesClient(_credentials, _httpClient);
            }

            return _iotTimeSeriesClient;
        }
    }

    public static class MindSphereSdkServiceCollectionExtensions
    {
        public static IServiceCollection AddMindSphereSdkService(this IServiceCollection collection,
            Action<MindSphereSdkServiceOptions> setupAction)
        {
            collection.Configure(setupAction);
            return collection.AddSingleton<IMindSphereSdkService, MindSphereSdkService>();
        }
    }

    public class MindSphereSdkServiceOptions
    {   
        public ICredentials Credentials { get; set; }
    }

}
