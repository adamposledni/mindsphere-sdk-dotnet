![MindSphere image](/other/mdsp.png)
#  MindSphere SDK for .NET

Open-source .NET SDK for [MindSphere](https://siemens.mindsphere.io/) APIs. 

*This project was started on my own initiative. I am still a student and I am trying my best to develop useful solution for the absence of .NET SDK. Of course, any help is more than welcome. My goal is to develop SDK similar to [MindSphere SDK for Node.js](https://developer.mindsphere.io/resources/mindsphere-sdk-node/index.html).*

❗ Check out new features and changes in the version v1.1.1 -> [CHANGELOG.md](./CHANGELOG.md)  ❗

---

- [Installation](#Installation)
- [Getting started](#Getting-started)
- [Credentials](#Credentials)
    - [Application credentials](#Application-credentials)
    - [User credentials](#User-credentials)
    - [Update credentials](#Update-credentials)
- [Client configuration](#Client-configuration)
- [Examples](#Examples) 
    - [List assets](#List-assets)
    - [Add asset](#Add-asset)
    - [Download file](#Download-file)
    - [Upload file](#Upload-file)
    - [Get time series data](#Get-time-series-data)
    - [Put time series data](#Put-time-series-data)
    - [Get time series aggregates](#Get-time-series-aggregates)
    - [Add event](#Add-event)
    - [List events](#List-events)
    - [Update event type](#Update-event-type)
- [APIs overview](#APIs-overview)

---

## Installation

The SDK is hosted as a package on the [nuget.org](https://www.nuget.org/packages/MindSphereSdk.Core/). It is possible to use following command.

```
dotnet add package MindSphereSdk.Core --version 1.1.1
```

An alternative is to install MindSphereSdk.Core via NuGet Package Manager.

## Getting started

To start using MindSphere APIs via this SDK an instance of the client class is needed. There are multiple clients available (e.g. *AssetManagementClient*). Those clients are provided by *MindSphereApiSdk* class. That has to be initialized with credentials and configuration.

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

*AppCredentials* instance can be created using constructor with following parameters.

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
 
### Update credentials

To change provided credentials in the runtime use the method *UpdateCredentials*. However it is not possible to change the credential type. New credentials must be the same type as the previous.

```csharp
var creds = new UserCredentials("<access-token>");
var sdk = new MindSphereApiSdk(creds, config);

var newCreds = new UserCredentials("<new-access-token>");
sdk.UpdateCredentials(newCreds);
```

## Client configuration

Additional options are passed to SDK client via *ClientConfiguration* that has following default values.

| Property | Default value |
| --- | --- |
| Region | eu1 |
| Domain | mindsphere.io |
| Timeout | 100000 ms |
| Proxy | *empty* |

```csharp
var config = new ClientConfiguration(/* domain, region, timeout, proxy */);
```

## Examples

❗ This SDK only provides C# code to make HTTP calls to the MindSphere APIs. It does not provide any additional logic. Always refer to the official docs to find out more about the REST API. E.g. [Asset Mangement API specification](https://developer.mindsphere.io/apis/advanced-assetmanagement/api-assetmanagement-api-swagger-3-16-3.html). ❗

### List assets

```csharp
var assetClient = sdk.GetAssetManagementClient();
var request = new ListAssetsRequest()
{
    Size = 5,
    Page = 2,
    /* and more ... */
};
var result = await assetClient.ListAssetsAsync(request);
```

### Add asset

```csharp
var assetClient = sdk.GetAssetManagementClient();
var request = new AddAssetRequest()
{
    Asset = new AssetAdd()
    {
        Name = "<asset-name>",
        TypeId = "<asset-type-id>",
        ParentId = "<parent-asset-id>",
        /* and more ... */
    }
};
var response = await assetClient.AddAssetAsync(request);
```

### Download file

```csharp
var assetClient = sdk.GetAssetManagementClient();
var request = new DownloadFileRequest()
{
    Id = "<file-id>",
    /* and more ... */
};
string fileContent = await assetClient.DownloadFileAsync(request);
```

### Upload file

```csharp
// using MdspFile = MindSphereSdk.Core.AssetManagement.File;

var assetClient = sdk.GetAssetManagementClient();
MdspFile file;
using (var fs = new FileStream("test.txt", FileMode.Open))
{
    var request = new UploadFileRequest()
    {
        File = fs,
        Name = "test.txt",
        /* and more ... */
    };
    file = await assetClient.UploadFileAsync(request);
}
```

### Get time series data

To get time series data it is necessary to have corresponding class prepared. It is possible to use *MindSphereName* atribute or just to name the properties in the corresponding way so they could be deserialized. 

```csharp
var tsClient = sdk.GetIotTimeSeriesClient();
var request = new GetTimeSeriesRequest()
{
    EntityId = "<asset-id>",
    PropertySetName = "<aspect-name>"
    From = DateTime.Now.AddDays(-1),
    To = DateTime.Now,
    Limit = 10,
    /* and more ... */
};
var timeSeries = await tsClient.GetTimeSeriesAsync<TimeSeriesData>(request);
```

```csharp
// for each aspect type
public class TimeSeriesData 
{
    // for timestamp
    [MindSphereName("_time")]
    public DateTime Time { get; set; }

    // for aspect variable named "x"
    [MindSphereName("x")]
    public double X { get; set; }

    // for aspect variable named "y"
    [MindSphereName("y")]
    public double Y { get; set; }

    // for aspect variable named "z"
    [MindSphereName("z")]
    public double Z { get; set; }
}
```

### Put time series data

To put new time series data into the MindSphere either predefined class or anonymous type can be used.

Name the properties same as the variables in the MindSphere or add *MindSphereName* atribute. Otherwise the deserialization would fail.


```csharp
var tsClient = sdk.GetIotTimeSeriesClient();
DateTime nowUtc = DateTime.Now.ToUniversalTime();

// with class
List<TimeSeriesData> timeSeriesData = new()
{
    new TimeSeriesData(nowUtc, 0.5, 0.7, 0.3),
    new TimeSeriesData(nowUtc.AddMinutes(1), 0.8, 1.2, 0.7)
};

// with anonymous type
List<object> timeSeriesData = new()
{
    new { _time = DateTime.Now, x = 0.5, y = 0.7, z = 0.3 }),
    new { _time = DateTime.Now.AddMinutes(1), x = 0.8, y = 1.2, z = 0.7 })
};

PutTimeSeriesRequest request = new()
{
    Data = timeSeriesData,
    EntityId = "<asset-id>",
    PropertySetName = "<aspect-name>"
};

await tsClient.PutTimeSeriesAsync(request);
```

```csharp
// for each aspect type
public class TimeSeriesData 
{
    // for timestamp
    [MindSphereName("_time")]
    public DateTime Time { get; set; }

    // for aspect variable named "x"
    [MindSphereName("x")]
    public double X { get; set; }

    // for aspect variable named "y"
    [MindSphereName("y")]
    public double Y { get; set; }

    // for aspect variable named "z"
    [MindSphereName("z")]
    public double Z { get; set; }
}
```

### Get time series aggregates

*GetAggregateTimeSeriesAsync* is generic method. It is necessary to set the type to a class derived from *AggregateSet* and to define expected MindSphere variables using properties of type *AggregateVariable* with corresponding names (or use *MindSphereName* attribute).

```csharp
var iotAggregClient = sdk.GetIotTsAggregateClient();
var request = new GetAggregateTimeSeriesRequest()
{
    AssetId = "<asset-id>",
    AspectName = "<aspect-name>"
    From = new DateTime(2021, 4, 25, 0, 0, 0),
    To = new DateTime(2021, 4, 26, 0, 0, 0),
    IntervalUnit = "minute",
    IntervalValue = 2,
    /* and more ... */
};

var tsAggregate = await iotAggregClient.GetAggregateTimeSeriesAsync<AggregateTsData>(request);
```

```csharp
// for each apect type
public class AggregateTsData : AggregateSet
{
    // for aspect variable named "x"
    [MindSphereName("x")]
    public AggregateVariable X { get; set; }

    // for aspect variable named "y"
    [MindSphereName("y")]
    public AggregateVariable Y { get; set; }

    // for aspect variable named "z"
    [MindSphereName("z")]
    public AggregateVariable Z { get; set; }
}
```

### Add event

Since the event can have different properties based on their event type it is necessary to provide custom class to the SDK in which the custom properties will be specified. 

In the code below the *MyEventAdd* class is custom class derived from the SDK's *EventAddUpdate* class that provides basic properties such as *EntityId*.

❗ Make sure that the custom property name matches with the name in the MindSphere or use *MindSphereName* attribute. ❗

```csharp
var eventClient = sdk.GetEventManagementClient();
var request = new AddEventRequest()
{
    Event = new MyEventAdd() // custom class derived from the SDK's Event class
    {
        EntityId = "<asset-id>",
        Timestamp = DateTime.Now,
        Description = "<description>", // event type specific property
        Severity = 4 // event type specific property
    },
    /* and more ... */
};
// it is possible to replace Event with custom class derived from the Event
var response = await eventClient.AddEventAsync<Event>(request);
```

```csharp
public class MyEventAddUpdate : EventAddUpdate
{
    [MindSphereName("description")]
    public string Description { get; set; }

    [MindSphereName("severity")]
    public int Severity { get; set; }   
}
```

### List events

*ListEventsAsync* is also generic method that takes in a type derived from the *Event* class. This provides an option to fetch informations about events with custom properties.

❗ Make sure that the custom property name matches with the name in the MindSphere or use *MindSphereName* attribute. ❗

```csharp
var eventClient = sdk.GetEventManagementClient();
var request = new ListEventsRequest();
var events = await eventClient.ListEventsAsync<MyEvent>(request);
```

```csharp
public class MyEvent : Event
{
    [MindSphereName("description")]
    public string Description { get; set; }

    [MindSphereName("severity")]
    public int Severity { get; set; }
}
```

### Update event type

```csharp
var eventClient = sdk.GetEventManagementClient();
var request = new UpdateEventTypeRequest()
{
    EventTypeId = "<event-type-id>",
    IfMatch = "0",
    EventTypePatch = new EventTypePatch
    {
        Op = "replace",
        Path = "/scope",
        Value = "GLOBAL"
    },
    /* and more ...*/
};
var response = await eventClient.UpdateEventTypeAsync(request);
```

## APIs overview

### Asset management client

**Aspect types**
| Method | Endpoint          | SDK                   |
|--------|-------------------|-----------------------|
| GET    | /aspecttypes      | ListAspectTypesAsync  |
| PUT    | /aspecttypes/{id} | PutAspectTypeAsync    |
| PATCH  | /aspecttypes/{id} | PatchAspectTypeAsync  |
| GET    | /aspecttypes/{id} | GetAspectTypeAsync    |
| DELETE | /aspecttypes/{id} | DeleteAspectTypeAsync |

**Asset types**
| Method | Endpoint                               | SDK                                |
|--------|----------------------------------------|------------------------------------|
| GET    | /assettypes                            | ListAssetTypesAsync                |
| PUT    | /assettypes/{id}                       | PutAssetTypeAsync                  |
| PATCH  | /assettypes/{id}                       | PatchAssetTypeAsync                |
| GET    | /assettypes/{id}                       | GetAssetTypeAsync                  |
| DELETE | /assettypes/{id}                       | DeleteAssetTypeAsync               |
| PUT    | /assettypes/{id}/fileAssignments/{key} | AddAssetTypeFileAssignmentAsync    |
| DELETE | /assettypes/{id}/fileAssignments/{key} | DeleteAssetTypeFileAssignmentAsync |
| PATCH  | /assettypes/{id}/variables             | PatchAssetTypeVariablesAsync       |

**Assets**
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

**Structures**
| Method | Endpoint               | SDK                     |
|--------|------------------------|-------------------------|
| GET    | /assets/{id}/variables | ListAssetVariablesAsync |
| GET    | /assets/{id}/aspects   | ListAssetAspectsAsync   |

**Locations**
| Method | Endpoint              | SDK                      |
|--------|-----------------------|--------------------------|
| PUT    | /assets/{id}/location | PutAssetLocationAsync    |
| DELETE | /assets/{id}/location | DeleteAssetLocationAsync |

**Files**
| Method | Endpoint             | SDK               |
|--------|----------------------|-------------------|
| POST   | /files               | UploadFileAsync   |
| GET    | /files               | ListFilesAsync    |
| GET    | /files/{fileId}/file | DownloadFileAsync |
| GET    | /files/{fileId}      | GetFileAsync      |
| PUT    | /files/{fileId}      | UpdateFileAsync   |
| DELETE | /files/{fileId}      | DeleteFileAsync   |

**Asset model locks**
| Method | Endpoint    | SDK               |
|--------|-------------|-------------------|
| GET    | /model/lock | GetLockStateAsync |
| PUT    | /model/lock | PutLockStateAsync |

### IoT time series client

| Method | Endpoint                                 | SDK                        |
|--------|------------------------------------------|----------------------------|
| PUT    | /timeseries                              | PutTimeSeriesMultipleAsync |
| GET    | /timeseries/{entityId}/{propertySetName} | GetTimeSeriesAsync         |
| PUT    | /timeseries/{entityId}/{propertySetName} | PutTimeSeriesAsync         |
| DELETE | /timeseries/{entityId}/{propertySetName} | DeleteTimeSeriesAsync      |

### IoT time series aggregates client

| Method | Endpoint    | SDK                         |
|--------|-------------|-----------------------------|
| GET    | /aggregates | GetAggregateTimeSeriesAsync |

### Event management

**Events**

| Method | Endpoint          | SDK              |
|--------|-------------------|------------------|
| POST   | /events           | AddEventAsync    |
| GET    | /events           | ListEventsAsync  |
| GET    | /events/{eventId} | GetEventAsync    |
| PUT    | /events/{eventId} | UpdateEventAsync |

**Event types**

| Method | Endpoint                  | SDK                  |
|--------|---------------------------|----------------------|
| POST   | /eventTypes               | AddEventTypeAsync    |
| GET    | /eventTypes               | ListEventTypesAsync  |
| PATCH  | /eventTypes/{eventTypeId} | UpdateEventTypeAsync |
| GET    | /eventTypes/{eventTypeId} | GetEventTypeAsync    |
| DELETE | /eventTypes/{eventTypeId} | DeleteEventTypeAsync |

**Jobs**

| Method | Endpoint                  | SDK               |
|--------|---------------------------|-------------------|
| POST   | /deleteEventsJobs         | *not implemented* |
| GET    | /deleteEventsJobs/{jobId} | *not implemented* |
| POST   | /createEventsJobs         | *not implemented* |
| GET    | /createEventsJobs/{jobId} | *not implemented* |