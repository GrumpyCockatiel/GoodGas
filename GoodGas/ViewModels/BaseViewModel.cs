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
		protected IDataStore<GasStation> DataStore => DependencyService.Get<IDataStore<GasStation>>();

		private bool _isBusy = false;

        private string _title = string.Empty;

        #endregion [ Fields ]

        #region [ Properties ]

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

        #endregion [ Properties ]

        /// <summary>Setting a property causes it to invoke a change event</summary>
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

        #region [ INotifyPropertyChanged ]

        /// <summary>This is what a Binding Context is looking for to subscribe to</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>To call listeners to a property</summary>
        protected void OnPropertyChanged( [CallerMemberName] string propertyName = "" )
        {
            if ( this.PropertyChanged == null || String.IsNullOrWhiteSpace( propertyName ) )
                return;

            // notify anyone listening for the specified property to change
            this.PropertyChanged.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        #endregion [ INotifyPropertyChanged ]
    }
}
