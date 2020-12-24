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
    public class ItemsViewModel : BaseViewModel
    {
        public ItemsViewModel()
        {
            Title = "Browse";

            Items = new ObservableCollection<GasStation>();

            LoadItemsCommand = new Command( async () => await ExecuteLoadItemsCommand() );

            //MessagingCenter.Subscribe<NewItemPage, Item>( this, "AddItem", async ( obj, item ) =>
            // {
            //     var newItem = item as Item;
            //     Items.Add( newItem );
            //     await DataStore.AddItemAsync( newItem );
            // } );
        }

        public ObservableCollection<GasStation> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await DataStore.GetItemsAsync( true );

                foreach ( var item in items )
                {
                    Items.Add( item );
                }
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