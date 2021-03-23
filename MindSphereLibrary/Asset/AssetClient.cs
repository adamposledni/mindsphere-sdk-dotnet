using MindSphereLibrary.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MindSphereLibrary.Asset
{
    public class AssetClient : SdkClient
    {
        private string _baseUri = "/api/assetmanagement/v3";

        public AssetClient(AppCredentials credentials, HttpClient httpClient) : base(credentials, httpClient)
        {
            
        }

        public async Task<string> ListAssetsAsync()
        {


            string uri = _baseUri + "/assets";

            string response = await HttpGetRequestAsync(uri);
            return response;
        }
    }
}
