using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MindSphereSdk.AspNetCore;
using MindSphereSdk.AssetManagement;
using MindSphereSdk.IotTimeSeries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class MainController : ControllerBase
    {
        private IMindSphereSdkService _mindSphereSdkService;
        private ILogger<MainController> _logger;

        public MainController(IMindSphereSdkService mindSphereSdkService, ILogger<MainController> logger)
        {
            _mindSphereSdkService = mindSphereSdkService;
            _logger = logger;
        }

        [HttpGet("list-assets")]
        public async Task<ActionResult<IEnumerable<Asset>>> ListAssets()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new ListAssetsRequest()
            {
                Size = 100
            };
            List<Asset> assets = (await assetClient.ListAssetsAsync(request)).ToList();

            return StatusCode(200, assets);
        }

        [HttpGet("get-asset")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAsset()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new GetAssetRequest()
            {
                Id = "73b2a7cdf27241e5b3f29b07266ff602"
            };
            Asset asset = await assetClient.GetAssetAsync(request);

            return StatusCode(200, asset);
        }

        [HttpGet("add-asset")]
        public async Task<ActionResult<Asset>> AddAsset()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new AddAssetRequest()
            {
                Body = new Asset()
                {
                    Name = "MyNewAsset",
                    TypeId = "iiotdgli.mobilephone",
                    ParentId = "ec206f76b04a49a4938c1573b35b6688",
                }
            };
            return StatusCode(200, await assetClient.AddAssetsAsync(request));
        }

        [HttpGet("update-asset")]
        public async Task<ActionResult<Asset>> UpdateAsset()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new UpdateAssetRequest()
            {
                Body = new AssetUpdate()
                {
                    Name = "MyUpdatedAsset"
                },
                Id = "8e775e74a9fa4b4f8fcf15e808d7fb10",
                IfMatch = "2"
            };

            Asset response = await assetClient.UpdateAssetAsync(request);
            return StatusCode(200, response);
        }

        [HttpGet("patch-asset")]
        public async Task<ActionResult<Asset>> PatchAsset()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new UpdateAssetRequest()
            {
                Body = new AssetUpdate()
                {
                    Name = "MyUpdatedAsset"
                },
                Id = "8e775e74a9fa4b4f8fcf15e808d7fb10",
                IfMatch = "4"
            };

            Asset response = await assetClient.PatchAssetAsync(request);
            return StatusCode(200, response);
        }

        [HttpGet("delete-asset")]
        public async Task<ActionResult> DeleteAsset()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new DeleteAssetRequest()
            {
                Id = "8e775e74a9fa4b4f8fcf15e808d7fb10",
                IfMatch = "5"
            };

            await assetClient.DeleteAssetAsync(request);
            return StatusCode(204);
        }

        [HttpGet("move-asset")]
        public async Task<ActionResult<Asset>> MoveAsset()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new MoveAssetRequest()
            {
                Id = "73b2a7cdf27241e5b3f29b07266ff602",
                IfMatch = "1",
                MoveParameters = new AssetMove() { NewParentId = "2d206ad87e9848948a1b5986ec29d028" }
            };

            var response = await assetClient.MoveAssetAsync(request);
            return StatusCode(200, response);
        }

        [HttpGet("get-root-asset")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetRootAsset()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            Asset asset = await assetClient.GetRootAssetAsync();

            return StatusCode(200, asset);
        }

        [HttpGet("save-asset-file-assignment")]
        public async Task<ActionResult<IEnumerable<Asset>>> SaveAssetFileAssignment()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new SaveAssetFileAssignmentRequest()
            {
                Id = "73b2a7cdf27241e5b3f29b07266ff602",
                Key = "testFile",
                FileId = "fe81d2c22a9448eea41d0f460e5a5731",
                IfMatch = "3"
            };

            Asset asset = await assetClient.SaveAssetFileAssignmentAsync(request);
            return StatusCode(200, asset);
        }

        [HttpGet("delete-asset-file-assignment")]
        public async Task<ActionResult<IEnumerable<Asset>>> DeleteAssetFileAssignment()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new DeleteAssetFileAssignmentRequest()
            {
                Id = "73b2a7cdf27241e5b3f29b07266ff602",
                Key = "testFile",
                IfMatch = "4"
            };

            Asset asset = await assetClient.DeleteAssetFileAssignmentAsync(request);
            return StatusCode(200, asset);
        }

        [HttpGet("get-timeseries")]
        public async Task<ActionResult<TestData>> GetTimeSeries()
        {
            var iotClient = _mindSphereSdkService.GetIotTimeSeriesClient();
            var request = new GetTimeSeriesRequest()
            {
                EntityId = "ec206f76b04a49a4938c1573b35b6688",
                PropertySetName = "acceleration",
                From = DateTime.Now.AddDays(-1),
                To = DateTime.Now,
                Limit = 10
            };
            List<TestData> timeSeries = (await iotClient.GetTimeSeriesAsync<TestData>(request)).ToList();
            
            return StatusCode(200, timeSeries);
        }

        [HttpGet("put-timeseries")]
        public async Task<ActionResult> PutTimeSeries()
        {
            var iotClient = _mindSphereSdkService.GetIotTimeSeriesClient();
            DateTime nowUtc = DateTime.Now.ToUniversalTime();

            List<object> timeSeriesData = new List<object>();
            timeSeriesData.Add(new TestData(nowUtc, 0.5, 0.7, 0.3));
            timeSeriesData.Add(new TestData(nowUtc.AddMinutes(1), 0.8, 1.2, 0.7));
            timeSeriesData.Add(new TestData(nowUtc.AddMinutes(2), 1.6, 0.2, 0.5));
            //timeSeriesData.Add(new { _time = DateTime.Now, x = 0.5, y = 0.7, z = 0.3 });
            //timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(1), x = 0.8, y = 1.2, z = 0.7 });
            //timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(2), x = 1.6, y = 0.2, z = 0.5 });

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
            await iotClient.PutTimeSeriesAsync(request);

            return StatusCode(201);
        }

        [HttpGet("delete-timeseries")]
        public async Task<ActionResult> DeleteTimeSeries()
        {
            var iotClient = _mindSphereSdkService.GetIotTimeSeriesClient();
            var request = new DeleteTimeSeriesRequest()
            {
                EntityId = "ec206f76b04a49a4938c1573b35b6688",
                PropertySetName = "acceleration",
                From = new DateTime(2021, 3, 30, 0, 0, 0),
                To = new DateTime(2021, 4, 2, 0, 0, 0)
            };
            await iotClient.DeleteTimeSeriesAsync(request);

            return StatusCode(204);
        }
    }

    public class TestData
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
