using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using GoodGas.ViewModels;
using GoodGas.Models;

namespace GoodGas.Views
{
    /// <summary>List of items view page</summary>
    public partial class ItemsPage : ContentPage
    {
        private ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            // set the bind context
            BindingContext = _viewModel = new ItemsViewModel();
        }

        async void OnItemSelected( object sender, EventArgs args )
        {
            var layout = (BindableObject)sender;

            var item = (GasStation)layout.BindingContext;

            await Navigation.PushAsync( new ItemDetailPage( new ItemDetailViewModel( item ) ) );
        }

        async void AddItem_Clicked( object sender, EventArgs e )
        {
            await Navigation.PushModalAsync( new NavigationPage( new NewItemPage() ) );
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if ( this._viewModel.Items.Count == 0 )
                this._viewModel.IsBusy = true;
        }
    }
}