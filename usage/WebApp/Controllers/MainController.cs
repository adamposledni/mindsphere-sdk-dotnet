using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MindSphereSdk.AspNetCore;
using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.EventManagement;
using MindSphereSdk.Core.IotTimeSeries;
using MindSphereSdk.Core.IotTsAggregates;
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

        #region Asset

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
                Body = new AssetAdd()
                {
                    Name = "MyNewAsset",
                    TypeId = "iiotdgli.mobilephone",
                    ParentId = "ec206f76b04a49a4938c1573b35b6688",
                }
            };
            return StatusCode(200, await assetClient.AddAssetAsync(request));
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

        #endregion

        #region Time Series

        [HttpGet("get-timeseries")]
        public async Task<ActionResult<IEnumerable<TestTimeSeriesData>>> GetTimeSeries()
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
            List<TestTimeSeriesData> timeSeries = (await iotClient.GetTimeSeriesAsync<TestTimeSeriesData>(request)).ToList();
            
            return StatusCode(200, timeSeries);
        }

        [HttpGet("put-timeseries")]
        public async Task<ActionResult> PutTimeSeries()
        {
            var iotClient = _mindSphereSdkService.GetIotTimeSeriesClient();
            DateTime nowUtc = DateTime.Now.ToUniversalTime();

            List<TestTimeSeriesData> timeSeriesData = new List<TestTimeSeriesData>();
            timeSeriesData.Add(new TestTimeSeriesData(nowUtc, 0.5, 0.7, 0.3));
            timeSeriesData.Add(new TestTimeSeriesData(nowUtc.AddMinutes(1), 0.8, 1.2, 0.7));
            timeSeriesData.Add(new TestTimeSeriesData(nowUtc.AddMinutes(2), 1.6, 0.2, 0.5));
            //timeSeriesData.Add(new { _time = DateTime.Now, x = 0.5, y = 0.7, z = 0.3 });
            //timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(1), x = 0.8, y = 1.2, z = 0.7 });
            //timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(2), x = 1.6, y = 0.2, z = 0.5 });

            List<TimeSeries> timeSeriesObjects = new List<TimeSeries>();
            timeSeriesObjects.Add(new TimeSeries()
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

        #endregion

        #region Aggregate Time Series

        [HttpGet("get-aggregate-timeseries")]
        public async Task<ActionResult<IEnumerable<TestAggregateTsData>>> GetAggregateTimeSeries()
        {
            var iotAggregClient = _mindSphereSdkService.GetIotTsAggregateClient();
            var request = new GetAggregateTimeSeriesRequest()
            {
                AssetId = "ec206f76b04a49a4938c1573b35b6688",
                AspectName = "acceleration",
                From = new DateTime(2021, 4, 25, 0, 0, 0),
                To = new DateTime(2021, 4, 26, 0, 0, 0),
                IntervalUnit = "minute",
                IntervalValue = 2
            };

            var tsAggregate = await iotAggregClient.GetAggregateTimeSeriesAsync<TestAggregateTsData>(request);
            return StatusCode(200, tsAggregate);
        }

        #endregion

        #region Aspect Type

        [HttpGet("list-aspect-types")]
        public async Task<ActionResult<IEnumerable<AspectType>>> ListAspectTypes()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new ListAspectTypesRequest()
            {
                Size = 100
            };
            List<AspectType> aspectTypes = (await assetClient.ListAspectTypesAsync(request)).ToList();

            return StatusCode(200, aspectTypes);
        }

        [HttpGet("get-aspect-types")]
        public async Task<ActionResult<AspectType>> GetAspectType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new GetAspectTypeRequest()
            {
                Id = "iiotdgli.acceleration"
            };
            AspectType aspectTypes = await assetClient.GetAspectTypeAsync(request);

            return StatusCode(200, aspectTypes);
        }

        [HttpGet("delete-aspect-types")]
        public async Task<ActionResult> DeleteAspectType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new DeleteAspectTypeRequest()
            {
                Id = "iiotdgli.My_new_asset",
                IfMatch = "0"
            };
            await assetClient.DeleteAspectTypeAsync(request);

            return StatusCode(204);
        }

        [HttpGet("add-aspect-types")]
        public async Task<ActionResult<AspectType>> AddAspectType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();

            var newAspectType = new AspectTypeUpdate()
            {
                Name = "My_new_asset",
                Category = "static",
                Description = "Test",
                Scope = "private",
                Variables = new VariableDetail[] {
                    new VariableDetail() 
                    {
                        Name = "velocity",
                        Unit = "m/s",
                        DataType = "DOUBLE"
                    },
                }
            };

            var request = new PutAspectTypeRequest()
            {
                Id = "iiotdgli.My_new_asset",
                IfNoneMatch = "*",
                Body = newAspectType
            };
            var aspectType = await assetClient.PutAspectTypeAsync(request);

            return StatusCode(200, aspectType);
        }

        [HttpGet("update-aspect-types")]
        public async Task<ActionResult<AspectType>> UpdateAspectType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();

            var updatedAspectType = new AspectTypeUpdate()
            {
                Name = "My_new_aspect_type",
                Category = "static",
                Description = "Updated test",
                Scope = "private",
                Variables = new VariableDetail[] {
                    new VariableDetail()
                    {
                        Name = "velocity",
                        Unit = "m/s",
                        DataType = "DOUBLE"
                    },
                }
            };

            var request = new PutAspectTypeRequest()
            {
                Id = "iiotdgli.My_new_aspect_type",
                IfMatch = "0",
                Body = updatedAspectType
            };
            var aspectType = await assetClient.PutAspectTypeAsync(request);

            return StatusCode(200, aspectType);
        }

        [HttpGet("patch-aspect-types")]
        public async Task<ActionResult<AspectType>> PatchAspectType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();

            var updatedAspectType = new AspectTypeUpdate()
            {
                Description = "Patched test"
            };

            var request = new PatchAspectTypeRequest()
            {
                Id = "iiotdgli.My_new_aspect_type",
                IfMatch = "1",
                Body = updatedAspectType
            };
            var aspectType = await assetClient.PatchAspectTypeAsync(request);

            return StatusCode(200, aspectType);
        }

        #endregion

        #region Asset Type

        [HttpGet("list-asset-types")]
        public async Task<ActionResult<IEnumerable<AssetType>>> ListAssetTypes()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new ListAssetTypesRequest()
            {
                Size = 100
            };
            List<AssetType> aspectTypes = (await assetClient.ListAssetTypesAsync(request)).ToList();

            return StatusCode(200, aspectTypes);
        }

        [HttpGet("get-asset-type")]
        public async Task<ActionResult<AssetType>> GetAssetType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new GetAssetTypeRequest()
            {
                Id = "core.basicagent"
            };
            AssetType assetType = await assetClient.GetAssetTypeAsync(request);

            return StatusCode(200, assetType);
        }

        [HttpGet("add-asset-type")]
        public async Task<ActionResult<AssetType>> AddAssetType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();

            var aspects = new List<AspectPut>();
            aspects.Add(new AspectPut() { Name = "acceleration", AspectTypeId = "iiotdgli.acceleration" });

            var newAssetType = new AssetTypeUpdate()
            {
                Name = "My_new_asset_type",
                Description = "New asset type",
                ParentTypeId = "core.basicasset",
                Instantiable = true,
                Scope = "private",
                Aspects = aspects
            };

            var request = new PutAssetTypeRequest()
            {
                Id = "iiotdgli.My_new_asset_type",
                IfNoneMatch = "*",
                Body = newAssetType
            };
            var assetType = await assetClient.PutAssetTypeAsync(request);

            return StatusCode(200, assetType);
        }

        [HttpGet("update-asset-type")]
        public async Task<ActionResult<AssetType>> UpdateAssetType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();

            var aspects = new List<AspectPut>();
            aspects.Add(new AspectPut() { Name = "acceleration", AspectTypeId = "iiotdgli.acceleration" });

            var newAssetType = new AssetTypeUpdate()
            {
                Name = "My_new_asset_type",
                Description = "Updated asset type",
                ParentTypeId = "core.basicasset",
                Instantiable = true,
                Scope = "private",
                Aspects = aspects
            };

            var request = new PutAssetTypeRequest()
            {
                Id = "iiotdgli.My_new_asset_type",
                IfMatch = "0",
                Body = newAssetType
            };
            var assetType = await assetClient.PutAssetTypeAsync(request);

            return StatusCode(200, assetType);
        }

        [HttpGet("patch-asset-type")]
        public async Task<ActionResult<AssetType>> PatchAssetType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();

            var updatedAssetType = new AssetTypeUpdate()
            {
                Description = "Patched asset type"
            };

            var request = new PatchAssetTypeRequest()
            {
                Id = "iiotdgli.My_new_asset_type",
                IfMatch = "1",
                Body = updatedAssetType
            };
            var assetType = await assetClient.PatchAssetTypeAsync(request);

            return StatusCode(200, assetType);
        }

        [HttpGet("delete-asset-type")]
        public async Task<ActionResult<AssetType>> DeleteAssetType()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();

            var request = new DeleteAssetTypeRequest()
            {
                Id = "iiotdgli.My_new_asset_type",
                IfMatch = "2"
            };
            await assetClient.DeleteAssetTypeAsync(request);

            return StatusCode(204);
        }

        [HttpGet("save-asset-type-file-assignment")]
        public async Task<ActionResult<IEnumerable<AssetType>>> SaveAssetTypeFileAssignment()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new SaveAssetTypeFileAssignmentRequest()
            {
                Id = "iiotdgli.My_new_asset_type",
                Key = "testFile",
                FileId = "fe81d2c22a9448eea41d0f460e5a5731",
                IfMatch = "0"
            };

            AssetType assetType = await assetClient.SaveAssetTypeFileAssignmentAsync(request);
            return StatusCode(200, assetType);
        }

        [HttpGet("delete-asset-type-file-assignment")]
        public async Task<ActionResult<IEnumerable<AssetType>>> DeleteAssetTypeFileAssignment()
        {
            var assetClient = _mindSphereSdkService.GetAssetManagementClient();
            var request = new DeleteAssetTypeFileAssignmentRequest()
            {
                Id = "iiotdgli.My_new_asset_type",
                Key = "testFile",
                IfMatch = "1"
            };

            AssetType assetType = await assetClient.DeleteAssetTypeFileAssignmentAsync(request);
            return StatusCode(200, assetType);
        }

        #endregion

        #region Event

        [HttpGet("add-event")]
        public async Task<ActionResult<Event>> AddEvent()
        {
            var eventClient = _mindSphereSdkService.GetEventManagementClient();
            var request = new AddEventRequest()
            {
                Body = new MyEventAdd()
                {
                    EntityId = "ec206f76b04a49a4938c1573b35b6688",
                    Timestamp = DateTime.Now.ToUniversalTime(),
                    Description = "Error happened in the test",
                    Severity = 5

                }
            };
            return StatusCode(200, await eventClient.AddEventAsync<MyEvent>(request));
        }

        #endregion
    }

    public class TestTimeSeriesData
    {
        [JsonProperty("_time")]
        public DateTime Time { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }

        public TestTimeSeriesData(DateTime time, double x, double y, double z)
        {
            Time = time;
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class TestAggregateTsData : AggregateSet
    {
        [JsonProperty("x")]
        public AggregateVariable X { get; set; }

        [JsonProperty("y")]
        public AggregateVariable Y { get; set; }

        [JsonProperty("z")]
        public AggregateVariable Z { get; set; }
    }

    public class MyEventAdd : EventAdd
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("severity")]
        public int Severity { get; set; }   
    }

    public class MyEvent : Event
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("severity")]
        public int Severity { get; set; }
    }
}
