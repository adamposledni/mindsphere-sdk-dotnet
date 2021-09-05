using MindSphereSdk.Core.AssetManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MindSphereSdk.Core.EventManagement
{
    #region Embedded

    /// <summary>
    /// Embedded event list.
    /// </summary>
    /// <typeparam name="T">
    /// Type derived from Event or dynamic.
    /// </typeparam>
    internal class EmbeddedEventList<T>
    {
        [JsonProperty("events")]
        public IEnumerable<T> Events { get; set; }
    }

    #endregion

    #region Event

    /// <summary>
    /// Event.
    /// </summary>
    public class Event
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
    /// Event to add.
    /// </summary>
    public class EventAddUpdate
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

    #region Request

    /// <summary>
    /// Request for adding an event.
    /// </summary>
    public class AddEventRequest
    {
        /// <summary>
        /// Event.
        /// </summary>
        public EventAddUpdate Event { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for listing events
    /// </summary>
    public class ListEventsRequest
    {
        /// <summary>
        /// Specifies the number of elements in a page.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Specifies the requested page index.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Specifies the additional filtering criteria.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Specifies the ordering of returned elements.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// Optional paramater, if we want to retrieve the history of an event which is based on using the same correlationID, entityID, typeID.
        /// </summary>
        public bool? History { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }

        /// <summary>
        /// To specify if page caching is enabled or not. This property enables faster fetching of events from multiple pages for same filter criteria.
        /// </summary>
        public bool? EnablePageCache { get; set; }
    }

    /// <summary>
    /// Request for getting an event.
    /// </summary>
    public class GetEventRequest
    {
        /// <summary>
        /// ID of an event.
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// ETag hash of previous request to allow caching.
        /// </summary>
        public string IfNoneMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for updateing an event.
    /// </summary>
    public class UpdateEventRequest
    {
        /// <summary>
        /// ID of an event.
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// Event content to be created or updated.
        /// </summary>
        public EventAddUpdate Event { get; set; }

        /// <summary>
        /// Used for optimistic concurrency control.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    #endregion
}
