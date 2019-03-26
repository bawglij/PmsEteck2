using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;


namespace Brunata.Services
{
    class AuthenticationManager
    {
        public AuthenticationManager(string clientId, string secret)
        {
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            Secret = secret ?? throw new ArgumentNullException(nameof(secret));
            Authenticate();
        }

        public string AccessToken { get; private set; }
        public DateTime? Expires { get; private set; }
        string ClientId;
        string Secret;

        private void Authenticate()
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["BaseUri"]);
            RestRequest restRequest = new RestRequest("hub/access/token", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("Id", ConfigurationManager.AppSettings["ClientId"]);
            restRequest.AddParameter("Secret", ConfigurationManager.AppSettings["Secret"]);
            IRestResponse restResponse = client.Execute(restRequest);
            JsonDeserializer deserializer = new JsonDeserializer();
            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Dictionary<string, object> deserializedObject = deserializer.Deserialize<Dictionary<string, object>>(restResponse);
                AccessToken = deserializedObject.First(f => f.Key.Equals("accessToken")).Value.ToString();
                DateTime date;
                if(DateTime.TryParse(deserializedObject.First(f => f.Key.Equals("expiresAt")).Value.ToString(), out date))
                    Expires = date;
            }

        }

        public void RenewAuthentication()
        {
            Authenticate();
        }
    }
}
