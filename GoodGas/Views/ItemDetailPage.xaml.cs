using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GoodGas.Models;
using GoodGas.ViewModels;

namespace GoodGas.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible( false )]
	public partial class ItemDetailPage : ContentPage
	{
		private ItemDetailViewModel _viewModel;

		public ItemDetailPage( ItemDetailViewModel viewModel )
		{
			InitializeComponent();

			this.BindingContext = this._viewModel = viewModel;
		}

		public ItemDetailPage()
		{
			InitializeComponent();

			var item = new GasStation
			{
				Vendor = "Item 1",
				Address = "This is an item description."
			};

			this._viewModel = new ItemDetailViewModel( item );
			BindingContext = this._viewModel;
		}
	}
}