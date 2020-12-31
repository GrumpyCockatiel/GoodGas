using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GoodGas.Services;
using GoodGas.Views;
using GoodGas.Models;
using GoodGas.Logging;

namespace GoodGas
{
	/// <summary></summary>
	public partial class App : Application
	{
		#region [ Config ]

		/// <summary>The API base address</summary>
		public static string APIBaseURL = "https://goodgas-dev-v1-api.azurewebsites.net/api";

		/// <summary>This key can only be used to List the Gas Stations</summary>
		public static string FunctionKey = "0DW8yzcddAJPydMUWMd1TMmOSSSmzppK6xcWj26bqeRWrh0D1IgSwQ==";

		#endregion [ Config ]

		/// <summary></summary>
		public App()
		{
			InitializeComponent();

			// register a singleton logger but as 2 different types
			ListLogger logger = new ListLogger( "iOS", LogLevel.All );
			DependencyService.RegisterSingleton<ILogRepository>( logger );
			DependencyService.RegisterSingleton<ILogger>( logger );

			// register the back-end service as a singleton
			// we can register a mock here based on some startup setting
			//DependencyService.RegisterSingleton<IDataStore<GasStation>>( new MockDataStore() );
			DependencyService.RegisterSingleton<IDataStore<GasStation>>( new GasService(APIBaseURL, FunctionKey) );

			// create the main page
			MainPage = new MainPage();
		}

		/// <summary>When the app first starts</summary>
		protected override void OnStart()
		{
			// Handle when your app starts
		}

		/// <summary>When the app is pushed to the background</summary>
		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		/// <summary>When the app is brought to the foreground</summary>
		protected override void OnResume()
		{
			// lets rerefesh the data
		}
	}
}
