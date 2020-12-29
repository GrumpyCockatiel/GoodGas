using System;
using System.Net;

namespace GoodGas.Models
{
    /// <summary>Wraps a response back from the service</summary>
    /// <typeparam name="T">The data type sent back from the service</typeparam>
    /// <remarks>Unused right now</remarks>
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
            this.StatusCode = 0;
            this.Data = new APIResult<T>();
        }

        /// <summary>HTTP Status code returned by the service</summary>
		public HttpStatusCode StatusCode { get; set; }

        /// <summary>The results back from the API</summary>
        public APIResult<T> Data { get; set; }

        /// <summary>Is HTTP Status a 200</summary>
        public bool IsAuthorized
        {
            get
            {
                return this.StatusCode >= HttpStatusCode.OK && this.StatusCode < HttpStatusCode.Ambiguous;
            }
        }

        /// <summary>The exception that occurred</summary>
        public bool IsException
        {
            get
            {
                return this.Exception != null;
            }
        }

        /// <summary>Any exception occured</summary>
        public System.Exception Exception { get; set; }
    }
}
