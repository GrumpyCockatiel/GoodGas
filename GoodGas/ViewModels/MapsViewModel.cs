using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GoodGas.Models;
using GoodGas.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GoodGas.ViewModels
{
	/// <summary>Delegate for when a list of items is updated.</summary>
	public delegate void ItemsUpdated(Object sender);

	/// <summary>View model for the Maps View</summary>
	public class MapsViewModel : BaseViewModel
	{
        #region [ Fields ]

        private List<MapItem> _items;

        #endregion [ Fields ]

        /// <summary>Constructor</summary>
        public MapsViewModel()
		{
            // set the title
			this.Title = "Map";

            // init the items
			this.Items = new List<MapItem>();

			// set a handler for load items
			this.LoadItemsCommand = new Command( () =>
			{
                // refresh the data
                Task<IEnumerable<GasStation>> t = this.DataStore.ListAll();
                t.ContinueWith( ant => { LoadModel( ant.Result.ToList() ); } );
            } );
        }

        /// <summary>Data Source </summary>
        public List<MapItem> Items
        {
            get { return _items; }
            set { SetProperty( ref _items, value ); }
        }

        /// <summary>Command to load data into the view model</summary>
        public Command LoadItemsCommand { get; set; }

		#region [ Events ]

		/// <summary>Event when</summary>
		public event ItemsUpdated ItemsUpdated;

        #endregion [ Events ]

        #region [ Methods ]

        /// <summary>Call the update event</summary>
        public void OnItemsUpdated()
        {
            if ( this.ItemsUpdated == null )
                return;

            this.ItemsUpdated( this );
        }

        /// <summary>Callback when stations are loaded</summary>
        public void LoadModel( List<GasStation> results )
        {
            if ( this.IsBusy )
                return;

            this.IsBusy = true;

            try
            {
                // update the items with Map Items
                this.Items = results.Select( i => new MapItem( new Position( i.Latitude, i.Longitude ), i.Vendor ) ).ToList();

                // we want to know after ALL the map items have changed not on each one
                this.OnItemsUpdated();
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

        #endregion [ Methods ]

    }
}