![MindSphere image](/other/mdsp.png)
#  MindSphere SDK for .NET

Open-source .NET SDK for [MindSphere](https://siemens.mindsphere.io/) APIs mainly to support backend development in [ASP.NET Core](https://github.com/dotnet/aspnetcore).

❗ **Project is still a work in progress.** ❗

This project was started on my own initiative. I am still a student and I am trying my best to develop useful solution for the absence of .NET SDK. Of course, any help is more than welcome.

My goal is to develop SDK similar to [MindSphere SDK for Node.js](https://developer.mindsphere.io/resources/mindsphere-sdk-node/index.html).

## What is ready?

Following features are implemented for now (in some cases few more changes are needed).

**Authentication**
- using application credentials

**Asset managemet**
- list all assets
- read an asset
- create an asset
- update an asset
- patch an asset
- delete an asset
- move an asset
- read the root asset of the user
- save asset file assignment
- delete asset file assignment
- list all aspect types
- read an aspect type
- delete an aspect type
- put an aspect type
- patch and aspect type

**IOT time series**
- read time series data
- write time series data
- delete time series data

**IOT time series aggregates**
- read aggregate time series data

**Others**
- usage with ASP.<i></i>NET Core
- load application credentials from a file

## Examples

Examples of the current state of the project. Some of them might change in the future.


### Usage with ASP.<i></i>NET Core

Register service in the *Startup.cs* with credentials settings.

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

Specify dependency in e.g. controller. 

```csharp
private IMindSphereSdkService _mindSphereSdkService;

public MainController(IMindSphereSdkService mindSphereSdkService)
{
    _mindSphereSdkService = mindSphereSdkService;
}
```

### Listing assets

```csharp
var assetClient = _mindSphereSdkService.GetAssetManagementClient();
var request = new ListAssetsRequest() 
{
    Size = 5
};
List<Asset> assets = (await assetClient.ListAssetsAsync(request)).ToList();
```

After calling the *ListAssetsAsync* method enumeration of following objects is returned.

```csharp
public class Asset
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

### Getting time series data

To get time series data it is necessary to have corresponding class prepared. It is possible to use Newtonsoft *JsonProperty* atributes. Or just to name your properties in the corresponding way so they could be deserialized. 

```csharp
public class TestTimeSeriesData {
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
List<TestTimeSeriesData> timeSeries = (await iotClient.GetTimeSeriesAsync<TestTimeSeriesData>(request)).ToList();
```

### Putting new time series data

To put new time series data into the MindSphere you can use predefined class or anonymous type.

If you use your own class you need to name the properties in the corresponding way or to add Newtonsoft *JsonProperty* atributes. Otherwise the deserialization would fail.

```csharp
var iotClient = _mindSphereSdkService.GetIotTimeSeriesClient();

// with anonymous type
List<object> timeSeriesData = new List<object>();
timeSeriesData.Add(new { _time = DateTime.Now, x = 0.5, y = 0.7, z = 0.3 });
timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(1), x = 0.8, y = 1.2, z = 0.7 });

// with class
List<TestTimeSeriesData> timeSeriesData = new List<TestTimeSeriesData>();
timeSeriesData.Add(new TestTimeSeriesData(nowUtc, 0.5, 0.7, 0.3));
timeSeriesData.Add(new TestTimeSeriesData(nowUtc.AddMinutes(1), 0.8, 1.2, 0.7));
timeSeriesData.Add(new TestTimeSeriesData(nowUtc.AddMinutes(2), 1.6, 0.2, 0.5));

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

### Getting time series aggregates

*GetAggregateTimeSeriesAsync* is also generic method. It is necessary to set the generic type to class derived from *AggregateSet*. Specify expected MindSphere variables using properties of type *AggregateVariable* with corresponding names (or JsonProperty).

```csharp
public class TestAggregateTsData : AggregateSet
{
    [JsonProperty("x")]
    public AggregateVariable X { get; set; }

    [JsonProperty("y")]
    public AggregateVariable Y { get; set; }

    [JsonProperty("z")]
    public AggregateVariable Z { get; set; }
}
```

```csharp
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
```