using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using GoodGas.ViewModels;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using GoodGas.Models;

namespace GoodGas.Views
{
	/// <summary></summary>
	[DesignTimeVisible( false )]
	public partial class MapsPage : ContentPage
	{
		/// <summary>Direct reference to the view model</summary>

		private MapsViewModel _viewModel;

		public MapsPage()
		{
			InitializeComponent();

			// set this binding context to a new MapViewsModel
			this.BindingContext = this._viewModel = new MapsViewModel();

			// set the inital position over DT Houston
			this.Center = new MapSpan( new Position( 29.7507, -95.362 ), 0.01, 0.01 );

			// watch the model list for done changes
			this._viewModel.ItemsUpdated += DoMapItemsChanged;

		}

		/// <summary>View model lets us know the maps items have been completely updated</summary>
		private void DoMapItemsChanged(Object sender)
		{
			if ( this._viewModel != null && this._viewModel.Items != null && this._viewModel.Items.Count > 0 )
			{
				int idx = this._viewModel.Items.Count - 1;
				this.Center = new MapSpan( this._viewModel.Items[idx].Position, 0.01, 0.01 );
			}
		}

		/// <summary>This will update on each item added</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void DoItemsChanged( object sender, NotifyCollectionChangedEventArgs e )
		//      {
		//	ObservableCollection<MapItem> items = sender as ObservableCollection<MapItem>;

		//	if ( items != null && items.Count > 0 )
		//          {
		//		this.Center = new MapSpan( items[0].Position, 0.01, 0.01 );
		//	}
		//      }

		/// <summary></summary>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			// if there are no items - fetch
			if ( this._viewModel.Items.Count < 1 )
				this._viewModel.LoadItemsCommand.Execute( null );
		}

		/// <summary>Centers the map view</summary>
		protected MapSpan Center
		{
			set
			{
				this.gasMap.MoveToRegion( value );
			}
		}
	}
}