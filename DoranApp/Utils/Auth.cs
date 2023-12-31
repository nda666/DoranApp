﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using DalSoft.RestClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DoranApp.Utils
{
    internal class Auth
    {
        private void findError(dynamic response)
        {
            var x = new Client();
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