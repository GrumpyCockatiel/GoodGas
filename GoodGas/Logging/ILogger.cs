using System;
using System.Collections.Generic;

namespace GoodGas.Logging
{
	/// <summary>Delegate to define an event logger</summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
	public delegate void LogHandler( object sender, string log );

	/// <summary>Interface for retrieving logs</summary>
	public interface ILogRepository
    {
		List<LogRecord> List { get;  }
    }

	/// <summary>Interface for logging/inputing messages</summary>
	public interface ILogger
	{
		/// <summary>The minimum logging this logger will accept.</summary>
        /// <remarks>Incoming logs less than this level will be ignored.</remarks>
		LogLevel Level { set; }

		/// <summary>Insert a type Debug log</summary>
		void Debug( string message );

		/// <summary>Log a full log object</summary>
		void Log( LogRecord message );

		/// <summary>Log to the default logger</summary>
		void Log( string message, LogLevel level = LogLevel.Info );

		/// <summary>Log to a specific category</summary>
		void Log( string message, string category, LogLevel level = LogLevel.Info );

		/// <summary>Log to a category with additional info</summary>
		void Log( string message, string category, LogLevel level, params object[] args );

		/// <summary>Log an exception as type Error</summary>
		void Log( Exception exception );

		/// <summary>Log an exception as type Error and additional args</summary>
		void Log( Exception exp, params object[] args );
	}
}
