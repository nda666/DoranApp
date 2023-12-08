using System.Net.Http;

namespace DoranApp.Utils;

public partial class Client
{
    public Client()
    {
        var httpClient = new HttpClient();
        if (!string.IsNullOrEmpty(Properties.Settings.Default.AuthToken))
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", Properties.Settings.Default.AuthToken);
        }

        var url = "http://" + Properties.Settings.Default.BASE_API_URL + ":44376";
        BaseUrl = url;
        _httpClient = httpClient;
        _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings, true);
    }
}