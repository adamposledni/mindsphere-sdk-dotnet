using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.IotTimeSeries
{
    /// <summary>
    /// Time Series object for one asset and one aspect
    /// </summary>
    public class TimeSeriesObject
    {
        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        [JsonProperty("propertySetName")]
        public string PropertySetName { get; set; }

        [JsonProperty("data")]
        public IEnumerable<object> Data { get; set; }
    }

    /// <summary>
    /// Request object for getting time series
    /// </summary>
    public class GetTimeSeriesRequest
    {
        public string EntityId { get; set; }
        public string PropertySetName { get; set; }
    }

    /// <summary>
    /// Request object for putting time series
    /// </summary>
    public class PutTimeSeriesRequest
    {
        [JsonProperty("timeseries")]
        public IEnumerable<TimeSeriesObject> TimeSeries { get; set; }
    }

}
