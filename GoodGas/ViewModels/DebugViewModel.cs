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
	/// <summary></summary>
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

		/// <summary></summary>
		public ICommand ClearCommand { get; }

		/// <summary></summary>
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

		/// <summary>Setting a property causes it to invoke a change event</summary>
		protected bool SetProperty<T>( ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null )
		{
			if ( EqualityComparer<T>.Default.Equals( backingStore, value ) )
				return false;

			backingStore = value;
			onChanged?.Invoke();
			this.OnPropertyChanged( propertyName );
			return true;
		}

		#region [ INotifyPropertyChanged ]

		/// <summary></summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Any property invokes a change event</summary>
		protected void OnPropertyChanged( [CallerMemberName] string propertyName = "" )
		{
			var changed = PropertyChanged;

			if ( changed == null )
				return;

			changed.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
		}

		#endregion [ INotifyPropertyChanged ]
	}
}

