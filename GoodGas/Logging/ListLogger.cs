using System;
using System.Collections.Generic;

namespace GoodGas.Logging
{
	/// <summary>List Logger logs to an in memory list of strings</summary>
	public class ListLogger : ILogger, ILogRepository
	{
		#region [ Fields ]

		public static readonly string ErrorCategory = "Exception";

		/// <summary></summary>
		private LogLevel _level = LogLevel.Off;

		/// <summary></summary>
		private string _src = null;

		/// <summary></summary>
		private readonly List<LogRecord> _logs;

		#endregion [ Fields ]

		#region [ Constructors ]

		/// <summary></summary>
		/// <param name="source">Who is doing the logging.</param>
		public ListLogger( string source ) : this(source, LogLevel.Off)
		{
		}

		/// <summary></summary>
		/// <param name="source">Who is doing the logging.</param>
		/// <param name="baseLevel">Minimum level to log.</param>
		public ListLogger( string source, LogLevel baseLevel )
		{
			this._logs = new List<LogRecord>();
			this.Level = baseLevel;
			this.Source = source;
		}

		#endregion [ Constructors ]

		//public event LogHandler LogIt;

		///// <summary>Call the update event</summary>
		//public void DoLog( string msg )
		//{
		//	if ( this.LogIt == null )
		//		return;

		//	this.LogIt( this, msg );
		//}

		#region [ Properties ]

		public List<LogRecord> List
        {
			get => this._logs;
        }

		/// <summary>The minimum level inclusive to log based on the LogLevel enumeration [level,...]</summary>
		public LogLevel Level
		{
			get { return this._level; }
			set { this._level = value; }
		}

		/// <summary>The logging source. Who is doing the logging.</summary>
		public string Source
		{
			get { return this._src; }
			set
			{
				if ( value != null )
					this._src = value.Trim();
			}
		}

		#endregion [ Properties ]

		#region [ Methods ]

		/// <summary></summary>
		/// <param name="message"></param>
		public void Debug( string message )
		{
			this.InsertLog( this.Source, LogLevel.Debug, null, message, null );
		}

		/// <summary></summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		public void Log( string message, LogLevel level = LogLevel.Info )
		{
			this.InsertLog( this.Source, level, null, message, null );
		}

		/// <summary></summary>
		/// <param name="message"></param>
		public void Log( LogRecord message )
		{
			this.InsertLog( this.Source, message.Level, message.Category, message.Message, message.Args );
		}

		/// <summary></summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		public void Log( string message, string category, LogLevel level = LogLevel.Info )
		{
			this.InsertLog( this.Source, level, category, message, null );
		}

		/// <summary></summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
		/// <param name="args"></param>
		public void Log( string message, string category, LogLevel level, params object[] args )
		{
			this.InsertLog( this.Source, level, category, message, args );
		}

		/// <summary></summary>
		/// <param name="exp"></param>
		public void Log( System.Exception exp )
		{
			this.InsertLog( this.Source, LogLevel.Error, ErrorCategory, exp.ToLogMsg( true ), null );
		}

		/// <summary></summary>
		/// <param name="exp"></param>
		public void Log( Exception exp, params object[] args )
		{
			this.InsertLog( this.Source, LogLevel.Error, ErrorCategory, exp.ToLogMsg( true ), args );
		}

		/// <summary>Base logging method</summary>
		/// <param name="logger">The source of the source such as the application name.</param>
		/// <param name="lvl">The std log level as defined by Log4j</param>
		/// <param name="category">An application specific category that can be used for further organization, or routing to differnt locations/</param>
		/// <param name="msg">The actual message to log</param>
		/// <param name="args">any additional data fields to append to the log message. Used for debugging.</param>
		/// <returns></returns>
		protected int InsertLog( string logger, LogLevel lvl, string category, string msg, params object[] args )
		{
			if ( lvl < this.Level )
				return 0;

			// append category
			category = String.IsNullOrWhiteSpace( category ) ? String.Empty : category.Trim();

			// message
			msg = String.IsNullOrWhiteSpace( msg ) ? String.Empty : msg.Trim();

			this._logs.Add( new LogRecord {
				Message = msg,
				Level = lvl,
				Category = category,
				Source = logger,
				Timestamp = DateTime.UtcNow,
				Args = args
			});

			return 1;
		}

		#endregion [ Methods ]
	}
}
