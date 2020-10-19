using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoviesRentalSimulationProject.Connector
{
    public class Customer
    {
        public HttpClient MoviesPresnet()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55836");
            return client;
        }
        public HttpClient RentingInfo()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51939");
            return client;
        }
        public HttpClient AuthenticationInfo()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51110");
            return client;

        }
    }
}
