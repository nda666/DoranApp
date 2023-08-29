using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DoranApp.Utils
{
    internal class RestV2
    {
        private RestClient Client { get; set; }

        public RestV2()
        {
            Client = new RestClient(DoranApp.Properties.Settings.Default.BASE_API_URL);

            if (!string.IsNullOrEmpty(Properties.Settings.Default.AuthToken))
            {
                Client.AddDefaultHeader("Authorization", Properties.Settings.Default.AuthToken);
            }
        }

        private string FindError(RestResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                dynamic dynamicErrors = JsonConvert.DeserializeObject(response.Content);

                if (dynamicErrors?.errors != null)
                {
                    string error = "";
                    foreach (var memberError in dynamicErrors.errors)
                    {
                        foreach (var errorLabel in memberError)
                        {
                            foreach (var message in errorLabel)
                            {
                                error += $"{message}\n";
                            }
                        }
                    }
                    return error;
                }

                if (!string.IsNullOrEmpty(dynamicErrors?.message))
                {
                    return dynamicErrors.message;
                }

                return response.StatusDescription;
            }

            return null;
        }

        protected RestRequest GetRequest(string resource, dynamic query = null)
        {
            var request = new RestRequest(resource, Method.Get);

            if (query != null)
            {
                // Assuming query is a dictionary or an object
                foreach (var property in query.GetType().GetProperties())
                {
                    request.AddQueryParameter((string)property.Name, (string)property.GetValue(query));
                }
            }
            return request;
        }

        public async Task<dynamic> GetJson(string resource, dynamic query = null)
        {
            var response = await Get(resource, query);

            var responseObject = JsonConvert.DeserializeObject(response.Content);
            ConsoleDump.Extensions.Dump(response.Content, "aa");
            return responseObject;
        }

        public async Task<T> GetJson<T>(string resource, dynamic query = null)
        {
            var response = await Get(resource, query);

            var responseObject = JsonConvert.DeserializeObject<T>(response.Content);
            return responseObject;
        }

        public async Task<RestResponse> Get(string resource, dynamic query = null)
        {
            var request = GetRequest(resource, query);
            var response = await Client.ExecuteAsync(request);
            var error = FindError(response);
            if (error != null)
            {
                throw new Exception(error);
            }
            return response;
        }

        public async Task<IRestResponse<T>> Get<T>(string resource, dynamic query = null)
        {
            var response = await Get(resource, query);
            var error = FindError(response);
            if (error != null)
            {
                throw new Exception(error);
            }

            var responseObject = JsonConvert.DeserializeObject<T>(response.Content);

            return new IRestResponse<T>
            {
                StatusCode = response.StatusCode,
                Content = response.Content,
                Data = responseObject
            };
        }
    }

    internal class IRestResponse<T> : RestResponse
    {
        public T Data { get; set; }
    }
}