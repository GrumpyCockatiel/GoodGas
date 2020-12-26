using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoodGas.Models
{
	/// <summary>Gas Station Entity</summary>
	public class GasStation
	{
		/// <summary>The Entitys client ID</summary>
		[JsonProperty( "id" )]
		public string ID { get; set; }

		/// <summary></summary>
		[JsonProperty( PropertyName = "vendor" )]
		public string Vendor { get; set; }

		/// <summary></summary>
		[JsonProperty( PropertyName = "address" )]
		public string Address { get; set; }

		/// <summary></summary>
		[JsonProperty( PropertyName = "city" )]
		public string City { get; set; }

		/// <summary></summary>
		[JsonProperty( PropertyName = "state" )]
		public string State { get; set; }

		/// <summary></summary>
		[JsonProperty( PropertyName = "zip" )]
		public string Zip { get; set; }

		/// <summary></summary>
		[JsonProperty( PropertyName = "latitude" )]
		public double Latitude { get; set; }

		/// <summary></summary>
		[JsonProperty( PropertyName = "longitude" )]
		public double Longitude { get; set; }

		/// <summary>Flag indicates the station uses chipped CC reader</summary>
		[JsonProperty( PropertyName = "secure" )]
		public bool Secure { get; set; }
	}
}
