using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankOfDotNet.ConsoleClient
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "Secret");
            var tokenResponce = await tokenClient.RequestClientCredentialsAsync("bankOfDotnetApi");
            if (tokenResponce.IsError)
            {
                Console.WriteLine(tokenResponce.Error);
                return;
            }
            Console.WriteLine(tokenResponce.Json);
            Console.WriteLine("\n\n");

            ////Consume our Customer Api
            //var client = new HttpClient();
            //client.SetBearerToken(tokenResponce.AccessToken);

            //var customerInfo = new StringContent(
            //    JsonConvert.SerializeObject(
            //        new { Id = 10, FirstName = "Darko", LastName = "Gelevski" }),
            //        Encoding.UTF8, "application/json");

            //var createCustomerResponse = await client.PostAsync("http://localhost:28978/api/customers/", customerInfo);
            //if (createCustomerResponse.IsSuccessStatusCode)
            //    Console.WriteLine(createCustomerResponse.StatusCode);

            //var getCustomerResponse = await client.GetAsync("http://localhost:28978/api/customers");
            //if (!getCustomerResponse.IsSuccessStatusCode)
            //    Console.WriteLine(getCustomerResponse.StatusCode);
            //else
            //{
            //    var content = getCustomerResponse.Content.ReadAsStringAsync();
            //    Console.WriteLine(JArray.Parse(content.ToString()));
            //}

            Console.Read();


        }
    }
}
