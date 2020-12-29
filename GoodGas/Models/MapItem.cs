using System;
using System.ComponentModel;
using Xamarin.Forms.Maps;

namespace GoodGas.Models
{
	/// <summary>A Map entity</summary>
	public class MapItem : INotifyPropertyChanged
	{
		/// <summary></summary>
		private Position _position;

		/// <summary>Constructor</summary>
		public MapItem( Position position, string address, string description = "" )
		{
			Address = address;
			Description = description;
			Position = position;
		}

		#region [ INotifyPropertyChanged ]

		/// <summary>Things listening to see when this object has a change to a property</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion [ INotifyPropertyChanged ]

		/// <summary></summary>
		public string Address { get; }

		/// <summary></summary>
		public string Description { get; }

		/// <summary>Lat/long geo position</summary>
		/// <remarks>Only position invokes a change</remarks>
		public Position Position
		{
			get => this._position;
			set
			{
				// did the actual position change
				if ( !this._position.Equals( value ) )
				{
					this._position = value;

					// if property changed is not null, then call the event
					PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( Position ) ) );
				}
			}
		}
	}

}
