using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoodGas.Models
{
	/// <summary>Gas Station Entity</summary>
	public class GasStation
	{
		[JsonProperty( "id" )]
		public string RowKey { get; set; }

		[JsonProperty( PropertyName = "vendor" )]
		public string Vendor { get; set; }

		[JsonProperty( PropertyName = "address" )]
		public string Address { get; set; }

		[JsonProperty( PropertyName = "city" )]
		public string City { get; set; }

		[JsonProperty( PropertyName = "state" )]
		public string State { get; set; }

		[JsonProperty( PropertyName = "zip" )]
		public string Zip { get; set; }

		[JsonProperty( PropertyName = "latitude" )]
		public double Latitude { get; set; }

		[JsonProperty( PropertyName = "longitude" )]
		public double Longitude { get; set; }

		[JsonProperty( PropertyName = "secure" )]
		public bool Secure { get; set; }
	}
}
