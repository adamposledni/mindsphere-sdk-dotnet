using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.IotTimeSeries
{
    public interface ITimeSeriesData
    {
        DateTime Time { get; set; }
    }

    public class TimeSeriesObject
    {
        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        [JsonProperty("propertySetName")]
        public string PropertySetName { get; set; }

        [JsonProperty("data")]
        public IEnumerable<ITimeSeriesData> Data { get; set; }
    }

    public class GetTimeSeriesRequest
    {
        public string EntityId { get; set; }
        public string PropertySetName { get; set; }
    }

    public class PutTimeSeriesRequest
    {
        [JsonProperty("timeseries")]
        public IEnumerable<TimeSeriesObject> TimeSeries { get; set; }
    }

}
