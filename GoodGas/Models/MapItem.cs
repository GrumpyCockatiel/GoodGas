using System;
using System.ComponentModel;
using Xamarin.Forms.Maps;

namespace GoodGas.Models
{
	/// <summary>Map entity</summary>
	public class MapItem : INotifyPropertyChanged
	{
		/// <summary></summary>
		private Position _position;

		/// <summary></summary>
		public MapItem( Position position, string address, string description = "" )
		{
			Address = address;
			Description = description;
			Position = position;
		}

		/// <summary></summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary></summary>
		public string Address { get; }

		/// <summary></summary>
		public string Description { get; }

		/// <summary></summary>
		public Position Position
		{
			get => _position;
			set
			{
				if ( !_position.Equals( value ) )
				{
					_position = value;
					PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( Position ) ) );
				}
			}
		}
	}

}
