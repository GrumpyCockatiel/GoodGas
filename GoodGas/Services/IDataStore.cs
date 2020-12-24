using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodGas.Services
{
	/// <summary>Interface that defines a Data Store</summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>A project template class</remarks>
	public interface IDataStore<T>
	{
		//Task<bool> AddItemAsync( T item );
		//Task<bool> UpdateItemAsync( T item );
		//Task<bool> DeleteItemAsync( string id );
		//Task<T> GetItemAsync( string id );
		Task<IEnumerable<T>> GetItemsAsync( bool forceRefresh = false );
	}
}
