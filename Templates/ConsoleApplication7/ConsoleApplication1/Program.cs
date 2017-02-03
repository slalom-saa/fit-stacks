using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxMind.GeoIP2;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new WebServiceClient(119543, "1Ksa6iuvfOJu"))
            {
                // Do the lookup
                var response = client.CityAsync("71.197.137.82").Result;

                Console.WriteLine(response.Location);
            }
        }
    }
}
