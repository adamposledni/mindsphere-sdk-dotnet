![MindSphere image](/other/mdsp.png)
#  MindSphere SDK for .NET

Open-source .NET SDK for [MindSphere](https://siemens.mindsphere.io/) APIs mainly to support backend development in [ASP.NET Core](https://github.com/dotnet/aspnetcore).


*This project was started on my own initiative. I am still a student and I am trying my best to develop useful solution for the absence of .NET SDK. Of course, any help is more than welcome. My goal is to develop SDK similar to [MindSphere SDK for Node.js](https://developer.mindsphere.io/resources/mindsphere-sdk-node/index.html).*

---

- [Installation](#Installation)
- [Getting started](#Getting-started)
- [Credentials](#Credentials)
    - [Application credentials](#Application-credentials)
    - [User credentials](#User-credentials)
- [Client configuration](#Client-configuration)
- [Clients](#Clients)
    - [Listing assets](#Listing-assets)
    - [Download file](#Download-file)
    - [Upload file](#Upload-file)
    - [Getting time series data](#Getting-time-series-data)
    - [Putting new time series data](#Putting-new-time-series-data)
    - [Getting time series aggregates](#Getting-time-series-aggregates)

---
## Installation

The SDK is hosted as a package on the [nuget.org](https://www.nuget.org/packages/MindSphereSdk.Core/). It is possible to use following command.

```
dotnet add package MindSphereSdk.Core --version 1.0.1
```

An alternative is to install MindSphereSdk.Core via NuGet Package Manager.

## Getting started

To start using MindSphere APIs via this SDK you need to create new instance of the client class. There are multiple clients available (e.g. *IotTimeSeriesClient*). Each client must be initialized with credentials, configuration and *HttpClient* instance.

```csharp
// prerequisites
var credentials = new AppCredentials(
    "<client-id>",
    "<client-secret>",
    "<app-name>",
    "<app-version>",
    "<host-tenant>",
    "<user-tenant>"
);
var config = new ClientConfiguration();
var httpClient = new HttpClient();
var request = new ListAssetsRequest();
// initialize client
var sdkClient = new AssetManagementClient(credentials, config, httpClient);
// make request
var assets = await client.ListAssetsAsync(request);

```

## Credentials

There are two possible ways how to provide credentials to the SDK client. Application and user credentials.

### Application credentials

Application credentials are issued in the Developer Cockpit. [Find out more.](https://documentation.mindsphere.io/resources/html/developer-cockpit/en-US/124342231819.html)

*AppCredentials* object can be created using following code.

```csharp
var credentials = new AppCredentials(
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
var credentials = AppCredentials.FromJsonFile("<file-path>");
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

### User credentials

*UserCredentials* constructor must be provided with access token, that can be exracted from the request headers. [Find out more.](https://developer.mindsphere.io/concepts/concept-authentication.html#calling-apis-from-backend)

```csharp
var credentials = new UserCredentials("<access-token>");
```
 
## Client configuration

Additional options are passed to SDK client via *ClientConfiguration*.

```csharp
var config = new ClientConfiguration("<region>", "<domain>");
```

With parameterless constructor the default values are as follows.

| Property | Value |
| --- | --- |
| Region | eu1 |
| Domain | mindsphere.io |

```csharp
var config = new ClientConfiguration();
```

## Clients

Every client constructor must be provided with *HttpClient* instance. 

❗ When you create multiple MindSphere clients you should reuse your *HttpClient*. [Find out more.](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0#remarks)

```csharp
var httpClient = new HttpClient();
var credentials = new UserCredentials("<access-token>");
var config = new ClienConfiguration();
var client = new AssetManagementClient(credentials, config, httpClient);
```

<!-- TODO: overview of the clients -->
<!-- TODO: restructure method docs -->

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

*GetAggregateTimeSeriesAsync* is generic method. It is necessary to set the generic type to a class derived from *AggregateSet*. Specify expected MindSphere variables using properties of type *AggregateVariable* with corresponding names (or *JsonProperty*).

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
