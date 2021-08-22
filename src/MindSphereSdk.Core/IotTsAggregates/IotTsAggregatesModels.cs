using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string AssetId { get; set; }
        public string AspectName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? IntervalValue { get; set; }
        public string IntervalUnit { get; set; }
        public string Select { get; set; }
        public int? Count { get; set; }
    }

    #endregion
}
