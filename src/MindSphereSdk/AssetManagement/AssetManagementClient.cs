using MindSphereSdk.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereSdk.Core.AssetManagement
{
    /// <summary>
    /// Configuring, reading and managing assets, asset types and aspect types
    /// </summary>
    public class AssetManagementClient : SdkClient
    {
        private readonly string _baseUri = "/api/assetmanagement/v3";

        public AssetManagementClient(ICredentials credentials, HttpClient httpClient) : base(credentials, httpClient)
        {

        }

        #region Asset

        /// <summary>
        /// List all available assets
        /// </summary>
        public async Task<IEnumerable<Asset>> ListAssetsAsync(ListAssetsRequest request = null)
        {
            // prepare query string
            string queryString = "?";
            queryString += request.Size != null ? $"size={request.Size}&" : "";
            queryString += request.Page != null ? $"page={request.Page}&" : "";
            queryString += request.Sort != null ? $"sort={request.Sort}&" : "";
            queryString += request.Filter != null ? $"filter={request.Filter}&" : "";

            string uri = _baseUri + "/assets" + queryString;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var assetListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedAssetList>>(response);
            var assetList = assetListWrapper.Embedded.Assets;

            return assetList;
        }

        /// <summary>
        /// Create an asset
        /// </summary>
        public async Task<Asset> AddAssetAsync(AddAssetRequest request)
        {
            string uri = _baseUri + "/assets";

            // prepare HTTP request body
            StringContent body = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json");

            string response = await HttpActionAsync(HttpMethod.Post, uri, body);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Read a single asset 
        /// </summary>
        public async Task<Asset> GetAssetAsync(GetAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Update an asset
        /// </summary>
        public async Task<Asset> UpdateAssetAsync(UpdateAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body, 
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);
            
            return asset;
        }

        /// <summary>
        /// Patch an asset
        /// </summary>
        public async Task<Asset> PatchAssetAsync(UpdateAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Delete an asset 
        /// </summary>
        public async Task DeleteAssetAsync(DeleteAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        /// <summary>
        /// Move an asset 
        /// </summary>
        public async Task<Asset> MoveAssetAsync(MoveAssetRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id + "/move";

            // prepare HTTP request body
            StringContent body = new StringContent(JsonConvert.SerializeObject(request.MoveParameters), Encoding.UTF8, "application/json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            string response = await HttpActionAsync(HttpMethod.Post, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);
            return asset;
        }

        /// <summary>
        /// Read the root asset of the user 
        /// </summary>
        public async Task<Asset> GetRootAssetAsync()
        {
            string uri = _baseUri + "/assets/root";

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Save a file assignment to an asset
        /// </summary>
        public async Task<Asset> SaveAssetFileAssignmentAsync(SaveAssetFileAssignmentRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id + "/fileAssignments/" + request.Key;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            // prepare HTTP request body
            object fileIdObject = new { fileId = request.FileId };
            StringContent body = new StringContent(JsonConvert.SerializeObject(fileIdObject), Encoding.UTF8, "application/json");

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        /// <summary>
        /// Delete a file assignment from an asset
        /// </summary>
        public async Task<Asset> DeleteAssetFileAssignmentAsync(DeleteAssetFileAssignmentRequest request)
        {
            string uri = _baseUri + "/assets/" + request.Id + "/fileAssignments/" + request.Key;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            string response = await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
            var asset = JsonConvert.DeserializeObject<Asset>(response);

            return asset;
        }

        #endregion

        #region Aspect type

        /// <summary>
        /// List all aspect types
        /// </summary>
        public async Task<IEnumerable<AspectType>> ListAspectTypesAsync(ListAspectTypesRequest request)
        {
            // prepare query string
            string queryString = "?";
            queryString += request.Size != null ? $"size={request.Size}&" : "";
            queryString += request.Page != null ? $"page={request.Page}&" : "";
            queryString += request.Sort != null ? $"sort={request.Sort}&" : "";
            queryString += request.Filter != null ? $"filter={request.Filter}&" : "";

            string uri = _baseUri + "/aspecttypes" + queryString;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var aspectTypeListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedAspectTypeList>>(response);
            var aspectTypeList = aspectTypeListWrapper.Embedded.AspectTypes;

            return aspectTypeList;
        }

        /// <summary>
        /// Read an aspect type 
        /// </summary>
        public async Task<AspectType> GetAspectTypeAsync(GetAspectTypeRequest request)
        {
            string uri = _baseUri + "/aspecttypes/" + request.Id;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var aspectType = JsonConvert.DeserializeObject<AspectType>(response);

            return aspectType;
        }

        /// <summary>
        /// Delete an aspect type 
        /// </summary>
        public async Task DeleteAspectTypeAsync(DeleteAspectTypeRequest request)
        {
            string uri = _baseUri + "/aspecttypes/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        /// <summary>
        /// Create or update an aspect type
        /// </summary>
        public async Task<AspectType> PutAspectTypeAsync(PutAspectTypeRequest request)
        {
            string uri = _baseUri + "/aspecttypes/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body);
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch));
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var aspectType = JsonConvert.DeserializeObject<AspectType>(response);

            return aspectType;
        }

        /// <summary>
        /// Patch an aspect type
        /// </summary>
        public async Task<AspectType> PatchAspectTypeAsync(PatchAspectTypeRequest request)
        {
            string uri = _baseUri + "/aspecttypes/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var aspectType = JsonConvert.DeserializeObject<AspectType>(response);

            return aspectType;
        }

        #endregion

        #region Asset type

        /// <summary>
        /// List all asset types
        /// </summary>
        public async Task<IEnumerable<AssetType>> ListAssetTypesAsync(ListAssetTypesRequest request)
        {
            // prepare query string
            string queryString = "?";
            queryString += request.Size != null ? $"size={request.Size}&" : "";
            queryString += request.Page != null ? $"page={request.Page}&" : "";
            queryString += request.Sort != null ? $"sort={request.Sort}&" : "";
            queryString += request.Filter != null ? $"filter={request.Filter}&" : "";

            string uri = _baseUri + "/assettypes" + queryString;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var assetTypeListWrapper = JsonConvert.DeserializeObject<MindSphereResourceWrapper<EmbeddedAssetTypeList>>(response);
            var assetTypeList = assetTypeListWrapper.Embedded.AssetTypes;

            return assetTypeList;
        }

        /// <summary>
        /// Read an asset type 
        /// </summary>
        public async Task<AssetType> GetAssetTypeAsync(GetAssetTypeRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id;

            string response = await HttpActionAsync(HttpMethod.Get, uri);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Create or update an asset type
        /// </summary>
        public async Task<AssetType> PutAssetTypeAsync(PutAssetTypeRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-None-Match", request.IfNoneMatch));
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            string response = await HttpActionAsync(HttpMethod.Put, uri, body, headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Patch an asset type
        /// </summary>
        public async Task<AssetType> PatchAssetTypeAsync(PatchAssetTypeRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id;

            // prepare HTTP request body
            string jsonString = JsonConvert.SerializeObject(request.Body,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            StringContent body = new StringContent(jsonString, Encoding.UTF8, "application/merge-patch+json");

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            string response = await HttpActionAsync(new HttpMethod("PATCH"), uri, body, headers);
            var assetType = JsonConvert.DeserializeObject<AssetType>(response);

            return assetType;
        }

        /// <summary>
        /// Delete an asset type 
        /// </summary>
        public async Task DeleteAssetTypeAsync(DeleteAssetTypeRequest request)
        {
            string uri = _baseUri + "/assettypes/" + request.Id;

            // prepare HTTP request headers
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("If-Match", request.IfMatch));

            await HttpActionAsync(HttpMethod.Delete, uri, headers: headers);
        }

        #endregion
    }
}
