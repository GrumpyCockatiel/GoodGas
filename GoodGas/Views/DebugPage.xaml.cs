using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GoodGas.Models;
using GoodGas.ViewModels;

namespace GoodGas.Views
{
	/// <summary></summary>
	[DesignTimeVisible( false )]
	public partial class DebugPage : ContentPage
	{
		private DebugViewModel _viewModel;

		public DebugPage()
		{
			InitializeComponent();

			this.BindingContext = this._viewModel = new DebugViewModel();
		}
	}
}
