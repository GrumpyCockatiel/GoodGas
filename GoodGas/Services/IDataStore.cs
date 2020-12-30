using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodGas.Models;

namespace GoodGas.Services
{
	/// <summary>Interface that defines a Data Store</summary>
    /// <typeparam name="T">The business or domain object</typeparam>
    /// <remarks>Sometimes called IRepository which I prefer</remarks>
	public interface IDataStore<T>
	{
		/// <summary>Get a list of all the items</summary>
		/// <returns>A list of items of type T</returns>
		Task<IEnumerable<T>> ListAll();

		/// <summary></summary>
        /// <returns></returns>
		Task<bool> AddItem( T item );

		/* we're gonna implement this later */
		//Task<T> GetItem( string id );

		/* nothing below here will be used at this time */
		//Task<bool> AddItemAsync( T item );
		//Task<bool> UpdateItemAsync( T item );
		//Task<bool> DeleteItemAsync( string id );
		//Task<IEnumerable<T>> GetItemsAsync( bool forceRefresh = false );
	}
}
