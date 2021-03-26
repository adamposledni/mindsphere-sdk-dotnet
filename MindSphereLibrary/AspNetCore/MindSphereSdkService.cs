using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MindSphereLibrary.Asset;
using MindSphereLibrary.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MindSphereLibrary.AspNetCore
{
    public interface IMindSphereSdkService
    {
        AssetClient GetAssetClient();
    }

    public class MindSphereSdkService : IMindSphereSdkService
    {
        private HttpClient _httpClient;

        private AssetClient _assetClient;

        private ICredentials _credentials;

        public MindSphereSdkService(HttpClient httpClient, IOptions<MindSphereSdkServiceOptions> options)
        {
            _httpClient = httpClient;
            _credentials = options.Value.Credentials;
        }

        public AssetClient GetAssetClient()
        {
            if (_assetClient == null)
            {
                _assetClient = new AssetClient(_credentials, _httpClient);
            }

            return _assetClient;
        }
    }

    public static class MindSphereSdkServiceCollectionExtensions
    {
        public static IServiceCollection AddMindSphereSdkService(this IServiceCollection collection,
            Action<MindSphereSdkServiceOptions> setupAction)
        {
            collection.Configure(setupAction);
            return collection.AddScoped<IMindSphereSdkService, MindSphereSdkService>();
        }
    }

    public class MindSphereSdkServiceOptions
    {   
        public ICredentials Credentials { get; set; }
    }

}
