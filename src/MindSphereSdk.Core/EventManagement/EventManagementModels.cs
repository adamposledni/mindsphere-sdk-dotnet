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
        public EventAdd Body { get; set; }
    }

    #endregion

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
}
