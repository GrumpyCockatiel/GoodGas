using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using GoodGas.Models;

namespace GoodGas.Services
{
    /// <summary>Defines a delegate to call when an API function is complete</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="results"></param>
    public delegate void APICallback<T>( ServiceResponse<T> results );

    /// <summary>Services a base class for a REST Service</summary>
    public class RESTServiceBase
    {
        #region [ Fields ]

        /// <summary></summary>
        /// <param name="env"></param>
        public RESTServiceBase( string baseURL )
        {
            this.APIBaseURL = baseURL;

            // create a new client
            this.Client = new HttpClient();
        }

        #endregion [ Fields ]

        #region [ Properties ]

        // <summary>HTTP Client to use for all calls</summary>
        protected HttpClient Client { get; set; }

        // <summary>Session token to add to the auth header</summary>
        public string SessionID { get; set; }

        /// <summary>header to use for security</summary>
        protected static readonly string TimestampHeader = "x-timestamp";

        /// <summary>header to use for security</summary>
        public string AuthTokenHeader { get; set; } = "x-token";

        /// <summary>API Key</summary>
        protected string APIKey = String.Empty;

        /// <summary>API Base URL</summary>
        private string APIBaseURL = String.Empty;

        #endregion [ Properties ]

        #region [ Methods ]

        /// <summary>Send a POST request</summary>
        /// <param name="method"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        protected HttpRequestMessage PostRequest( string method, string body, bool secure = false )
        {
            HttpRequestMessage message = new HttpRequestMessage( HttpMethod.Post, $"{this.APIBaseURL}/{method}" );
            message.Headers.Clear();
            message.Headers.Add( "x-functions-key", this.APIKey );
            message.Headers.Add( TimestampHeader, DateTime.UtcNow.ToString( "o" ) );

            if ( secure )
                message.Headers.Add( AuthTokenHeader, this.SessionID );

            message.Content = new StringContent( body, Encoding.UTF8, "application/json" );
            byte[] bytes = Encoding.UTF8.GetBytes( body );
            message.Content.Headers.Add( "Content-Length", bytes.Length.ToString() );

            return message;
        }

        /// <summary>Send a GET Request with no parameters</summary>
        /// <param name="method"></param>
        /// <param name="anon"></param>
        /// <param name="secure"></param>
        /// <returns></returns>
        protected HttpRequestMessage GetRequest( string method, bool anon = true, bool secure = false )
        {
            return this.GetRequest( method, new Dictionary<string, string>(), anon, secure );
        }

        /// <summary>GET Request with parameters</summary>
        /// <param name="method"></param>
        /// <returns></returns>
        protected HttpRequestMessage GetRequest( string method, Dictionary<string, string> param, bool anon, bool secure = false )
        {
            UriBuilder sb = new UriBuilder( $"{this.APIBaseURL}/{method}" );

            if ( param != null && param.Count > 0 )
            {
                foreach ( KeyValuePair<string, string> kvp in param )
                {
                    if ( String.IsNullOrWhiteSpace( kvp.Key ) || String.IsNullOrWhiteSpace( kvp.Value ) )
                        continue;

                    sb.Query += $"{kvp.Key.Trim()}={kvp.Value.Trim()}&";
                }
            }

            HttpRequestMessage message = new HttpRequestMessage( HttpMethod.Get, sb.Uri );
            message.Headers.Clear();
            message.Headers.Add( TimestampHeader, DateTime.UtcNow.ToString( "o" ) );

            if ( !anon )
                message.Headers.Add( "x-functions-key", this.APIKey );

            if ( secure )
                message.Headers.Add( AuthTokenHeader, this.SessionID );

            return message;
        }

        /// <summary>Converts a string to a boolean value based on the first char</summary>
        /// <returns></returns>
        private static bool GetBoolValue( string value )
        {
            if ( String.IsNullOrWhiteSpace( value ) )
                return false;

            char leading = value.Trim().ToLower()[0];

            return ( leading == 't' || leading == 'y' || leading == '1' ) ? true : false;
        }

        #endregion [ Methods ]
    }
}
