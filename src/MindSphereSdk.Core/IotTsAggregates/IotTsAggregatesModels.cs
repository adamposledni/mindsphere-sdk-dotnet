using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MindSphereSdk.Core.IotTsAggregates
{
    #region AggregateTimeSeries

    /// <summary>
    /// Aggregate time series data wrapper
    /// </summary>
    internal class AggregateWrapper<T> where T : AggregateSet
    {
        [JsonProperty("aggregates")]
        public IEnumerable<T> Aggregates { get; set; }
    }

    /// <summary>
    /// Aggregate time series set
    /// </summary>
    public abstract class AggregateSet
    {
        [JsonProperty("starttime")]
        public DateTime StartTime { get; set; }

        [JsonProperty("endtime")]
        public DateTime EndTime { get; set; }
    }

    /// <summary>
    /// Aggregate time series variable
    /// </summary>
    public class AggregateVariable
    {
        [JsonProperty("firsttime")]
        public DateTime FirstTime { get; set; }

        [JsonProperty("lasttime")]
        public DateTime LastTime { get; set; }

        [JsonProperty("mintime")]
        public DateTime MinTime { get; set; }

        [JsonProperty("maxtime")]
        public DateTime MaxTime { get; set; }

        [JsonProperty("minvalue")]
        public double MinValue { get; set; }

        [JsonProperty("maxvalue")]
        public double MaxValue { get; set; }

        [JsonProperty("firstvalue")]
        public double FirstValue { get; set; }

        [JsonProperty("lastvalue")]
        public double LastValue { get; set; }

        [JsonProperty("countbad")]
        public int CountBad { get; set; }

        [JsonProperty("countgood")]
        public int CountGood { get; set; }

        [JsonProperty("countuncertain")]
        public int CountUncertain { get; set; }

        [JsonProperty("sum")]
        public double Sum { get; set; }

        [JsonProperty("average")]
        public double Average { get; set; }

        [JsonProperty("sd")]
        public double Sd { get; set; }
    }
    #endregion

    #region Request

    /// <summary>
    /// Request for getting aggregate time series
    /// </summary>
    public class GetAggregateTimeSeriesRequest
    {
        /// <summary>
        /// Unique identifier of the asset
        /// </summary>
        public string AssetId { get; set; }

        /// <summary>
        /// Name of the aspect
        /// </summary>
        public string AspectName { get; set; }

        /// <summary>
        /// Beginning of the time range to read
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        /// End of the time range to read
        /// </summary>
        public DateTime? To { get; set; }

        /// <summary>
        /// Interval duration for the aggregates in intervalUnits
        /// </summary>
        public int? IntervalValue { get; set; }

        /// <summary>
        /// Interval duration unit for the aggregates
        /// </summary>
        public string IntervalUnit { get; set; }

        /// <summary>
        /// Variables names to filter out variables
        /// </summary>
        public string Select { get; set; }

        /// <summary>
        /// Number of aggregate objects
        /// </summary>
        public int? Count { get; set; }
    }

    #endregion
}
