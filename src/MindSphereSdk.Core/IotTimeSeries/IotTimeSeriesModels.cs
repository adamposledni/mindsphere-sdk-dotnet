using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MindSphereSdk.Core.IotTimeSeries
{
    #region TimeSeries
    /// <summary>
    /// Time Series for one asset and one aspect.
    /// </summary>
    public class TimeSeriesSet
    {
        /// <summary>
        /// Unique identifier of the asset.
        /// </summary>
        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        /// <summary>
        /// Name of the aspect.
        /// </summary>
        [JsonProperty("propertySetName")]
        public string PropertySetName { get; set; }

        /// <summary>
        /// Time series data.
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<object> Data { get; set; }
    }
    #endregion

    #region Request

    /// <summary>
    /// Request for putting time series.
    /// </summary>
    public class PutTimeSeriesMultipleRequest
    {
        /// <summary>
        /// Time series.
        /// </summary>
        [JsonProperty("timeseries")]
        public IEnumerable<TimeSeriesSet> TimeSeries { get; set; }
    }

    /// <summary>
    /// Request for getting time series.
    /// </summary>
    public class GetTimeSeriesRequest
    {
        /// <summary>
        /// Beginning of the time range to be retrieved (exclusive).
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        /// End of the time range to be retrieved (inclusive).
        /// </summary>
        public DateTime? To { get; set; }

        /// <summary>
        /// Maximum number of time series data items to be retrieved.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Comma-separated list of properties to be returned.
        /// </summary>
        public string Select { get; set; }

        /// <summary>
        /// Define sorting order of returned data.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// If true, only the latest value of each property is returned.
        /// </summary>
        public bool? LatestValue { get; set; }

        /// <summary>
        /// Unique identifier of the asset.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Name of the aspect.
        /// </summary>
        public string PropertySetName { get; set; }
    }

    /// <summary>
    /// Request for putting time series.
    /// </summary>
    public class PutTimeSeriesRequest
    {
        /// <summary>
        /// Unique identifier of the asset.
        /// </summary>
        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        /// <summary>
        /// Name of the aspect.
        /// </summary>
        [JsonProperty("propertySetName")]
        public string PropertySetName { get; set; }

        /// <summary>
        /// Time series data.
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<object> Data { get; set; }
    }

    /// <summary>
    /// Request for deleting time series.
    /// </summary>
    public class DeleteTimeSeriesRequest
    {
        /// <summary>
        /// Beginning of the time range to be retrieved (exclusive).
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        /// End of the time range to be retrieved (inclusive).
        /// </summary>
        public DateTime? To { get; set; }

        /// <summary>
        /// Unique identifier of the asset.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Name of the aspect.
        /// </summary>
        public string PropertySetName { get; set; }
    }

    #endregion
}
