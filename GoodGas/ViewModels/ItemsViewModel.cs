using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GoodGas.Models;
using GoodGas.Services;
using Xamarin.Forms;

namespace GoodGas.ViewModels
{
    /// <summary>The view model for the Items List Page</summary>
    public class ItemsViewModel : BaseViewModel
    {
        #region [ Fields ]

        private List<GasStation> _items;

        #endregion [ Fields ]

        /// <summary>Constructor</summary>
        public ItemsViewModel()
        {
            // set the page view title
            this.Title = "Gas Stations";

            // init the local items collection
            this.Items = new List<GasStation>();

            // set the Load Items Command handler
            this.LoadItemsCommand = new Command( () =>
            {
                // refresh the data
                //async () => await ExecuteLoadItemsCommand()
                //Task<bool> t = this.DataStore.ListGasStations( this.LoadModel );
                Task<IEnumerable<GasStation>> t = this.DataStore.ListGasStations();
                t.ContinueWith( ant => { LoadModel( ant.Result.ToList() ); }  );
            } );

            //MessagingCenter.Subscribe<NewItemPage, Item>( this, "AddItem", async ( obj, item ) =>
            // {
            //     var newItem = item as Item;
            //     Items.Add( newItem );
            //     await DataStore.AddItemAsync( newItem );
            // } );
        }

        #region [ Properties ]

        /// <summary>Data Source from month drop down menu</summary>
        public List<GasStation> Items
        {
            get { return _items; }
            set { SetProperty( ref _items, value ); }
        }

        /// <summary>Command called to load the local Observable list</summary>
        public Command LoadItemsCommand { get; set; }

        #endregion [ Properties ]

        /// <summary></summary>
        /// <param name="results"></param>
        public void LoadModel( List<GasStation> results )
        {
            if ( IsBusy )
                return;

            IsBusy = true;

            try
            {
                this.Items = results;
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