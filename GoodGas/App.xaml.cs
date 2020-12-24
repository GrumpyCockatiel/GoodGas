using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GoodGas.Services;
using GoodGas.Views;

namespace GoodGas
{
	/// <summary></summary>
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			// register the back-end service
			DependencyService.Register<GasService>();

			// create the main page
			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
