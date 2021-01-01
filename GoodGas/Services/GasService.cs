using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GoodGas.Models;
using System.Net;
using Xamarin.Forms;
using GoodGas.Logging;

namespace GoodGas.Services
{
    /// <summary>The Gas Station Service API client class</summary>
    public class GasService : RESTServiceBase, IDataStore<GasStation>
    {
        #region [ Fields ]

        // the logger
        private ILogger _logger = new NullLogger();

        #endregion [ Fields ]

        #region [ Constructor ]

        /// <summary>Constructor</summary>
        /// <param name="env"></param>
        public GasService( string url, string key ) : base( url, key )
        {
        }

        #endregion [ Constructor ]

        #region [ Properties ]

        /// <summary>Gets or set the logger</summary>
        public ILogger Logger
        {
            //get => DependencyService.Get<ILogger>();
            get => this._logger ?? new NullLogger();

            set => this._logger = value;
        }

        #endregion [ Properties ]

        #region [ IDataStore ]

        /// <summary>Later maybe to add an items to the user's own local data but right now does nothing</summary>
        /// <returns>Always false for now to satisify the interface</returns>
        public async Task<bool> AddItem( GasStation item )
        {
            return await Task.FromResult( false );
        }

        /// <summary>Calls the List Gas Stations API endpoint</summary>
        /// <returns></returns>
        public async Task<IEnumerable<GasStation>> ListAll()
        {
            // roll the request
            HttpRequestMessage message = this.GetRequest( "ListGasStations", false, false );

            // log something but lets make this easier
            this.Logger.Log( "Called the ListAll API method.", "API" );

            // actually call the API
            HttpResponseMessage results = await this.Client.SendAsync( message );

            // lets check the HTTP reponse
            HttpStatusCode code = results.StatusCode;

            // deserialize the response
            string body = await results.Content.ReadAsStringAsync();
            APIResult<List<GasStation>> resp = JsonConvert.DeserializeObject<APIResult<List<GasStation>>>( body );

            // and return the data to the caller
            if ( resp.IsSuccess && resp.ResultObject != null )
            {
                this.Logger.Log( $"ListAll returned {resp.ResultObject.Count} stations.", "API" );
                return resp.ResultObject;
            }

            return new List<GasStation>();
        }

        /// <summary>Get the gas stations list as an async call</summary>
        /// <remarks>This is a callback version for comparison</remarks>
        //public async Task<bool> ListGasStations( APICallback<List<GasStation>> callback )
        //{
        //    // define the request
        //    HttpRequestMessage message = this.GetRequest( "ListGasStations", false, false );

        //    ServiceResponse<List<GasStation>> resp = new ServiceResponse<List<GasStation>>();

        //    // actually call the API
        //    HttpResponseMessage results = await this.Client.SendAsync( message );
        //    resp.StatusCode = results.StatusCode;
        //    string body = await results.Content.ReadAsStringAsync();

        //    resp.Data = JsonConvert.DeserializeObject<APIResult<List<GasStation>>>( body );
        //    callback( resp );

        //    return true;
        //}

        #endregion [ IDataStore ]

    }
}
