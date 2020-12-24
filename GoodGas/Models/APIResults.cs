using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoodGas.Models
{
	/// <summary>This class is used to encapsulate the result of an api method call.</summary>
	public class APIResult<T>
	{
		public APIResult( APIResultType code )
		{
			this.ResultCode = code;
			this.ResultObject = default( T );
		}

		public APIResult()
		{
			this.ResultCode = APIResultType.Unknown;
			this.ResultObject = default( T );
		}

		/// <summary>The error code on error</summary>
		[JsonProperty( "resultType" )]
		[JsonConverter( typeof( StringEnumConverter ) )]
		public APIResultType ResultCode { get; set; }

		/// <summary>The result of a successful api method call.</summary>
		[JsonProperty( "result" )]
		public T ResultObject { get; set; }

		/// <summary>Returns whether or not the api method call was successful.</summary>
		[JsonProperty( "isSuccess" )]
		public bool IsSuccess
		{
			get
			{
				return ( this.ResultCode == APIResultType.Success );
			}
		}

	}
}
