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

        #endregion [ Fields ]

        #region [ Constructor ]

        /// <summary>Constructor</summary>
        /// <param name="env"></param>
        public GasService(string url, string key) : base( url, key )
        {
            _ = 0;
        }

        #endregion [ Constructor ]

        #region [ Properties ]

        #endregion [ Properties ]

        #region [ IDataStore ]

        /// <summary></summary>
        /// <returns></returns>
        public async Task<bool> AddItem( GasStation item )
        {
            return await Task.FromResult( false );
        }

        /// <summary>Calls the List Gas Stations API endpoint</summary>
        /// <returns></returns>
        public async Task<IEnumerable<GasStation>> ListAll()
        {
            // define the request
            HttpRequestMessage message = this.GetRequest( "ListGasStations", false, false );

            // actually call the API - we want to make it easier to call the logger
            var logger = DependencyService.Get<ILogger>();
            logger.Log( "Called the API", "API" );
            HttpResponseMessage results = await this.Client.SendAsync( message );

            // lets check the HTTP reponse
            HttpStatusCode code = results.StatusCode;

            string body = await results.Content.ReadAsStringAsync();

            // deserialize the response
            APIResult<List<GasStation>> resp = JsonConvert.DeserializeObject<APIResult<List<GasStation>>>( body );

            // now check the API call results
            logger.Log( "The API returned", "API" );

            // and return the data to the caller
            return resp.ResultObject;
        }

        /// <summary>Get the gas stations list as an async call</summary>
        /// <returns></returns>
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
