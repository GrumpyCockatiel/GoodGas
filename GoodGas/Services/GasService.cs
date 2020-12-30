using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GoodGas.Models;
using System.Net;

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

        /// <summary>Calls the List Gas Stations API endpoint</summary>
        /// <returns></returns>
        public async Task<IEnumerable<GasStation>> ListAll()
        {
            // define the request
            HttpRequestMessage message = this.GetRequest( "ListGasStations", false, false );

            // actually call the API
            HttpResponseMessage results = await this.Client.SendAsync( message );
            HttpStatusCode code = results.StatusCode;
            // lets check the HTTP reponse
            string body = await results.Content.ReadAsStringAsync();

            // deserialize the response
            APIResult<List<GasStation>> resp = JsonConvert.DeserializeObject<APIResult<List<GasStation>>>( body );

            // now check the API call results

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
