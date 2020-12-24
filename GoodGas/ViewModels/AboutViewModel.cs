using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodGas.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public AboutViewModel()
		{
			Title = "About";

			OpenWebCommand = new Command( () => Device.OpenUri( new Uri( "https://www.raydreams.com" ) ) );
		}

		public ICommand OpenWebCommand { get; }
	}
}