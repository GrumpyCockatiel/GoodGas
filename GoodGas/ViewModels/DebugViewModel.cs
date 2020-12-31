using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GoodGas.Logging;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GoodGas.ViewModels
{
	/// <summary>View Model for the Debugging Log Page</summary>
	public class DebugViewModel : INotifyPropertyChanged
	{
		#region [ Fields ]

		private string _items;

		private bool _isBusy = false;

		private string _title = string.Empty;

		protected ListLogger Logger => DependencyService.Get<ILogger>() as ListLogger;

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
				this.DebugInfo = String.Join( Environment.NewLine, this.Logger.Logs.ToArray() );
			} );
		}

		#region [ Properties ]

		/// <summary>Clears out the logs</summary>
		public ICommand ClearCommand { get; }

		/// <summary>Explicit refresh doesnt do anything right now</summary>
		public ICommand RefreshCommand { get; }

		/// <summary>Command to load data into the view model</summary>
		public Command LoadItemsCommand { get; set; }

		/// <summary>Is the page busy - then show loading icon</summary>
		public bool IsBusy
		{
			get { return _isBusy; }
			set { SetProperty( ref _isBusy, value ); }
		}

		/// <summary>View title value</summary>
		public string Title
		{
			get { return _title; }
			set { SetProperty( ref _title, value ); }
		}

		/// <summary>Property the Debug view is bound to</summary>
		public string DebugInfo
		{
			get { return _items; }
			set { SetProperty( ref _items, value ); }
		}

		#endregion [ Properties ]

		#region [ INotifyPropertyChanged ]

		/// <summary>Property change event</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Common code when setting a property value</summary>
		protected bool SetProperty<T>( ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null )
		{
			// are the old and new values the same
			if ( EqualityComparer<T>.Default.Equals( backingStore, value ) )
				return false;

			// update the value
			backingStore = value;

			// some callback after the update occurs
			onChanged?.Invoke();

			// let everyone know
			this.OnPropertyChanged( propertyName );

			// all done
			return true;
		}

		/// <summary>To call listeners to a property</summary>
		protected void OnPropertyChanged( [CallerMemberName] string propertyName = "" )
		{
			if ( this.PropertyChanged == null || String.IsNullOrWhiteSpace(propertyName) )
				return;

			// notify anyone listening for the specified property to change
			this.PropertyChanged.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
		}

		#endregion [ INotifyPropertyChanged ]
	}
}

