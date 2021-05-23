using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.IotTimeSeries
{
    /// <summary>
    /// Time Series for one asset and one aspect
    /// </summary>
    public class TimeSeries
    {
        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        [JsonProperty("propertySetName")]
        public string PropertySetName { get; set; }

        [JsonProperty("data")]
        public IEnumerable<object> Data { get; set; }
    }

    #region Request

    /// <summary>
    /// Request for getting time series
    /// </summary>
    public class GetTimeSeriesRequest
    {
        public string EntityId { get; set; }
        public string PropertySetName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? Limit { get; set; }
        public string Select { get; set; }
        public string Sort { get; set; }
        public bool? LatestValue { get; set; }
    }

    /// <summary>
    /// Request for putting time series
    /// </summary>
    public class PutTimeSeriesRequest
    {
        [JsonProperty("timeseries")]
        public IEnumerable<TimeSeries> TimeSeries { get; set; }
    }

    /// <summary>
    /// Request for deleting time series
    /// </summary>
    public class DeleteTimeSeriesRequest
    {
        public string EntityId { get; set; }
        public string PropertySetName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    #endregion
}
