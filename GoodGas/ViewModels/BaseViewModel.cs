using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using GoodGas.Models;
using GoodGas.Services;

namespace GoodGas.ViewModels
{
	/// <summary>Base view model class</summary>
	public class BaseViewModel : INotifyPropertyChanged
	{
		#region [ Fields ]

		/// <summary>The data store itself</summary>
		/// <remarks>Looks for a registered Type of IDataStore-GasStation which was reigstered in App.cs</remarks>
		//protected IDataStore<GasStation> DataStore => DependencyService.Get<IDataStore<GasStation>>();
		protected IDataStore<GasStation> DataStore => DependencyService.Get<GasService>();

		private bool _isBusy = false;

        private string _title = string.Empty;

        #endregion [ Fields ]

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

		/// <summary>Setting a property causes it to invoke a change event</summary>
		protected bool SetProperty<T>( ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null )
		{
			if ( EqualityComparer<T>.Default.Equals( backingStore, value ) )
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged( propertyName );
			return true;
		}

		#region INotifyPropertyChanged

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

		#endregion
	}
}
