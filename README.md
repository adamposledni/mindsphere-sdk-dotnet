![MindSphere image](/other/mdsp.png)
#  MindSphere SDK for .NET

Open-source .NET SDK for [MindSphere](https://siemens.mindsphere.io/) APIs mainly to support backend development in [ASP.NET Core](https://github.com/dotnet/aspnetcore). 


*This project was started on my own initiative. I am still a student and I am trying my best to develop useful solution for the absence of .NET SDK. Of course, any help is more than welcome. My goal is to develop SDK similar to [MindSphere SDK for Node.js](https://developer.mindsphere.io/resources/mindsphere-sdk-node/index.html).*

📌 What am I working on?  - [Tasks](https://github.com/hroudaadam/mindsphere-sdk-dotnet/projects/1)

---

- [Installation](#Installation)
- [Getting started](#Getting-started)
- [Credentials](#Credentials)
    - [Application credentials](#Application-credentials)
    - [User credentials](#User-credentials)
- [Client configuration](#Client-configuration)
- [Asset management client](#Asset-management-client)
    - [List assets](#List-assets)
    - [Download file](#Download-file)
    - [Upload file](#Upload-file)
- [IoT time series client](#IoT-time-series-client)
    - [Get time series data](#Get-time-series-data)
    - [Put new time series data](#Put-new-time-series-data)
- [IoT time series aggregates client](#IoT-time-series-aggregates-client)
    - [Get time series aggregates](#Get-time-series-aggregates)

---
## Installation

The SDK is hosted as a package on the [nuget.org](https://www.nuget.org/packages/MindSphereSdk.Core/). It is possible to use following command.

```
dotnet add package MindSphereSdk.Core --version 1.1
```

An alternative is to install MindSphereSdk.Core via NuGet Package Manager.

## Getting started

To start using MindSphere APIs via this SDK you need to get instance of the client class. There are multiple clients available (e.g. *IotTimeSeriesClient*). Those clients are provided by *MindSphereApiSdk* class. That must be initialized with credentials and configuration.

```csharp
// prerequisites
var creds = new AppCredentials(
    "<client-id>",
    "<client-secret>",
    "<app-name>",
    "<app-version>",
    "<host-tenant>",
    "<user-tenant>"
);
var config = new ClientConfiguration();
var sdk = new MindSphereApiSdk(creds, config);
// get client
var assetClient = sdk.GetAssetManagementClient();
// make request
var request = new ListAssetsRequest();
var assets = await assetClient.ListAssetsAsync(request);

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

*UserCredentials* constructor must be provided with access token, that can be extracted from the request headers. [Find out more.](https://developer.mindsphere.io/concepts/concept-authentication.html#calling-apis-from-backend)

```csharp
var creds = new UserCredentials("<access-token>");
var sdk = new MindSphereApiSdk(creds, config);
```
 
## Client configuration

Additional options are passed to SDK client via *ClientConfiguration* that has following default values.

| Property | Default value |
| --- | --- |
| Region | eu1 |
| Domain | mindsphere.io |
| Timeout | 100s |
| Proxy | *empty* |

```csharp
var config = new ClientConfiguration();
```

## Asset management client
Client for configuring, reading and managing assets, asset types and aspect types.

**Aspect type**
| Method | Endpoint          | SDK                   |
|--------|-------------------|-----------------------|
| GET    | /aspecttypes      | ListAspectTypesAsync  |
| PUT    | /aspecttypes/{id} | PutAspectTypeAsync    |
| PATCH  | /aspecttypes/{id} | PatchAspectTypeAsync  |
| GET    | /aspecttypes/{id} | GetAspectTypeAsync    |
| DELETE | /aspecttypes/{id} | DeleteAspectTypeAsync |

**Asset type**
| Method | Endpoint                               | SDK                                |
|--------|----------------------------------------|------------------------------------|
| GET    | /assettypes                            | ListAssetTypesAsync                |
| PUT    | /assettypes/{id}                       | PutAssetTypeAsync                  |
| PATCH  | /assettypes/{id}                       | PatchAssetTypeAsync                |
| GET    | /assettypes/{id}                       | GetAssetTypeAsync                  |
| DELETE | /assettypes/{id}                       | DeleteAssetTypeAsync               |
| PUT    | /assettypes/{id}/fileAssignments/{key} | AddAssetTypeFileAssignmentAsync    |
| DELETE | /assettypes/{id}/fileAssignments/{key} | DeleteAssetTypeFileAssignmentAsync |
| PATCH  | /assettypes/{id}/variables             | *not implemented*                  |

**Asset**
| Method | Endpoint                           | SDK                            |
|--------|------------------------------------|--------------------------------|
| GET    | /assets                            | ListAssetsAsync                |
| POST   | /assets                            | AddAssetAsync                  |
| GET    | /assets/{id}                       | GetAssetAsync                  |
| PUT    | /assets/{id}                       | PutAssetAsync                  |
| PATCH  | /assets/{id}                       | PatchAssetAsync                |
| DELETE | /assets/{id}                       | DeleteAssetAsync               |
| POST   | /assets/{id}/move                  | MoveAssetAsync                 |
| PUT    | /assets/{id}/fileAssignments/{key} | SaveAssetFileAssignmentAsync   |
| DELETE | /assets/{id}/fileAssignments/{key} | DeleteAssetFileAssignmentAsync |
| GET    | /assets/root                       | GetRootAssetAsync              |

**Structure**
| Method | Endpoint               | SDK                     |
|--------|------------------------|-------------------------|
| GET    | /assets/{id}/variables | ListAssetVariablesAsync |
| GET    | /assets/{id}/aspects   | ListAssetAspectsAsync   |

**Location**
| Method | Endpoint              | SDK                      |
|--------|-----------------------|--------------------------|
| PUT    | /assets/{id}/location | PutAssetLocationAsync    |
| DELETE | /assets/{id}/location | DeleteAssetLocationAsync |

**File**
| Method | Endpoint             | SDK               |
|--------|----------------------|-------------------|
| POST   | /files               | UploadFileAsync   |
| GET    | /files               | ListFilesAsync    |
| GET    | /files/{fileId}/file | DownloadFileAsync |
| GET    | /files/{fileId}      | GetFileAsync      |
| PUT    | /files/{fileId}      | UpdateFileAsync   |
| DELETE | /files/{fileId}      | DeleteFileAsync   |

**Asset model lock**
| Method | Endpoint    | SDK               |
|--------|-------------|-------------------|
| GET    | /model/lock | *not implemented* |
| PUT    | /model/lock | *not implemented* |

### List assets

```csharp
var assetClient = _sdk.GetAssetManagementClient();
var request = new ListAssetsRequest()
{
    Size = 5,
    Page = 2
};
var result = await assetClient.ListAssetsAsync(request);
```

### Download file

```csharp
var assetClient = _sdk.GetAssetManagementClient();
var request = new DownloadFileRequest()
{
    Id = "<file-id>"
};
string fileContent = await assetClient.DownloadFileAsync(request);
```

### Upload file

```csharp
var assetClient = _sdk.GetAssetManagementClient();
var fs = new FileStream("test.txt", FileMode.Open);

var request = new UploadFileRequest()
{
    File = fs,
    Name = "test.txt"
};
var file = await assetClient.UploadFileAsync(request);
```

## IoT time series client
Client for creating, reading, updating, and deleting time series data.

| Method | Endpoint                                 | SDK                        |
|--------|------------------------------------------|----------------------------|
| PUT    | /timeseries                              | PutTimeSeriesMultipleAsync |
| GET    | /timeseries/{entityId}/{propertySetName} | GetTimeSeriesAsync         |
| PUT    | /timeseries/{entityId}/{propertySetName} | PutTimeSeriesAsync         |
| DELETE | /timeseries/{entityId}/{propertySetName} | DeleteTimeSeriesAsync      |

### Get time series data

To get time series data it is necessary to have corresponding class prepared. It is possible to use Newtonsoft *JsonProperty* atributes. Or just to name your properties in the corresponding way so they could be deserialized. 

```csharp
public class TimeSeriesData 
{
        // for timestamp
        [JsonProperty("_time")]
        public DateTime Time { get; set; }

        // for aspect variable named "x"
        [JsonProperty("x")]
        public double X { get; set; }

        // for aspect variable named "y"
        [JsonProperty("y")]
        public double Y { get; set; }

        // for aspect variable named "z"
        [JsonProperty("z")]
        public double Z { get; set; }
}
```

After that you can pass it to generic method *GetTimeSeriesAsync*.

```csharp
var tsClient = _sdk.GetIotTimeSeriesClient();
var request = new GetTimeSeriesRequest()
{
    EntityId = "<asset-id>",
    PropertySetName = "<aspect-name>"
    From = DateTime.Now.AddDays(-1),
    To = DateTime.Now,
    Limit = 10
};
var timeSeries = (await tsClient.GetTimeSeriesAsync<TestTimeSeriesData>(request)).ToList();
```

### Put time series data

To put new time series data into the MindSphere you can use predefined class or anonymous type.

If you use your own class you need to name the properties in the corresponding way or to add Newtonsoft *JsonProperty* atributes. Otherwise the deserialization would fail.

```csharp
public class TimeSeriesData 
{
        // for timestamp
        [JsonProperty("_time")]
        public DateTime Time { get; set; }

        // for aspect variable named "x"
        [JsonProperty("x")]
        public double X { get; set; }

        // for aspect variable named "y"
        [JsonProperty("y")]
        public double Y { get; set; }

        // for aspect variable named "z"
        [JsonProperty("z")]
        public double Z { get; set; }
}
```

```csharp
var tsClient = _sdk.GetIotTimeSeriesClient();
DateTime nowUtc = DateTime.Now.ToUniversalTime();

// with class
List<TestTimeSeriesData> timeSeriesData = new()
{
    new TestTimeSeriesData(nowUtc, 0.5, 0.7, 0.3),
    new TestTimeSeriesData(nowUtc.AddMinutes(1), 0.8, 1.2, 0.7),
    new TestTimeSeriesData(nowUtc.AddMinutes(2), 1.6, 0.2, 0.5)
};

// with anonymous type
List<object> timeSeriesData = new()
{
    new { _time = DateTime.Now, x = 0.5, y = 0.7, z = 0.3 }),
    new { _time = DateTime.Now.AddMinutes(1), x = 0.8, y = 1.2, z = 0.7 }),
    new { _time = DateTime.Now.AddMinutes(2), x = 1.6, y = 0.2, z = 0.5 })
};

PutTimeSeriesRequest request = new()
{
    Data = timeSeriesData,
    EntityId = "<asset-id>",
    PropertySetName = "<aspect-name>"
};

await tsClient.PutTimeSeriesAsync(request);
```

## IoT time series aggregates client
For querying aggregated time series data.

| Method | Endpoint    | SDK                         |
|--------|-------------|-----------------------------|
| GET    | /aggregates | GetAggregateTimeSeriesAsync |

### Get time series aggregates

*GetAggregateTimeSeriesAsync* is generic method. It is necessary to set the type to a class derived from *AggregateSet* and to define expected MindSphere variables using properties of type *AggregateVariable* with corresponding names (or *JsonProperty*).

```csharp
public class AggregateTsData : AggregateSet
{
    // for aspect variable named "x"
    [JsonProperty("x")]
    public AggregateVariable X { get; set; }

    // for aspect variable named "y"
    [JsonProperty("y")]
    public AggregateVariable Y { get; set; }

    // for aspect variable named "z"
    [JsonProperty("z")]
    public AggregateVariable Z { get; set; }
}
```

```csharp
var iotAggregClient = _sdk.GetIotTsAggregateClient();
var request = new GetAggregateTimeSeriesRequest()
{
    AssetId = "<asset-id>",
    AspectName = "<aspect-name>"
    From = new DateTime(2021, 4, 25, 0, 0, 0),
    To = new DateTime(2021, 4, 26, 0, 0, 0),
    IntervalUnit = "minute",
    IntervalValue = 2
};

var tsAggregate = await iotAggregClient.GetAggregateTimeSeriesAsync<TestAggregateTsData>(request);
```

<!-- TODO ## Progress -->
<!-- clients progress overview -->