using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MindSphereSdk.Asset;
using MindSphereSdk.Common;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppCredentials appCredentials = new AppCredentials(
                "iiotdgli-testapplication-1.0.0",
                "Rh59aFXcmaq9tUK2cyESaIG9SpSnRKoV3PBjwONtd8G",
                "testapplication",
                "1.0.0",
                "iiotdgli",
                "iiotdgli"
            );

            HttpClient httpClient = new HttpClient();

            AssetClient assetClient = new AssetClient(appCredentials, httpClient);

            List<AssetResponse> test = await assetClient.GetAssetsAsync();
            foreach (var item in test)
            {
                Console.WriteLine(item.AssetId);
                if (item.Location != null) {
                    Console.WriteLine(item.Location.Country);
                }
            }
            
            Console.ReadKey();
        }

    }
}
