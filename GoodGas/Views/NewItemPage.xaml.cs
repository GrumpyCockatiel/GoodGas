using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GoodGas.Models;

namespace GoodGas.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible( false )]
	public partial class NewItemPage : ContentPage
	{
		public GasStation Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();

			Item = new GasStation
			{
				Vendor = "Item name",
				Address = "This is an item description."
			};

			BindingContext = this;
		}

		async void Save_Clicked( object sender, EventArgs e )
		{
			MessagingCenter.Send( this, "AddItem", Item );
			await Navigation.PopModalAsync();
		}

		async void Cancel_Clicked( object sender, EventArgs e )
		{
			await Navigation.PopModalAsync();
		}
	}
}