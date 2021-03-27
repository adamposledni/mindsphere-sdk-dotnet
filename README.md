![MindSphere image](/docs/mdsp.png)
#  MindSphere SDK for .NET

Open-source .NET SDK for [MindSphere](https://siemens.mindsphere.io/) APIs mainly to support backend development in [ASP.NET Core](https://github.com/dotnet/aspnetcore).

❗ **Project is still a work in progress. Following features are done or being developed** ❗
- authentication using AppCredentials
- listing assets
- adding new assets
- getting time series data
- putting new time series data
- extension method for ServiceCollection to register SDK as service

This project was started on my own initiative. I am still a student and I am trying my best to develop useful solution for the absence of .NET SDK. Of course, any help is more than welcome.

My goal is the develop SDK that provides at least same features as MindSphere SDK for [Node.js](https://developer.mindsphere.io/resources/mindsphere-sdk-node/index.html).

---
## Examples

Examples of the current state of the project. Some of them might change in the future.

### 1) Usage with ASP.<i></i></i>NET Core

Register service in the *Startup.cs* with credentials settings

```csharp
services.AddHttpClient(); // SDK needs HttpClient in the DI container
services.AddMindSphereSdkService(options =>
{
    options.Credentials = new AppCredentials(
        "<client-id>",
        "<client-secret>",
        "<app-name>",
        "<app-version",
        "<host-tenant>",
        "<user-tenant>"
    );
});
```

Specify dependency in e.g. controller

```csharp
private IMindSphereSdkService _mindSphereSdkService;

public MainController(IMindSphereSdkService mindSphereSdkService)
{
    _mindSphereSdkService = mindSphereSdkService;
}
```

Then you can use the SDK (see below)

### 2) Listing assets

First, the asset management client instance is required. Then you can specify the request object and call corresponding method.

```csharp
var assetClient = _mindSphereSdkService.GetAssetManagementClient();
var request = new ListAssetsRequest() 
{
    Size = 5
};
List<AssetResource> assets = (await assetClient.ListAssetsAsync(request)).ToList();
```

After calling the *ListAssetsAsync* method enumeration of following objects is returned.

```csharp
public class AssetResource
{
    public string Name { get; set; }
    public string ExternalId { get; set; }
    public string Description { get; set; }
    public Location Location { get; set; }
    public IEnumerable<Variable> Variables { get; set; }
    public IEnumerable<Aspect> Aspects { get; set; }
    // and more ...
}
```

### 3) Adding assets

```csharp
var assetClient = _mindSphereSdkService.GetAssetManagementClient();
var request = new AddAssetRequest()
{
    Body = new AssetResource()
    {
        Name = "NewAssetName",
        TypeId = "AssetType",
        ParentId = "ParentId",
    }
};
AssetResource createdAsset = await assetClient.AddAssetsAsync(request);
```

### 4) Getting time series data

To get time series data it is necessary to have corresponding type (class) prepared. It is possible to use Newtonsoft *JsonProperty* atributes. Or just to name your properties in the corresponding way so they could be deserialized. 

```csharp
public class AccelerationData {
        [JsonProperty("_time")]
        public DateTime Time { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }
}
```

After that you can pass it to generic method *GetTimeSeriesAsync*.

```csharp
var iotClient = _mindSphereSdkService.GetIotTimeSeriesClient();
var request = new GetTimeSeriesRequest()
{
    EntityId = "EntityId",
    PropertySetName = "Aspect",
    From = DateTime.Now.AddDays(-1),
    To = DateTime.Now,
    Limit = 2
};
List<AccelerationData> timeSeries = (await iotClient.GetTimeSeriesAsync<AccelerationData>(request)).ToList();
```


### 5) Putting new time series data

To put new time series data into the MindSphere you can use predefined datatype (class) or anonymous type.

If you use your own class you need to name the properties in the corresponding way or to add Newtonsoft *JsonProperty* atributes.

```csharp
var iotClient = _mindSphereSdkService.GetIotTimeSeriesClient();

List<object> timeSeriesData = new List<object>();
timeSeriesData.Add(new { _time = DateTime.Now, x = 0.5, y = 0.7, z = 0.3 });
timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(1), x = 0.8, y = 1.2, z = 0.7 });

List<TimeSeriesObject> timeSeriesObjects = new List<TimeSeriesObject>();
timeSeriesObjects.Add(new TimeSeriesObject()
{
    EntityId = "EntityId",
    PropertySetName = "AspectName",
    Data = timeSeriesData
});

PutTimeSeriesRequest request = new PutTimeSeriesRequest()
{
    TimeSeries = timeSeriesObjects
};
await iotClient.PutTimeSeriesAsync(request);
```