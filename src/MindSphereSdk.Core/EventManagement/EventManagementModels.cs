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

    /// <summary>
    /// Embedded event type list.
    /// </summary>
    internal class EmbeddedEventTypeList
    {
        [JsonProperty("eventTypes")]
        public IEnumerable<EventType> EventTypes { get; set; }
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

    #region Field

    /// <summary>
    /// Event type field.
    /// </summary>
    public class Field
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("filterable")]
        public bool? Filterable { get; set; }

        [JsonProperty("required")]
        public bool? Required { get; set; }

        [JsonProperty("updatable")]
        public bool? Updatable { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("values")]
        public IEnumerable<string> Values { get; set; }
    }

    /// <summary>
    /// Event type field to add.
    /// </summary>
    public class FieldAdd
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("filterable")]
        public bool? Filterable { get; set; }

        [JsonProperty("required")]
        public bool? Required { get; set; }

        [JsonProperty("updatable")]
        public bool? Updatable { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("values")]
        public IEnumerable<string> Values { get; set; }
    }

    #endregion

    #region Event type

    /// <summary>
    /// Event type.
    /// </summary>
    public class EventType
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }

        [JsonProperty("etag")]
        public int Etag { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("fields")]
        public IEnumerable<Field> Fields { get; set; }
    }

    /// <summary>
    /// Event type to add.
    /// </summary>
    public class EventTypeAdd
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("ttl")]
        public int? Ttl { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("fields")]
        public IEnumerable<FieldAdd> Fields { get; set; }
    }

    /// <summary>
    /// Event type (patch).
    /// </summary>
    public class EventTypePatch
    {
        [JsonProperty("op")]
        public string Op { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
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
    /// Request for listing events.
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


    /// <summary>
    /// Request for adding an event type.
    /// </summary>
    public class AddEventTypeRequest
    {
        /// <summary>
        /// Event type content to be created.
        /// </summary>
        public EventTypeAdd EventType { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for listing event types.
    /// </summary>
    public class ListEventTypesRequest
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
        /// Specifies if the operation should take into account shared aspect types.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for updating an event type.
    /// </summary>
    public class UpdateEventTypeRequest
    {
        /// <summary>
        /// ID of an event type.
        /// </summary>
        public string EventTypeId { get; set; }

        /// <summary>
        /// Used for optimistic concurrency control.
        /// </summary>
        public string IfMatch { get; set; }

        /// <summary>
        /// New value for the given attribute or the field, that should be applied, depending on the op value.
        /// </summary>
        public EventTypePatch EventTypePatch { get; set; }

        /// <summary>
        /// Specifies if the operation should take into account shared entities.
        /// </summary>
        public bool? IncludeShared { get; set; }
    }

    /// <summary>
    /// Request for getting an event type.
    /// </summary>
    public class GetEventTypeRequest
    {
        /// <summary>
        /// ID of an event type.
        /// </summary>
        public string EventTypeId { get; set; }

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
    /// Request for deleting an event type.
    /// </summary>
    public class DeleteEventTypeRequest
    {
        /// <summary>
        /// ID of an event type.
        /// </summary>
        public string EventTypeId { get; set; }

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
