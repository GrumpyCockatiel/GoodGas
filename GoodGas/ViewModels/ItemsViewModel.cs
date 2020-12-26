using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using GoodGas.Models;
using GoodGas.Services;
using Xamarin.Forms;

namespace GoodGas.ViewModels
{
    /// <summary>The view model for the Items List Page</summary>
    public class ItemsViewModel : BaseViewModel
    {
        public ItemsViewModel()
        {
            this.Title = "Gas Stations";

            this.Items = new ObservableCollection<GasStation>();

            this.LoadItemsCommand = new Command( () =>
            {

                //async () => await ExecuteLoadItemsCommand()

                Task<bool> t = this.DataStore.ListGasStations( this.LoadModel );
            }
            );

            //MessagingCenter.Subscribe<NewItemPage, Item>( this, "AddItem", async ( obj, item ) =>
            // {
            //     var newItem = item as Item;
            //     Items.Add( newItem );
            //     await DataStore.AddItemAsync( newItem );
            // } );
        }

        #region [ Properties ]

        /// <summary>The model to bind to</summary>
        public ObservableCollection<GasStation> Items { get; set; }

        /// <summary>Command called to load the local Observable list</summary>
        public Command LoadItemsCommand { get; set; }

        #endregion [ Properties ]

        public void LoadModel( ServiceResponse<List<GasStation>> results )
        {
            if ( IsBusy )
                return;

            IsBusy = true;

            try
            {
                // check the response is good

                this.Items.Clear();

                results.Data.ResultObject.ForEach( i => this.Items.Add(i) ); 

                //foreach ( GasStation s in results.Data.ResultObject )
                //{
                //    // add a new map item
                //    this.Items.Add( s );
                //    //this.Items.Add( new MapItem( new Position( 29.7532963, -95.4024021 ), "Place 2", "Another place." ) );
                //}
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

        /// <summary></summary>
        /// <returns></returns>
        //public async Task ExecuteLoadItemsCommand()
        //{
        //    IsBusy = true;

        //    try
        //    {
        //        Items.Clear();

        //        var items = await this.DataStore.GetItemsAsync( true );

        //        foreach ( var item in items )
        //        {
        //            Items.Add( item );
        //        }
        //    }
        //    catch ( Exception ex )
        //    {
        //        Debug.WriteLine( ex );
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}