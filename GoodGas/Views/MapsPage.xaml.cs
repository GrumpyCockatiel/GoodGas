using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using GoodGas.Models;
using GoodGas.Views;
using GoodGas.ViewModels;
using Xamarin.Essentials;

namespace GoodGas.Views
{
	/// <summary></summary>
	[DesignTimeVisible( false )]
	public partial class MapsPage : ContentPage
	{
		private MapsViewModel _viewModel;

		public MapsPage()
		{
			InitializeComponent();

			this.BindingContext = this._viewModel = new MapsViewModel();

			// set the inital position over DT Houston
			Position position = new Position( 29.7507, -95.362 );
			MapSpan mapSpan = new MapSpan( position, 0.01, 0.01 );

			// center the map
			this.myMap.MoveToRegion( mapSpan );
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			// if there are no items - fetch
			if ( this._viewModel.Items.Count == 0 )
				this._viewModel.LoadItemsCommand.Execute( null );
		}
	}
}