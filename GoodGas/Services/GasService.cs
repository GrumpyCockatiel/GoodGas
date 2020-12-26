using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GoodGas.Models;

namespace GoodGas.Services
{
    /// <summary>The Gas Station Service API client class</summary>
    public class GasService : RESTServiceBase, IDataStore<GasStation>
    {
        #region [ Fields ]

        /// <summary>The API base address</summary>
        public static string APIBaseURL = "https://goodgas-dev-v1-api.azurewebsites.net/api";

        /// <summary>This key can only be used to List the Gas Stations</summary>
        public static string FunctionKey = "0DW8yzcddAJPydMUWMd1TMmOSSSmzppK6xcWj26bqeRWrh0D1IgSwQ==";

        #endregion [ Fields ]

        #region [ Constructor ]

        /// <summary>Constructor</summary>
        /// <param name="env"></param>
        public GasService() : base( APIBaseURL )
        {
            // TODO - need to change this
            base.APIKey = FunctionKey;
        }

        #endregion [ Constructor ]

        #region [ Properties ]

        #endregion [ Properties ]

        #region [ IDataStore ]

        /// <summary>Get the gas stations list as an async call</summary>
        /// <returns></returns>
        public async Task<bool> ListGasStations( APICallback<List<GasStation>> callback )
        {
            HttpRequestMessage message = this.GetRequest( "ListGasStations", false, false );

            ServiceResponse<List<GasStation>> resp = new ServiceResponse<List<GasStation>>();

            HttpResponseMessage results = await this.Client.SendAsync( message );
            resp.StatusCode = results.StatusCode;
            string body = await results.Content.ReadAsStringAsync();
            resp.Data = JsonConvert.DeserializeObject<APIResult<List<GasStation>>>( body );
            callback( resp );

            return true;
        }

        /// <summary></summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        //public async Task<IEnumerable<GasStation>> GetItemsAsync( bool forceRefresh = false )
        //{
        //    HttpRequestMessage message = this.GetRequest( "ListGasStations", false, false );

        //    ServiceResponse<List<GasStation>> resp = new ServiceResponse<List<GasStation>>();

        //    HttpResponseMessage results = await this.Client.SendAsync( message );
        //    resp.StatusCode = results.StatusCode;
        //    string body = await results.Content.ReadAsStringAsync();
        //    resp.Data = JsonConvert.DeserializeObject<APIResult<List<GasStation>>>( body );

        //    return resp.Data.ResultObject;
        //}

        #endregion [ IDataStore ]

        #region [ API Methods ]

        /// <summary>Get the gas stations list as an async call</summary>
        /// <returns></returns>
        //public Task ListGasStations(APICallback<List<GasStation>> callback)
        //{
        //    HttpRequestMessage message = this.GetRequest( "ListGasStations", false, false);

        //    ServiceResponse<List<GasStation>> resp = new ServiceResponse<List<GasStation>>();

        //    Task<HttpResponseMessage> task1 = this.Client.SendAsync( message );

        //    Task task2 = task1.ContinueWith( ( ant ) => {
        //        resp.StatusCode = ant.Result.StatusCode;
        //        string body = ant.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //        resp.Data = JsonConvert.DeserializeObject<APIResult<List<GasStation>>>( body );
        //        callback(resp);
        //    } );

        //    return task1;
        //}



        #endregion [ API Methods ]

    }
}
