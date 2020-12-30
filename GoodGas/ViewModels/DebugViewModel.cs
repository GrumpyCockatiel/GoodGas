using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GoodGas.ViewModels
{
	/// <summary></summary>
	public class DebugViewModel : BaseViewModel
	{
		#region [ Fields ]

		private string _items;

		#endregion [ Fields ]

		public DebugViewModel()
		{
			this.Title = "Debug";

			ClearCommand = new Command( () => { this.DebugInfo = String.Empty; } );

			this.DebugInfo = String.Empty;

			// set a handler for load items
			this.LoadItemsCommand = new Command( () =>
			{
				// refresh the data
				//Task<bool> t = this.DataStore.ListGasStations( this.LoadModel );
				Task<IEnumerable<string>> t = this.Logger.ListAll();
				t.ContinueWith( ant => { LoadModel( ant.Result.ToList() ); } );
			} );
		}

		/// <summary></summary>
		public ICommand ClearCommand { get; }

		/// <summary></summary>
		public ICommand RefreshCommand { get; }

		/// <summary>Command to load data into the view model</summary>
		public Command LoadItemsCommand { get; set; }

		/// <summary>Property the Debug view is bound to</summary>
		public string DebugInfo
		{
			get { return _items; }
			set { SetProperty( ref _items, value ); }
		}

		/// <summary></summary>
		/// <param name="results"></param>
		public void LoadModel( List<string> results )
		{
			if ( IsBusy )
				return;

			IsBusy = true;

			try
			{
				this.DebugInfo = String.Join( Environment.NewLine, results.ToArray() ); ;
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

