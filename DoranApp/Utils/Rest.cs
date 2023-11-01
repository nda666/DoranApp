using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DalSoft.RestClient;
using DoranApp.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DoranApp.Utils
{
    [Obsolete("Lebih baik jangan gunakan ini.Pakai \"DoranApp.Utils.Client\" hasil dari generate an NSwag")]
    internal class Rest
    {
        public Rest(string uri)
        {
            aaa = uri;
            Headers header = new Headers();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.AuthToken))
            {
                header.Add("Authorization", Properties.Settings.Default.AuthToken);
            }

            Client = new RestClient(DoranApp.Properties.Settings.Default.BASE_API_URL,
                header
                , new Config().SetJsonSerializerSettings(
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateParseHandling = DateParseHandling.DateTime,
                        DateFormatString = "yyyy-MM-dd HH:mm:ss",
                        ContractResolver = new DefaultContractResolver
                            { NamingStrategy = new CamelCaseNamingStrategy() },
                    }
                ));

            Resource = Client.Resource(uri);
        }

        public string aaa { get; set; } = "";
        public RestClient Client { get; private set; }

        public dynamic Response { get; private set; }
        public dynamic HttpResponseMessage { get; private set; }

        private IQuery Resource { get; set; }

        public void Main()
        {
            HttpResponseMessage = new Object();
            Response = new Object();
        }

        private string FindError(HttpResponseMessage httpResponseMessage, dynamic response)
        {
            var status = (Int32)httpResponseMessage.StatusCode;
            var error = httpResponseMessage.ReasonPhrase;
            if (!status.ToString().StartsWith("2"))
            {
                JObject dynamicErrors = response;

                if (dynamicErrors == null)
                {
                    throw new RestException(status, error);
                }

                switch (status)
                {
                    case 422:
                        var errors = response.errors;
                        List<string> errorTexts = new List<string>();
                        // errorTexts.Add($"{error}:");
                        if (errors != null)
                        {
                            foreach (dynamic memberError in errors)
                            {
                                errorTexts.Add((string)memberError);
                            }
                        }

                        throw new RestException(status, String.Join("\n", errorTexts));
                    default:

                        if (response?.message != null)
                        {
                            throw new RestException(status, response.message, response.errorType, response?.data);
                        }

                        if (response?.title != null)
                        {
                            throw new RestException(status, response.title, response.errorType, response?.data);
                        }

                        throw new RestException(status, error, response?.errorType, response?.data);
                }
            }

            return null;
        }

        public async Task<TReturn> Get(dynamic query)
        {
            if (query != null)
            {
                Resource.Query(query);
            }

            return await Get();
        }

        public async Task<TReturn> Get()
        {
            var response = await Resource.Get();
            return Return(response);
        }

        public async Task<TReturn<T>> Get<T>(dynamic? query)
        {
            if (query != null)
            {
                Resource.Query(query);
            }

            try
            {
                dynamic response = await Resource.Get();
                return Return<T>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TReturn> Post(dynamic postData)
        {
            var response = await Resource.Post(postData);
            return Return(response);
        }

        public async Task<TReturn> Patch(dynamic postData)
        {
            var response = await Resource.Patch(postData);
            return Return(response);
        }

        public async Task<TReturn> Put(dynamic postData)
        {
            var response = await Resource.Put(postData);
            return Return(response);
        }

        public async Task<TReturn> Delete()
        {
            var response = await Resource.Delete();
            return Return(response);
        }

        public async Task<TReturn<T>> Delete<T>(dynamic? data)
        {
            try
            {
                dynamic response = await Resource.Delete(data);
                return Return<T>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private TReturn Return(dynamic response)
        {
            TReturn tReturn = new TReturn();
            FindError(response.HttpResponseMessage, response);
            tReturn.HttpResponseMessage = response.HttpResponseMessage;
            tReturn.Response = response;
            Client.Dispose();
            return tReturn;
        }

        private TReturn<T> Return<T>(dynamic response)
        {
            TReturn<T> tReturn = new TReturn<T>();
            tReturn.HttpResponseMessage = response.HttpResponseMessage;
            tReturn.Response = response;
            Client.Dispose();
            return tReturn;
        }
    }

    public class TReturn
    {
        public dynamic HttpResponseMessage { get; set; }
        public dynamic Response { get; set; }
        public string ErrorMessage { get; set; }

        public void Main()
        {
            ErrorMessage = null;
            HttpResponseMessage = new Object();
            Response = new Object();
        }
    }

    internal class TReturn<T>
    {
        public dynamic HttpResponseMessage { get; set; }
        public T Response { get; set; }
        public string ErrorMessage { get; set; }

        public void Main()
        {
            ErrorMessage = null;
            HttpResponseMessage = new Object();
        }
    }
}