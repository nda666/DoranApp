using DalSoft.RestClient;
using DoranApp.Exceptions;
using DoranApp.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DoranApp.Utils
{

    class Rest
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
                    ContractResolver = new CustomDateContractResolver("yyyy-MM-dd hh:mm:ss"),


                    //ContractResolver = new DefaultContractResolver
                    //{
                    //  NamingStrategy = new SnakeCaseNamingStrategy()
                    //}
                }
            ));

            Resource = Client.Resource(uri);
        }


        private string FindError(dynamic httpResponseMessage, dynamic response)
        {
            var status = (Int32)httpResponseMessage.StatusCode;
            if (!status.ToString().StartsWith("2"))
            {
                var xx = (string)response.ToString();
                if (xx.Count() <= 0)
                {
                    throw new Exception(status.ToString() + " " + httpResponseMessage?.ReasonPhrase);
                }
                var error = "";
                JObject dynamicErrors = response;


                var reparsed = JsonConvert.SerializeObject(dynamicErrors);
                dynamic d = JsonConvert.DeserializeObject<dynamic>(reparsed);

                TypeInfo type = d.GetType();
                var errors = d.errors;
                foreach (dynamic memberError in errors)
                {
                    foreach (dynamic errorLabel in memberError)
                    {
                        Console.WriteLine(memberError);
                        foreach (dynamic message in errorLabel)
                        {
                            error = $"{error + message}\n";
                        }
                    }

                }

                throw new Exception(error);
                switch (status)
                {
                    case 422:
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
                        throw new RestException(error, status, (string)d.error);
                    default:
                        throw new RestException((string)d.message, status, (string)d.error);

                }
            }

            return "";
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
            var error = FindError(response.HttpResponseMessage, response);
            if (error.Length > 0)
            {
                throw new Exception(error);
            }
            tReturn.HttpResponseMessage = response.HttpResponseMessage;
            tReturn.Response = response;
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
}
