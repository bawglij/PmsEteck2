using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;

namespace PmsEteck.Data.Services
{
    public class Fudura
    {

        private string apiFuduraTokenEndPoint = "https://login.microsoftonline.com/9f84cace-b3ed-4c40-badc-838bf6e52b53/oauth2/token";
        private string apiFuduraEndPoint = "https://fdr-ws-prd.azurewebsites.net/api/v1";

        private string FuduraClientID = ConfigurationManager.AppSettings["FuduraClientID"];
        private string FuduraSecret = ConfigurationManager.AppSettings["FuduraSecret"];
        public string FuduraAccessToken;
        public DateTime FuduraExpiredDate;

        public string ChannelID;
        public string ProductType;

        public DateTime GetAuthorizedDateFrom(string EANCode)
        {
            // Get new acccesstoken when expires in 10 minutes
            if (DateTime.Now.AddMinutes(-10) > FuduraExpiredDate)
                GetAccessToken();
            DateTime authorizedDate = new DateTime();

            string urlConnections = string.Format("{0}/aansluitingen", apiFuduraEndPoint);

            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
            client.Headers.Add("Content-type", "application/json");

            var result = client.DownloadString(urlConnections);

            JArray objectResult = JArray.Parse(result);

            foreach (JToken connection in objectResult)
            {
                if (connection["Ean"].ToString() == EANCode)
                {
                    authorizedDate = DateTime.Parse(connection["AuthorizedFrom"].ToString());
                }
            }

            return authorizedDate;
        }

        public DateTime GetLastAvailableData(string EANCode)
        {
            // Get new acccesstoken when expires in 10 minutes
            if (DateTime.Now.AddMinutes(-10) > FuduraExpiredDate)
                GetAccessToken();
            DateTime lastAvailableDate = new DateTime();

            string urlChannels = string.Format("{0}/aansluitingen/{1}/channels", apiFuduraEndPoint, EANCode);
            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
            client.Headers.Add("Content-type", "application/json");

            var result = client.DownloadString(urlChannels);

            JArray objectResult = JArray.Parse(result);

            foreach (JToken channel in objectResult)
            {
                if (channel["ChannelType"].ToString().Contains("TimeSeries") && channel["Id"].ToString().Contains("Verbruik"))
                {
                    ChannelID = channel["Id"].ToString();
                    ProductType = channel["Metadata"]["ProductType"].ToString();
                    lastAvailableDate = DateTime.Parse(channel["LastReading"].ToString());
                }
            }

            return lastAvailableDate;

        }

        public string GetTimeSeries(string EANCode, DateTime date)
        {
            // Get new acccesstoken when expires in 10 minutes
            if (DateTime.Now.AddMinutes(-10) > FuduraExpiredDate)
                GetAccessToken();
            string urlChannels = string.Format("{0}/aansluitingen/{1}/channels/{2}/timeseries/{3}/{4}/{5}", apiFuduraEndPoint, EANCode, ChannelID, date.Year, date.Month, date.Day);
            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
            client.Headers.Add("Content-type", "application/json");

            string resultString = string.Empty;
            try
            {
                resultString = client.DownloadString(urlChannels);
            }
            catch(Exception e)
            {
                e.ToString();
            }
            return resultString;
        }

        public string GetDataPoints(string EANCode, string channel, DateTime date)
        {
            // Get new acccesstoken when expires in 10 minutes
            if (DateTime.Now.AddMinutes(-10) > FuduraExpiredDate)
                GetAccessToken();
            string urlChannels = string.Format("{0}/aansluitingen/{1}/channels/{2}/datapoints/{3}/{4}/{5}", apiFuduraEndPoint, EANCode, channel, date.Year, date.Month, date.Day);
            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
            client.Headers.Add("Content-type", "application/json");

            string resultString = string.Empty;
            try
            {
                resultString = client.DownloadString(urlChannels);
            }
            catch { }
            return resultString;
        }

        //public decimal? GetFuduraLastMonthConsumption(DateTime today, string EANCode, int UnitKey)
        //{
        //    decimal? monthConsumption = null;
        //    DateTime startMonth = today.AddMonths(-1);

        //    // Wat te doen bij elektriciteit
        //    if (ProductType == "E")
        //    {
        //        // Nagaan of het om hoog of laagverbruik gaat
        //        if (UnitKey == 3)
        //        {
        //            // Haal alle maandverbruiken op van de laatste 2 maanden
        //            string urlDataPoints = string.Format("{0}/aansluitingen/{1}/channels/{2}/datapoints/{3}/{4}/{5}", apiFuduraEndPoint, EANCode, "E65_EMVLAL", startMonth.Year, startMonth.Month, startMonth.Day);
        //            WebClient client = new WebClient { Encoding = Encoding.UTF8 };
        //            client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
        //            client.Headers.Add("Content-Type", "application/json");

        //            string resultString = client.DownloadString(urlDataPoints);

        //            JArray jsonArray = JArray.Parse(resultString);

        //            DateTime dateTime = DateTime.Parse(jsonArray.First["From"].ToString());

        //            if (dateTime.Month == startMonth.Month)
        //            {
        //                monthConsumption = decimal.Parse(jsonArray.First["ReadingL"].ToString());
        //            }

        //        }
        //        else if (UnitKey == 4)
        //        {
        //            // Haal alle maandverbruiken op van de laatste 2 maanden
        //            string urlDataPoints = string.Format("{0}/aansluitingen/{1}/channels/{2}/datapoints/{3}/{4}/{5}", apiFuduraEndPoint, EANCode, "E65_EMVLAN", startMonth.Year, startMonth.Month, startMonth.Day);
        //            WebClient client = new WebClient { Encoding = Encoding.UTF8 };
        //            client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
        //            client.Headers.Add("Content-Type", "application/json");

        //            string resultString = client.DownloadString(urlDataPoints);

        //            JArray jsonArray = JArray.Parse(resultString);

        //            DateTime dateTime = DateTime.Parse(jsonArray.First["From"].ToString());

        //            if (dateTime.Month == startMonth.Month)
        //            {
        //                monthConsumption = decimal.Parse(jsonArray.First["ReadingN"].ToString());
        //            }
        //        }
        //    }

        //    return monthConsumption;
        //}

        public void GetAccessToken()
        {

            string accessToken = string.Empty;

            string resultString;

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["grant_type"] = "client_credentials";
                values["resource"] = "https://fdrwsadprd.onmicrosoft.com/FuduraWebserviceProduction";
                values["client_id"] = FuduraClientID;
                values["client_secret"] = FuduraSecret;

                var response = client.UploadValues(apiFuduraTokenEndPoint, values);

                resultString = Encoding.Default.GetString(response);
            }


            var objectResult = JsonConvert.DeserializeObject(resultString) as Dictionary<string, object>;

            // Set accesstoken
            FuduraAccessToken = objectResult["access_token"].ToString();

            int expiresIn = int.Parse(objectResult["expires_in"].ToString());

            // Set expired date
            FuduraExpiredDate = DateTime.Now.AddSeconds(expiresIn);

        }
    }
        
}