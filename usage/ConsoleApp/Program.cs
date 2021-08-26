using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.Authentication;
using MindSphereSdk.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppCredentials credentials;
            ClientConfiguration configuration = new ClientConfiguration();
            ListAssetsRequest request = new ListAssetsRequest()
            {
                Size = 200
            };

            List<Asset> assets;
            try
            {
                credentials = AppCredentials.FromJsonFile("mdspcreds.json");
                var sdk = new MindSphereApiSdk(credentials, configuration);
                var assetClient = sdk.GetAssetManagementClient();
                assets = (await assetClient.ListAssetsAsync(request)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            foreach (var asset in assets)
            {
                Console.WriteLine(asset.AssetId);
            }
            Console.ReadKey();
        }

    }
}
