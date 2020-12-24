using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GoodGas.Services;
using GoodGas.Views;

namespace GoodGas
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			DependencyService.Register<GasService>();
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
