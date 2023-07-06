using DalSoft.RestClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoranApp.Utils
{
    class Auth
    {

        private void findError(dynamic response)
        {

            try
            {
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Success");
            }
            catch (HttpRequestException exception)
            {
                throw new Exception("Koneksi ke server Error");
            }

        }

        public async Task<dynamic> getAsync(string actionName)
        {
            var client = new RestClient(actionName, new Config().SetJsonSerializerSettings(
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                }
            ));

            var response = await client.Get();
            var httpResponseMessage = response.HttpResponseMessage;

            findError(httpResponseMessage);

            return response;
        }
    }
}
