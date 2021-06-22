![MindSphere image](/other/mdsp.png)
#  MindSphere SDK for .NET

Open-source .NET SDK for [MindSphere](https://siemens.mindsphere.io/) APIs mainly to support backend development in [ASP.NET Core](https://github.com/dotnet/aspnetcore).


*This project was started on my own initiative. I am still a student and I am trying my best to develop useful solution for the absence of .NET SDK. Of course, any help is more than welcome. My goal is to develop SDK similar to [MindSphere SDK for Node.js](https://developer.mindsphere.io/resources/mindsphere-sdk-node/index.html).*

## Installation

The SDK is hosted as a package on the [nuget.org](https://www.nuget.org/).

[**Core project**](https://www.nuget.org/packages/MindSphereSdk.Core/)

```
dotnet add package MindSphereSdk.Core --version 1.0.0
```

[**Extension project**](https://www.nuget.org/packages/MindSphereSdk.AspNetCore/)

```
dotnet add package MindSphereSdk.AspNetCore --version 1.0.0
```

## Examples

Provided code examples will guide you through this SDK.

- [Application credentials](#Application-credentials)
- [Create a client](#Create-a-client)
- [ServiceCollection usage](#ServiceCollection-usage)
- [Listing assets](#Listing-assets)
- [Download file](#Download-file)
- [Upload file](#Upload-file)
- [Getting time series data](#Getting-time-series-data)
- [Putting new time series data](#Putting-new-time-series-data)
- [Getting time series aggregates](#Getting-time-series-aggregates)

### Application credentials

```csharp
AppCredentials credentials = new AppCredentials(
    "<client-id>",
    "<client-secret>",
    "<app-name>",
    "<app-version>",
    "<host-tenant>",
    "<user-tenant>"
);
```

It is also possible to import application credentials directly from a JSON file.

```csharp
AppCredentials appCredentials = AppCredentials.FromJsonFile("<file-path>");
```

The JSON file has to fit to the given structure.

```json
{
    "keyStoreClientId": "<client-id>",
    "keyStoreClientSecret": "<client-secret>",
    "appName": "<app-name>",
    "appVersion": "<app-version>",
    "hostTenant": "<host-tenant>",
    "userTenant": "<user-tenant>"
}
```
 
### Create a client

The client constructor must be provided with HttpClient. 

```csharp
HttpClient httpClient = new HttpClient();
var client = new AssetManagementClient(appCredentials, httpClient);
```

When you create multiple MindSphere clients you should reuse your HttpClient. [Here are more information regarding this matter.](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0#remarks)

### ServiceCollection usage

If you have installed ASP.NET Core extension, then you can simply register MindSphere service in the service container. However it is not necessary to use this feature.

This service needs to have HttpClient service in the container as well.

```csharp
// Startup.cs file
services.AddHttpClient();
services.AddMindSphereSdkService(options =>
{
    options.Credentials = new AppCredentials(...);
});
```

After that you can use this service in e.g. controller. 

```csharp
private AssetManagementClient _client;

public MainController(IMindSphereSdkService mindSphereSdkService)
{
    _client = mindSphereSdkService.GetAssetManagementClient();
}
```

### Listing assets

```csharp
var client = new AssetManagementClient(appCredentials, httpClient);

var request = new ListAssetsRequest() 
{
    Size = 5
};
List<Asset> assets = (await client.ListAssetsAsync(request)).ToList();
```

### Download file

```csharp
var client = new AssetManagementClient(appCredentials, httpClient);

var request = new DownloadFileRequest()
{
    Id = "<file-id>"
};
string fileContent = await client.DownloadFileAsync(request);
```

### Upload file

```csharp
var client = new AssetManagementClient(appCredentials, httpClient);
var fs = new FileStream("<file-path>", FileMode.Open);

var request = new UploadFileRequest()
{
    File = fs,
    Name = "<file-name>"
};
var file = await assetClient.UploadFileAsync(request);
```

### Getting time series data

To get time series data it is necessary to have corresponding class prepared. It is possible to use Newtonsoft *JsonProperty* atributes. Or just to name your properties in the corresponding way so they could be deserialized. 

```csharp
public class TimeSeriesData 
{
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
var client = new IotTimeSeriesClient(credentials, httpClient);
var request = new GetTimeSeriesRequest()
{
    EntityId = "<entity-id>",
    PropertySetName = "<aspect>",
    From = DateTime.Now.AddDays(-1),
    To = DateTime.Now,
    Limit = 2
};
var timeSeries = (await client.GetTimeSeriesAsync<TimeSeriesData>(request)).ToList();
```

### Putting new time series data

To put new time series data into the MindSphere you can use predefined class or anonymous type.

If you use your own class you need to name the properties in the corresponding way or to add Newtonsoft *JsonProperty* atributes. Otherwise the deserialization would fail.

```csharp
var client = new IotTimeSeriesClient(credentials, httpClient);

// with anonymous type
List<object> timeSeriesData = new List<object>();
timeSeriesData.Add(new { _time = DateTime.Now, x = 0.5, y = 0.7, z = 0.3 });
timeSeriesData.Add(new { _time = DateTime.Now.AddMinutes(1), x = 0.8, y = 1.2, z = 0.7 });

// with class
List<TimeSeriesData> timeSeriesData = new List<TimeSeriesData>();
timeSeriesData.Add(new TimeSeriesData(nowUtc, 0.5, 0.7, 0.3));
timeSeriesData.Add(new TimeSeriesData(nowUtc.AddMinutes(1), 0.8, 1.2, 0.7));
timeSeriesData.Add(new TimeSeriesData(nowUtc.AddMinutes(2), 1.6, 0.2, 0.5));

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
await client.PutTimeSeriesAsync(request);
```

### Getting time series aggregates

*GetAggregateTimeSeriesAsync* is also generic method. It is necessary to set the generic type to class derived from *AggregateSet*. Specify expected MindSphere variables using properties of type *AggregateVariable* with corresponding names (or JsonProperty).

```csharp
public class AggregateTsData : AggregateSet
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
var client = new IotTsAggregatesClient(credentials, httpClient);

var request = new GetAggregateTimeSeriesRequest()
{
    AssetId = "<entity-id>",
    AspectName = "<aspect>",
    From = new DateTime(2021, 4, 25, 0, 0, 0),
    To = new DateTime(2021, 4, 26, 0, 0, 0),
    IntervalUnit = "minute",
    IntervalValue = 2
};

var tsAggregate = await client.GetAggregateTimeSeriesAsync<AggregateTsData>(request);
```
