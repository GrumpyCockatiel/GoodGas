using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodGas.Models;

namespace GoodGas.Services
{
	/// <summary>Interface that defines a Data Store</summary>
    /// <typeparam name="T">Typical a BO stored in the data store.</typeparam>
    /// <remarks>We're going to modify this interface for our own backend</remarks>
	public interface IDataStore<T>
	{
		//Task<bool> AddItemAsync( T item );
		//Task<bool> UpdateItemAsync( T item );
		//Task<bool> DeleteItemAsync( string id );
		//Task<T> GetItemAsync( string id );
		//Task<IEnumerable<T>> GetItemsAsync( bool forceRefresh = false );

		Task<bool> ListGasStations( APICallback<List<GasStation>> callback );
	}
}
