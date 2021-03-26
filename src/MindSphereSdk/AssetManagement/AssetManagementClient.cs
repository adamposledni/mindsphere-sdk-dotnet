using MindSphereSdk.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.AssetManagement
{
    public class AssetManagementClient : SdkClient
    {
        private readonly string _baseUri = "/api/assetmanagement/v3";

        public AssetManagementClient(ICredentials credentials, HttpClient httpClient) : base(credentials, httpClient)
        {

        }

        /// <summary>
        /// List all available assets
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<AssetResource>> ListAssetsAsync(ListAssetsRequest request = null)
        {
            string queryString = "?";
            queryString += request.Size != null ? $"size={request.Size}&" : "";
            queryString += request.Page != null ? $"page={request.Page}&" : "";
            queryString += request.Sort != null ? $"sort={request.Sort}&" : "";
            queryString += request.Filter != null ? $"filter={request.Filter}&" : "";

            string uri = _baseUri + "/assets" + queryString;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var assetListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedAssetListResource>>(response);
            var assetList = assetListWrapper.Embedded.Assets.ToList();

            return assetList;
        }

        /// <summary>
        /// Create an asset
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AssetResource> AddAssetsAsync(AddAssetRequest request)
        {
            string uri = _baseUri + "/assets";
            StringContent body = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json");

            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var asset = JsonConvert.DeserializeObject<AssetResource>(response);

            return asset;
        }
    }
}
