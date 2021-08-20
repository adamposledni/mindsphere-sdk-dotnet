using MindSphereSdk.Core.AssetManagement;
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
            AppCredentials appCredentials;

            try
            {
                appCredentials = AppCredentials.FromJsonFile("mdspcreds.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            HttpClient httpClient = new HttpClient();
            AssetManagementClient assetClient = new AssetManagementClient(appCredentials, httpClient);

            ListAssetsRequest request = new ListAssetsRequest()
            {
                Size = 200
            };

            List<Asset> assets;
            try
            {
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
