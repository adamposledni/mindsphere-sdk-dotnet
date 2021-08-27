using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Core.EventManagement
{
    #region Request

    /// <summary>
    /// Request for adding event
    /// </summary>
    public class AddEventRequest
    {
        /// <summary>
        /// Event
        /// </summary>
        public EventAdd Body { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities
        /// </summary>
        public bool IncludeShared { get; set; }
    }

    #endregion

    #region Event

    /// <summary>
    /// Event
    /// </summary>
    public abstract class Event
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("typeId")]
        public string TypeId { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        [JsonProperty("etag")]
        public int Etag { get; set; }
    }

    /// <summary>
    /// Event to add
    /// </summary>
    public class EventAdd
    {
        [JsonProperty("typeId")]
        public string TypeId { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("entityId")]
        public string EntityId { get; set; }
    }

    #endregion
}
