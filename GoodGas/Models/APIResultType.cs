using System;

namespace GoodGas.Models
{
	/// <summary>Code that indicates the sucess or failture of an API call.</summary>
	public enum APIResultType
	{
		/// <summary>Normal, active state. Carry on.</summary>
		Success = 0,
		/// <summary>The app is offline. No one can login.</summary>
		Offline = 7,
		/// <summary>The input parameters do not validate</summary>
		InvalidInput = 21,
		/// <summary>No results data was found to return</summary>
		NoResults = 22,
		/// <summary>Failed a logic rule, see the logs or additional data</summary>
		FailedRule = 23,
		/// <summary>The user role is not authorized to access this method.</summary>
		Unauthorized = 31,
		/// <summary>An exception was thrown, see the logs</summary>
		Exception = 99,
		/// <summary>Error unknown, check the logs</summary>
		Unknown = 100
	}
}
