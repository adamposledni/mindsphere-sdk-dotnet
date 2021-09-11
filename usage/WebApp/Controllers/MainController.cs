using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MindSphereSdk.Core.AssetManagement;
using MindSphereSdk.Core.Common;
using MindSphereSdk.Core.EventManagement;
using MindSphereSdk.Core.IotTimeSeries;
using MindSphereSdk.Core.IotTsAggregates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = MindSphereSdk.Core.AssetManagement.File;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class MainController : ControllerBase
    {
        private readonly MindSphereApiSdk _sdk;

        public MainController(MindSphereApiSdk sdk)
        {
            _sdk = sdk;
        }

        #region Asset

        [HttpGet("list-assets")]
        public async Task<ActionResult<ResourceList<Asset>>> ListAssets()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new ListAssetsRequest()
            {
                Size = 1,
                Page = 2
            };
            var res = await assetClient.ListAssetsAsync(request);

            return StatusCode(200, res);
        }

        [HttpGet("get-asset")]
        public async Task<ActionResult<Asset>> GetAsset()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new GetAssetRequest()
            {
                Id = "da4aabbd3f2f488da7ef75fa506a8eaa"
            };
            Asset asset = await assetClient.GetAssetAsync(request);

            return StatusCode(200, asset);
        }

        [HttpGet("add-asset")]
        public async Task<ActionResult<Asset>> AddAsset()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new AddAssetRequest()
            {
                Asset = new AssetAdd()
                {
                    Name = "DotnetSdkAsset",
                    TypeId = "prsdevex.Dotnet_sdk",
                    ParentId = "11d4d30f87534500842d132233fffa46",
                }
            };
            return StatusCode(200, await assetClient.AddAssetAsync(request));
        }

        [HttpGet("update-asset")]
        public async Task<ActionResult<Asset>> UpdateAsset()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new UpdateAssetRequest()
            {
                Asset = new AssetUpdate()
                {
                    Name = "DotnetSdkAsset"
                },
                Id = "da4aabbd3f2f488da7ef75fa506a8eaa",
                IfMatch = "2"
            };

            Asset response = await assetClient.PutAssetAsync(request);
            return StatusCode(200, response);
        }

        [HttpGet("patch-asset")]
        public async Task<ActionResult<Asset>> PatchAsset()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new UpdateAssetRequest()
            {
                Asset = new AssetUpdate()
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
            var assetClient = _sdk.GetAssetManagementClient();
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
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new MoveAssetRequest()
            {
                Id = "73b2a7cdf27241e5b3f29b07266ff602",
                IfMatch = "1",
                NewParentId = "2d206ad87e9848948a1b5986ec29d028"
            };

            var response = await assetClient.MoveAssetAsync(request);
            return StatusCode(200, response);
        }

        [HttpGet("get-root-asset")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetRootAsset()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            Asset asset = await assetClient.GetRootAssetAsync(new GetRootAssetRequest());

            return StatusCode(200, asset);
        }

        [HttpGet("save-asset-file-assignment")]
        public async Task<ActionResult<IEnumerable<Asset>>> SaveAssetFileAssignment()
        {
            var assetClient = _sdk.GetAssetManagementClient();
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
            var assetClient = _sdk.GetAssetManagementClient();
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

        #region Structure

        [HttpGet("list-asset-variables")]
        public async Task<ActionResult<IEnumerable<VariableDetail>>> ListAssetVariables()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new ListAssetVariablesRequest()
            {
                Id = "8e16eb2d33774c22b94d9fa4792753b9"
            };
            var variables = (await assetClient.ListAssetVariablesAsync(request)).Data.ToList();

            return StatusCode(200, variables);
        }

        [HttpGet("list-asset-apects")]
        public async Task<ActionResult<IEnumerable<AspectFullDetail>>> ListAssetAspects()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new ListAssetAspectsRequest()
            {
                Id = "8e16eb2d33774c22b94d9fa4792753b9"
            };
            var aspects = (await assetClient.ListAssetAspectsAsync(request)).Data.ToList();

            return StatusCode(200, aspects);
        }

        #endregion

        #region Location

        [HttpGet("put-asset-location")]
        public async Task<ActionResult<Asset>> PutAssetLocation()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new PutAssetLocationRequest()
            {
                Location = new Location()
                {
                    Country = "Czech Republic",
                    StreetAddress = "Makova",
                    Region = "Ustecky kraj",
                    PostalCode = "400 11"
                },
                Id = "8e16eb2d33774c22b94d9fa4792753b9",
                IfMatch = "1"
            };

            Asset response = await assetClient.PutAssetLocationAsync(request);
            return StatusCode(200, response);
        }

        [HttpGet("delete-asset-location")]
        public async Task<ActionResult<Asset>> DeleteAssetLocation()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new DeleteAssetLocationRequest()
            {
                Id = "8e16eb2d33774c22b94d9fa4792753b9",
                IfMatch = "2"
            };

            Asset response = await assetClient.DeleteAssetLocationAsync(request);
            return StatusCode(200, response);
        }

        #endregion

        #region File

        [HttpGet("list-files")]
        public async Task<ActionResult<IEnumerable<File>>> ListFiles()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new ListFilesRequest()
            {
                Size = 100
            };
            var files = (await assetClient.ListFilesAsync(request)).Data.ToList();

            return StatusCode(200, files);
        }

        [HttpGet("get-file")]
        public async Task<ActionResult<File>> GetFile()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new GetFileRequest()
            {
                Id = "fe81d2c22a9448eea41d0f460e5a5731"
            };
            var file = await assetClient.GetFileAsync(request);

            return StatusCode(200, file);
        }

        [HttpGet("download-file")]
        public async Task<ActionResult<string>> DownloadFile()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new DownloadFileRequest()
            {
                Id = "d50316fdc608471c97cbe9a92a7ac4fc"
            };
            string fileContent = await assetClient.DownloadFileAsync(request);

            return StatusCode(200, fileContent);
        }

        [HttpGet("delete-file")]
        public async Task<ActionResult> DeleteFile()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new DeleteFileRequest()
            {
                Id = "fe81d2c22a9448eea41d0f460e5a5731",
                IfMatch = "0"
            };
            await assetClient.DeleteFileAsync(request);

            return StatusCode(204);
        }

        [HttpGet("upload-file")]
        public async Task<ActionResult<File>> UploadFile()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            File file;
            using (var fs = new FileStream("test.txt", FileMode.Open))
            {
                var request = new UploadFileRequest()
                {
                    File = fs,
                    Name = "test.txt"
                };
                file = await assetClient.UploadFileAsync(request);
            }

            return StatusCode(200, file);
        }

        [HttpGet("update-file")]
        public async Task<ActionResult<File>> UpdateFile()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var fs = new FileStream("updated-test.txt", FileMode.Open);

            var request = new UpdateFileRequest()
            {
                Id = "d50316fdc608471c97cbe9a92a7ac4fc",
                File = fs,
                Name = "updated-test.txt",
                Description = "updated file",
                Scope = "private",
                IfMatch = "0"
            };
            var file = await assetClient.UpdateFileAsync(request);

            return StatusCode(200, file);
        }

        #endregion

        #region Time Series

        [HttpGet("get-timeseries")]
        public async Task<ActionResult<IEnumerable<TestTimeSeriesData>>> GetTimeSeries()
        {
            var iotClient = _sdk.GetIotTimeSeriesClient();
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

        [HttpGet("put-timeseries-multiple")]
        public async Task<ActionResult> PutTimeSeriesMultiple()
        {
            var iotClient = _sdk.GetIotTimeSeriesClient();
            DateTime nowUtc = DateTime.Now.ToUniversalTime();

            List<TestTimeSeriesData> timeSeriesData = new()
            {
                new TestTimeSeriesData(nowUtc, 0.5, 0.7, 0.3),
                new TestTimeSeriesData(nowUtc.AddMinutes(1), 0.8, 1.2, 0.7),
                new TestTimeSeriesData(nowUtc.AddMinutes(2), 1.6, 0.2, 0.5)
            };
            //timeSeriesData.Add(new { _time = DateTime.Now, x = 0.5, y = 0.7, z = 0.3 });
            //timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(1), x = 0.8, y = 1.2, z = 0.7 });
            //timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(2), x = 1.6, y = 0.2, z = 0.5 });

            List<TimeSeriesSet> timeSeriesObjects = new()
            {
                new TimeSeriesSet()
                {
                    EntityId = "ec206f76b04a49a4938c1573b35b6688",
                    PropertySetName = "acceleration",
                    Data = timeSeriesData
                }
            };

            PutTimeSeriesMultipleRequest request = new()
            {
                TimeSeries = timeSeriesObjects
            };
            await iotClient.PutTimeSeriesMultipleAsync(request);

            return StatusCode(201);
        }

        [HttpGet("put-timeseries")]
        public async Task<ActionResult> PutTimeSeries()
        {
            var iotClient = _sdk.GetIotTimeSeriesClient();
            DateTime nowUtc = DateTime.Now.ToUniversalTime();

            List<TestTimeSeriesData> timeSeriesData = new()
            {
                new TestTimeSeriesData(nowUtc, 0.5, 0.7, 0.3),
                new TestTimeSeriesData(nowUtc.AddMinutes(1), 0.8, 1.2, 0.7),
                new TestTimeSeriesData(nowUtc.AddMinutes(2), 1.6, 0.2, 0.5)
            };
            //timeSeriesData.Add(new { _time = DateTime.Now, x = 0.5, y = 0.7, z = 0.3 });
            //timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(1), x = 0.8, y = 1.2, z = 0.7 });
            //timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(2), x = 1.6, y = 0.2, z = 0.5 });

            PutTimeSeriesRequest request = new()
            {
                Data = timeSeriesData,
                EntityId = "ec206f76b04a49a4938c1573b35b6688",
                PropertySetName = "acceleration"
            };
            await iotClient.PutTimeSeriesAsync(request);

            return StatusCode(201);
        }

        [HttpGet("delete-timeseries")]
        public async Task<ActionResult> DeleteTimeSeries()
        {
            var iotClient = _sdk.GetIotTimeSeriesClient();
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
            var iotAggregClient = _sdk.GetIotTsAggregateClient();
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
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new ListAspectTypesRequest()
            {
                Size = 100
            };
            List<AspectType> aspectTypes = (await assetClient.ListAspectTypesAsync(request)).Data.ToList();

            return StatusCode(200, aspectTypes);
        }

        [HttpGet("get-aspect-types")]
        public async Task<ActionResult<AspectType>> GetAspectType()
        {
            var assetClient = _sdk.GetAssetManagementClient();
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
            var assetClient = _sdk.GetAssetManagementClient();
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
            var assetClient = _sdk.GetAssetManagementClient();

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
                AspectType = newAspectType
            };
            var aspectType = await assetClient.PutAspectTypeAsync(request);

            return StatusCode(200, aspectType);
        }

        [HttpGet("update-aspect-types")]
        public async Task<ActionResult<AspectType>> UpdateAspectType()
        {
            var assetClient = _sdk.GetAssetManagementClient();

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
                AspectType = updatedAspectType
            };
            var aspectType = await assetClient.PutAspectTypeAsync(request);

            return StatusCode(200, aspectType);
        }

        [HttpGet("patch-aspect-types")]
        public async Task<ActionResult<AspectType>> PatchAspectType()
        {
            var assetClient = _sdk.GetAssetManagementClient();

            var updatedAspectType = new AspectTypeUpdate()
            {
                Description = "Patched test"
            };

            var request = new PatchAspectTypeRequest()
            {
                Id = "iiotdgli.My_new_aspect_type",
                IfMatch = "1",
                AspectType = updatedAspectType
            };
            var aspectType = await assetClient.PatchAspectTypeAsync(request);

            return StatusCode(200, aspectType);
        }

        #endregion

        #region Asset Type

        [HttpGet("list-asset-types")]
        public async Task<ActionResult<IEnumerable<AssetType>>> ListAssetTypes()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new ListAssetTypesRequest()
            {
                Size = 100
            };
            List<AssetType> aspectTypes = (await assetClient.ListAssetTypesAsync(request)).Data.ToList();

            return StatusCode(200, aspectTypes);
        }

        [HttpGet("get-asset-type")]
        public async Task<ActionResult<AssetType>> GetAssetType()
        {
            var assetClient = _sdk.GetAssetManagementClient();
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
            var assetClient = _sdk.GetAssetManagementClient();

            var aspects = new List<AspectPut>
            {
                new AspectPut() { Name = "acceleration", AspectTypeId = "iiotdgli.acceleration" }
            };

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
                AssetType = newAssetType
            };
            var assetType = await assetClient.PutAssetTypeAsync(request);

            return StatusCode(200, assetType);
        }

        [HttpGet("update-asset-type")]
        public async Task<ActionResult<AssetType>> UpdateAssetType()
        {
            var assetClient = _sdk.GetAssetManagementClient();

            var aspects = new List<AspectPut>
            {
                new AspectPut() { Name = "acceleration", AspectTypeId = "iiotdgli.acceleration" }
            };

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
                AssetType = newAssetType
            };
            var assetType = await assetClient.PutAssetTypeAsync(request);

            return StatusCode(200, assetType);
        }

        [HttpGet("patch-asset-type")]
        public async Task<ActionResult<AssetType>> PatchAssetType()
        {
            var assetClient = _sdk.GetAssetManagementClient();

            var updatedAssetType = new AssetTypeUpdate()
            {
                Description = "Patched asset type"
            };

            var request = new PatchAssetTypeRequest()
            {
                Id = "iiotdgli.My_new_asset_type",
                IfMatch = "1",
                AssetType = updatedAssetType
            };
            var assetType = await assetClient.PatchAssetTypeAsync(request);

            return StatusCode(200, assetType);
        }

        [HttpGet("delete-asset-type")]
        public async Task<ActionResult<AssetType>> DeleteAssetType()
        {
            var assetClient = _sdk.GetAssetManagementClient();

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
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new AddAssetTypeFileAssignmentRequest()
            {
                Id = "iiotdgli.My_new_asset_type",
                Key = "testFile",
                FileId = "fe81d2c22a9448eea41d0f460e5a5731",
                IfMatch = "0"
            };

            AssetType assetType = await assetClient.AddAssetTypeFileAssignmentAsync(request);
            return StatusCode(200, assetType);
        }

        [HttpGet("delete-asset-type-file-assignment")]
        public async Task<ActionResult<IEnumerable<AssetType>>> DeleteAssetTypeFileAssignment()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new DeleteAssetTypeFileAssignmentRequest()
            {
                Id = "iiotdgli.My_new_asset_type",
                Key = "testFile",
                IfMatch = "1"
            };

            AssetType assetType = await assetClient.DeleteAssetTypeFileAssignmentAsync(request);
            return StatusCode(200, assetType);
        }

        [HttpGet("patch-asset-type-variables")]
        public async Task<ActionResult> PatchAssetTypeVariables()
        {
            var assetClient = _sdk.GetAssetManagementClient();

            var variableMap = new VariableMap()
            {
                Var3 = new VariableUpdate()
                {
                    Name = "var5"
                },
                Var4 = new VariableUpdate()
                {
                    Name = "var6"
                },
            };

            var request = new PatchAssetTypeVariablesRequest()
            {
                Id = "prsdevex.Dotnet_sdk",
                IfMatch = "1",
                VariableMap = variableMap
            };
            await assetClient.PatchAssetTypeVariablesAsync(request);

            return StatusCode(204);
        }

        #endregion

        #region Asset model lock

        [HttpGet("get-lock-state")]
        public async Task<ActionResult<LockStateWithJobs>> GetModelLock()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            LockStateWithJobs lockState = await assetClient.GetLockStateAsync();

            return StatusCode(200, lockState);
        }

        [HttpGet("put-lock-state")]
        public async Task<ActionResult<LockState>> PutModelLock()
        {
            var assetClient = _sdk.GetAssetManagementClient();
            var request = new PutLockStateRequest() { Enabled = false };
            LockState lockState = await assetClient.PutLockStateAsync(request);

            return StatusCode(200, lockState);
        }

        #endregion

        #region Event

        [HttpGet("add-event")]
        public async Task<ActionResult<MyEvent>> AddEvent()
        {
            var eventClient = _sdk.GetEventManagementClient();
            var request = new AddEventRequest()
            {
                Event = new MyEventAddUpdate()
                {
                    EntityId = "da4aabbd3f2f488da7ef75fa506a8eaa",
                    Timestamp = DateTime.Now,
                    Description = "Error happened in the test",
                    Severity = 5
                }
            };
            return StatusCode(200, await eventClient.AddEventAsync<MyEvent>(request));
        }

        [HttpGet("list-events")]
        public async Task<ActionResult<IEnumerable<MyEvent>>> ListEvents()
        {
            var eventClient = _sdk.GetEventManagementClient();
            var request = new ListEventsRequest();
            return StatusCode(200, await eventClient.ListEventsAsync<Event>(request));
        }

        [HttpGet("get-event")]
        public async Task<ActionResult<MyEvent>> GetEvent()
        {
            var eventClient = _sdk.GetEventManagementClient();
            var request = new GetEventRequest()
            {
                EventId = "3a799ea2-289d-4421-8120-9ec4ac23cf54"
            };
            return StatusCode(200, await eventClient.GetEventAsync<MyEvent>(request));
        }

        [HttpGet("update-event")]
        public async Task<ActionResult<MyEvent>> UpdateEvent()
        {
            var eventClient = _sdk.GetEventManagementClient();
            var request = new UpdateEventRequest()
            {
                EventId = "3a799ea2-289d-4421-8120-9ec4ac23cf54",
                IfMatch = "1",
                Event = new MyEventAddUpdate()
                {
                    Severity = 3,
                    CorrelationId = "61349fc6e3c5d0f8a12069480abd4438",
                    EntityId = "da4aabbd3f2f488da7ef75fa506a8eaa",
                    Timestamp = DateTime.Parse("2021-09-05T10:45:25.527299Z").ToUniversalTime()
                }
            };
            return StatusCode(200, await eventClient.UpdateEventAsync<MyEvent>(request));
        }

        #endregion

        #region Event type

        [HttpGet("list-event-types")]
        public async Task<ActionResult<ResourceList<EventType>>> ListEventTypes()
        {
            var eventClient = _sdk.GetEventManagementClient();
            var request = new ListEventTypesRequest();
            return StatusCode(200, await eventClient.ListEventTypesAsync(request));
        }

        [HttpGet("add-event-type")]
        public async Task<ActionResult<EventType>> AddEventType()
        {
            var eventClient = _sdk.GetEventManagementClient();
            var request = new AddEventTypeRequest()
            {
                EventType = new EventTypeAdd()
                {
                    Name = "DotnetSdkEventType",
                    Fields = new List<FieldAdd>
                    {
                        new FieldAdd { Name = "Foo", Type = "STRING"}
                    }
                }
            };
            return StatusCode(200, await eventClient.AddEventTypeAsync(request));
        }

        [HttpGet("get-event-type")]
        public async Task<ActionResult<EventType>> GetEventType()
        {
            var eventClient = _sdk.GetEventManagementClient();
            var request = new GetEventTypeRequest()
            {
                EventTypeId = "43eba523-b5aa-4eea-998b-8bc311caf0d3"
            };
            return StatusCode(200, await eventClient.GetEventTypeAsync(request));
        }

        [HttpGet("update-event-type")]
        public async Task<ActionResult<EventType>> UpdateEventType()
        {
            var eventClient = _sdk.GetEventManagementClient();
            var request = new UpdateEventTypeRequest()
            {
                EventTypeId = "43eba523-b5aa-4eea-998b-8bc311caf0d3",
                IfMatch = "0",
                EventTypePatch = new EventTypePatch
                {
                    Op = "replace",
                    Path = "/scope",
                    Value = "GLOBAL"
                }
            };
            return StatusCode(200, await eventClient.UpdateEventTypeAsync(request));
        }

        [HttpGet("delete-event-type")]
        public async Task<ActionResult<EventType>> DeleteEventType()
        {
            var eventClient = _sdk.GetEventManagementClient();
            var request = new DeleteEventTypeRequest()
            {
                EventTypeId = "43eba523-b5aa-4eea-998b-8bc311caf0d3",
                IfMatch = "1"
            };
            await eventClient.DeleteEventTypeAsync(request);
            return StatusCode(204);
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

    public class VariableMap
    {
        [JsonProperty("var4")]
        public VariableUpdate Var3 { get; set; }

        [JsonProperty("var3")]
        public VariableUpdate Var4 { get; set; }
    }

    public class MyEventAddUpdate : EventAddUpdate
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
