using System;
using System.Linq;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GoodGas.ViewModels
{
	/// <summary></summary>
	public class DebugViewModel : BaseViewModel
	{
		private string _data = String.Empty;

		public DebugViewModel()
		{
			this.Title = "Debug";

			//// subscribe to log messages
   //         MainModel.Instance.Log += UpdateLog;

			//ClearCommand = new Command( () => { this.DebugInfo = String.Empty; } );

			//RefreshCommand = new Command( () => {
			//	// load some data;
			//} );
		}

		/// <summary></summary>
		public ICommand ClearCommand { get; }

		/// <summary></summary>
		public ICommand RefreshCommand { get; }

		/// <summary></summary>
		public string DebugInfo
		{
			get { return _data; }
			set { SetProperty( ref _data, value ); }
		}

        /// <summary>Handler when main model is updated</summary>
		public void UpdateLog(string msg)
        {
            this.DebugInfo += String.Format("{0} ({1}){2}", msg, DateTime.Now, Environment.NewLine);
        }

	}
}

