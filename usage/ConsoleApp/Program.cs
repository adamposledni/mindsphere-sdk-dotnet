using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.IotTimeSeries;
using MindSphereSdk.Core.IotTsAggregates;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppCredentials appCredentials = AppCredentials.FromJsonFile(@"..\..\..\..\..\mdspcreds.json");
            HttpClient httpClient = new HttpClient();

            AssetManagementClient assetClient = new AssetManagementClient(appCredentials, httpClient);
            

            ListAssetsRequest request = new ListAssetsRequest()
            {
                Size = 200
            };
            List<Asset> test = (await assetClient.ListAssetsAsync(request)).ToList();
            foreach (var item in test)
            {
                Console.WriteLine(item.AssetId);
            }
            
            Console.ReadKey();
        }

    }
}
