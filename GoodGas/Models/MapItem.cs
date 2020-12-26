using System;
using System.ComponentModel;
using Xamarin.Forms.Maps;

namespace GoodGas.Models
{
	/// <summary>Map entity</summary>
	public class MapItem : INotifyPropertyChanged
	{
		private Position _position;

		public MapItem( Position position, string address, string description = "" )
		{
			Address = address;
			Description = description;
			Position = position;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public string Address { get; }

		public string Description { get; }

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
