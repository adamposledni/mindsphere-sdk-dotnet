using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MindSphereSdk.AspNetCore;
using MindSphereSdk.AssetManagement;
using MindSphereSdk.IotTimeSeries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class MainController : ControllerBase
    {
        IMindSphereSdkService _mindSphereSdkService;

        public MainController(IMindSphereSdkService mindSphereSdkService)
        {
            _mindSphereSdkService = mindSphereSdkService;
        }

        [HttpGet("list-assets")]
        public async Task<ActionResult<IEnumerable<AssetResource>>> ListAssets()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new ListAssetsRequest()
            {
                Size = 1
            };
            return StatusCode(200, await assetClient.ListAssetsAsync(request));
        }

        [HttpGet("add-asset")]
        public async Task<ActionResult<AssetResource>> AddAsset()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new AddAssetRequest()
            {
                Body = new AssetResource()
                {
                    Name = "MyNewAsset",
                    TypeId = "iiotdgli.mobilephone",
                    ParentId = "ec206f76b04a49a4938c1573b35b6688",
                }
            };
            return StatusCode(200, await assetClient.AddAssetsAsync(request));
        }

        [HttpGet("get-time-series")]
        public async Task<ActionResult<string>> GetTimeSeries()
        {
            var iotClient = _mindSphereSdkService.GetIotTimeSeriesClient();
            var request = new GetTimeSeriesRequest()
            {
                EntityId = "ec206f76b04a49a4938c1573b35b6688",
                PropertySetName = "acceleration"
            };
            return StatusCode(200, await iotClient.GetTimeSeriesAsync(request));
        }

        [HttpGet("put-time-series")]
        public async Task<ActionResult<string>> PutTimeSeries()
        {
            var iotClient = _mindSphereSdkService.GetIotTimeSeriesClient();



            List<TestData> timeSeriesData = new List<TestData>();
            timeSeriesData.Add(new TestData(DateTime.Now, 0.5, 0.7, 0.3));
            timeSeriesData.Add(new TestData(DateTime.Now.AddMinutes(1), 0.8, 1.2, 0.7));
            timeSeriesData.Add(new TestData(DateTime.Now.AddMinutes(2), 1.6, 0.2, 0.5));

            List<TimeSeriesObject> timeSeriesObjects = new List<TimeSeriesObject>();
            timeSeriesObjects.Add(new TimeSeriesObject()
            {
                EntityId = "ec206f76b04a49a4938c1573b35b6688",
                PropertySetName = "acceleration",
                Data = timeSeriesData
            });

            PutTimeSeriesRequest request = new PutTimeSeriesRequest()
            {
                TimeSeries = timeSeriesObjects
            };

            return StatusCode(200, await iotClient.PutTimeSeriesAsync(request));
        }
    }

    public class TestData : ITimeSeriesData
    {
        [JsonProperty("_time")]
        public DateTime Time { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }

        public TestData(DateTime time, double x, double y, double z)
        {
            Time = time;
            X = x;
            Y = y;
            Z = z;
        }
    }
}
