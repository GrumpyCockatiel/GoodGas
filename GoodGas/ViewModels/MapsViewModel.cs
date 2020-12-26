using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using GoodGas.Models;
using Xamarin.Forms.Maps;
using GoodGas.Services;
using System.Collections.Generic;

namespace GoodGas.ViewModels
{
	/// <summary>View model for the Maps View</summary>
	public class MapsViewModel : BaseViewModel
	{
		/// <summary>Constructor</summary>
		public MapsViewModel()
		{
			this.Title = "Map";

			this.Items = new ObservableCollection<MapItem>();

			this.LoadItemsCommand = new Command( () =>
			{
				GasService svc = new GasService();
				//svc.GetAllStations( this.LoadStations );
				Task<bool> t = svc.ListGasStations( this.LoadStations );
			} );

			//MessagingCenter.Subscribe<NewItemPage, MapItem>( this, "AddItem", async ( obj, item ) =>
			// {
			//	 var newItem = item as MapItem;
			//	 Items.Add( newItem );
			//	 await DataStore.AddItemAsync( newItem );
			// } );
		}

		/// <summary>A observable collection of map items specific to this view</summary>
		public ObservableCollection<MapItem> Items { get; set; }

		/// <summary>Command to load data into the view model</summary>
		public Command LoadItemsCommand { get; set; }

		/// <summary>Callback when stations are loaded</summary>
		public void LoadStations( ServiceResponse<List<GasStation>> results )
        {
			if ( IsBusy )
				return;

			IsBusy = true;

			try
			{
				Items.Clear();

				foreach (GasStation s in results.Data.ResultObject )
                {
					// add a new map item
					this.Items.Add( new MapItem( new Position( s.Latitude, s.Longitude ), s.Vendor ) );
				}

				// needs to recenter the map over the fist item returned
			}
			catch ( Exception ex )
			{
				Debug.WriteLine( ex );
			}
			finally
			{
				IsBusy = false;
			}
        }
	}
}