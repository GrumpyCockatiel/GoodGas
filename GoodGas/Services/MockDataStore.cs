using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodGas.Models;

namespace GoodGas.Services
{
    /// <summary>A local mock store</summary>
    public class MockDataStore : IDataStore<GasStation>
    {
        readonly List<GasStation> items;

        /// <summary>Constructor</summary>
        public MockDataStore()
        {
            this.items = new List<GasStation>()
            {
                // load some fake data
                new GasStation { ID = "1", Vendor = "Valero", Address = "926 Westheimer Rd", City="Houston",State="TX", Zip="77006", Latitude=29.7408427, Longitude=-95.3975131 },
                new GasStation { ID = "2", Vendor = "Chevron", Address = "2602 Richmond Ave", City="Houston", State="TX", Zip="77098", Latitude = 29.7350346, Longitude = -95.4190183 },
            };
        }

        /// <summary></summary>
        /// <returns></returns>
        public async Task<IEnumerable<GasStation>> ListAll()
        {
            return await Task.FromResult( this.items );
        }

        /// <summary></summary>
        /// <returns></returns>
        public async Task<bool> AddItem( GasStation items )
        {
            return await Task.FromResult( false );
        }
    }

}