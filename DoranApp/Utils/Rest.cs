using DalSoft.RestClient;
using DoranApp.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace DoranApp.Utils
{
    internal class Rest
    {
        public void Main()
        {
            HttpResponseMessage = new Object();
            Response = new Object();
        }

        public RestClient Client { get; private set; }

        public dynamic Response { get; private set; }
        public dynamic HttpResponseMessage { get; private set; }

        private IQuery Resource { get; set; }

        public Rest(string uri)
        {
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
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                }
            ));
           
            Resource = Client.Resource(uri);
        }

        private string FindError(HttpResponseMessage httpResponseMessage, dynamic response)
        {
            var status = (Int32)httpResponseMessage.StatusCode;
            var error = httpResponseMessage.ReasonPhrase;
            if (!status.ToString().StartsWith("2"))
            {
                JObject dynamicErrors = response;
                var reparsed = JsonConvert.SerializeObject(dynamicErrors);
                dynamic d = JsonConvert.DeserializeObject<dynamic>(reparsed);
                if (d == null)
                {
                    throw new RestException(status, error);
                }

              
                TypeInfo type = d.GetType();

                switch (status)
                {
                    case 400:
                        var errors = d.errors;
                        if (errors != null)
                        {
                            foreach (dynamic memberError in errors)
                            {
                                foreach (dynamic errorLabel in memberError)
                                {
                                    foreach (dynamic message in errorLabel)
                                    {
                                        error = $"{error + message}\n";
                                    }
                                }
                            }
                        }
                        throw new RestException(status, error);
                    default:
                        if (d?.message != null)
                        {
                            throw new RestException(status, d.message);
                        }
                        throw new RestException(status, error);
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

        public async Task<TReturn<T>> Get<T>(dynamic query)
        {
            if (query != null)
            {
                Resource.Query(query);
            }
            return await Get<T>();
        }

        public async Task<TReturn<T>> Get<T>()
        {
            T response = await Resource.Get();
            return Return<T>(response);
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
            FindError(response.HttpResponseMessage, response);
            
            tReturn.HttpResponseMessage = response.HttpResponseMessage;
            tReturn.Response = response;
             Client.Dispose();
            return tReturn;
        }
    }

    internal class TReturn
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