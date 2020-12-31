using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoodGas.Logging
{
    /// <summary>Object representing a log event record.</summary>
    public class LogRecord
    {
        /// <summary>Constructor</summary>
        public LogRecord( LogLevel level )
        {
            this.Level = level;
        }

        /// <summary>Constructor</summary>
        public LogRecord() : this( LogLevel.Info )
        {
        }

        /// <summary>Unique ID of the record</summary>
        public long ID { get; set; }

        /// <summary>DateTime of the event preferably in UTC</summary>
        [JsonProperty( "timestamp" )]
        public DateTime Timestamp { get; set; }

        /// <summary>What was the source of the log - the app, service, ...</summary>
        [JsonProperty( "source" )]
        public string Source { get; set; }

        /// <summary>Severity</summary>
        /// <remarks>See enumerated LogLevels in Logging</remarks>
        [JsonConverter( typeof( StringEnumConverter ) )]
        [JsonProperty( "level" )]
        public LogLevel Level { get; set; }

        /// <summary>An optional category to help orgnaize log events.</summary>
        [JsonProperty( "category" )]
        public string Category { get; set; }

        /// <summary>The actual log message</summary>
        [JsonProperty( "message" )]
        public string Message { get; set; }

        /// <summary>Additional args to pass with the log for more detail</summary>
        public object[] Args { get; set; }
    }
}
