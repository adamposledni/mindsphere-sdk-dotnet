using MindSphereSdk.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Asset
{
    public class AssetClient : SdkClient
    {
        private readonly string _baseUri = "/api/assetmanagement/v3";

        public AssetClient(ICredentials credentials, HttpClient httpClient) : base(credentials, httpClient)
        {

        }

        public async Task<List<AssetResponse>> ListAssetsAsync()
        {
            string uri = _baseUri + "/assets";

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var responseWrapper = JsonConvert.DeserializeObject<MindSphereResponseWrapper<EmbeddedAssetResponse>>(response);
            var assetList = responseWrapper.Embedded.Assets.ToList();

            return assetList;
        }
    }
}
