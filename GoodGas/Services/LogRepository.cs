using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodGas.Services
{
    /// <summary></summary>
	/// <param name="log"></param>
	public delegate void Log( string log );

    /// <summary></summary>
    public class LogRepository : IDataStore<string>
    {
        private readonly List<string> _logs;

        public LogRepository()
        {
            this._logs = new List<string>();
        }

        /// <summary>Event to subscribe to listen for changes</summary>
        public event Log LogIt;

        /// <summary>Call the update event</summary>
        public void OnLog( string msg )
        {
            if ( this.LogIt == null )
                return;

            this.LogIt( msg );
        }

        /// <summary></summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListAll()
        {
            return await Task.FromResult( this._logs );
        }

        /// <summary></summary>
        /// <returns></returns>
        public async Task<bool> AddItem( string log )
        {
            bool results = false;

            if ( !String.IsNullOrWhiteSpace( log ) )
            {
                this._logs.Add( log.Trim() );
                results = true;
            }

            return await Task.FromResult( results );
        }
    }
}
