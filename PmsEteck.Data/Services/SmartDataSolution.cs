using Newtonsoft.Json.Linq;
using PmsEteck.Data.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PmsEteck.Data.Services
{
    public class SmartDataSolution
    {
        public readonly string EndPoint = "https://data.smartdatasolutions.nl";
        private readonly string ApiKey = ConfigurationManager.AppSettings["SDSApiKey"];
        private PmsEteckContext db = new PmsEteckContext();

        public string AssetType { get; set; }
        
        public Task<bool> ConsumptionMeterExists(string EanCode)
        {
            bool consumptionMeterExists = false;

            try
            {
                string url = string.Format("{0}/assets?apikey={1}&payloadonly=true&format=json&ShowDetailedInList=false&take=10000", EndPoint, ApiKey);

                WebClient client = new WebClient { Encoding = Encoding.UTF8 };

                try
                {
                    string result = client.DownloadString(url);

                    JObject resultObject = JObject.Parse(result);
                    JArray resultArray = JArray.Parse(resultObject["Payload"].ToString());
                    int numberOfResults = 0;
                    do
                    {
                        consumptionMeterExists = resultArray[numberOfResults]["Identifier"].ToString() == EanCode ? true : false;
                        numberOfResults++;
                    } while (!consumptionMeterExists && numberOfResults <= resultArray.Count());

                }
                catch (Exception)
                {
                }
                return Task.FromResult(consumptionMeterExists);

            }
            catch (Exception)
            {
                return Task.FromResult(consumptionMeterExists);
            }

        }
        public DateTime ConsumptionMeterAvailableFrom(string EANCode)
        {
            DateTime availableFirst = new DateTime();

            string urlAssetDetails = string.Format("{0}/assets/{1}?apikey={2}&format=json", EndPoint, EANCode, ApiKey);

            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            string resultString = client.DownloadString(urlAssetDetails);

            JObject returnObject = JObject.Parse(resultString);

            JToken payLoad = returnObject["Payload"];

            if (payLoad["Identifier"].ToString() == EANCode)
            {
                var parsed = DateTime.TryParse(payLoad["DataAvailableFirst"].ToString(), out availableFirst);
                //availableFirst = DateTime.Parse(payLoad["DataAvailableFirst"].ToString());
                AssetType = payLoad["AssetType"].ToString();
            }

            return availableFirst;
        }
        public DateTime LastAvailableDateTime(string EANCode)
        {
            DateTime availableLast = new DateTime();

            string urlAssetDetails = string.Format("{0}/assets/{1}?apikey={2}&format=json", EndPoint, EANCode, ApiKey);

            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            string resultString = client.DownloadString(urlAssetDetails);

            JObject returnObject = JObject.Parse(resultString);

            JToken payLoad = returnObject["Payload"];

            if (payLoad["Identifier"].ToString() == EANCode)
            {
                availableLast = DateTime.Parse(payLoad["DataAvailableLast"].ToString());
            }

            return availableLast;
        }
        public string GetConsumption(string EANCode, DateTime firstDateTime, DateTime lastDateTime)
        {
            string getUrl = string.Format("{0}/readings/Identifier/{1}/Day/{2}?apikey={3}&dateto={4}&datetimeformat=nl&format=json&source=day", EndPoint, EANCode, firstDateTime.ToString("yyyy-MM-dd"), ApiKey, lastDateTime.ToString("yyyy-MM-dd"));

            WebClient client = new WebClient { Encoding = Encoding.UTF8 };
            return client.DownloadString(getUrl);

        }
    }
}