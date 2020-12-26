﻿using System;
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
				Task<bool> t = svc.ListGasStations( this.LoadModel );
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

		//public Action<ObservableCollection<MapItem>> OnUpdate { get; set; }

		/// <summary>Callback when stations are loaded</summary>
		public void LoadModel( ServiceResponse<List<GasStation>> results )
        {
			if ( this.IsBusy )
				return;

			this.IsBusy = true;

			try
			{
				this.Items.Clear();

				foreach (GasStation s in results.Data.ResultObject )
                {
					// add a new map item
					this.Items.Add( new MapItem( new Position( s.Latitude, s.Longitude ), s.Vendor ) );
				}

				// we want to know after ALL the map items have changed not on each one
			}
			catch ( Exception ex )
			{
				Debug.WriteLine( ex );
			}
			finally
			{
				this.IsBusy = false;
			}
        }
	}
}