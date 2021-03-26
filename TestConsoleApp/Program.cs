using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MindSphereLibrary.Asset;
using MindSphereLibrary.Common;

namespace TestConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppCredentials appCredentials = new AppCredentials(
                "vdtcz-comosintegration-1.0.0",
                "4zK0l9GMhSzc777w9wHa1ASgASOLMmvTin9T4OI9T2k",
                "comosintegration",
                "1.0.0",
                "vdtcz",
                "vdtcz"
            );

            AssetClient assetClient = new AssetClient(appCredentials);

            List<AssetResponse> test = await assetClient.ListAssetsAsync();
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
