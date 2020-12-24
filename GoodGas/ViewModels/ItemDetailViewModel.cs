using System;
using GoodGas.Models;

namespace GoodGas.ViewModels
{
	public class ItemDetailViewModel : BaseViewModel
	{
		/// <summary>Constructor</summary>
        /// <param name="item"></param>
		public ItemDetailViewModel( GasStation item = null )
		{
			Title = item?.Vendor;
			this.Item = item;
		}

		public GasStation Item { get; set; }
	}
}
