using Brunata.Models;
using RestSharp;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;

namespace Brunata.Services
{
    public class BrunataManager
    {
        #region Constructors

        public BrunataManager(int objectID)
        {
            ObjectID = objectID;
            GetSystemFromApi();
        }

        #endregion

        #region Public Properties

        public int ObjectID { get; set; }

        public Models.Object ObjectDetails { get; private set; }

        public Meter MeterDetails { get; private set; }

        public Collection<Reading> LatestReadings { get; private set; }

        public Collection<Reading> DailyReadings { get; private set; }

        #endregion

        #region Private Properties
        AuthenticationManager Authentication = new AuthenticationManager("EteckClient", "1BE7031F#5D51-4909-a647-632D422989e6");

        RestClient RestClient = new RestClient(ConfigurationManager.AppSettings["BaseUri"]);

        #endregion
        #region Private Methods

        private void GetSystemFromApi()
        {
            if (!string.IsNullOrWhiteSpace(Authentication.AccessToken))
            {

                // Check if authenticationtoken must renewed
                if (Authentication.Expires < DateTime.UtcNow.AddMinutes(2))
                    Authentication.RenewAuthentication();
                RestRequest request = new RestRequest("systems/{SystemID}", Method.GET);
                request.AddUrlSegment("SystemID", ObjectID.ToString());
                request.AddHeader("Authorization", "bearer " + Authentication.AccessToken);

                IRestResponse<Models.Object> systemResponse = RestClient.Execute<Models.Object>(request);
                if (systemResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObjectDetails = systemResponse.Data;
                }
                else
                {
                    ObjectDetails = null;
                }
            }
        }

        public Result GetLatestReadingForMeter(string meterNumber)
        {
            if (ObjectID != 0)
            {

                // Check if authenticationtoken must renewed
                if (Authentication.Expires < DateTime.UtcNow.AddMinutes(2))
                    Authentication.RenewAuthentication();

                RestRequest request = new RestRequest("systems/{SystemID}/meters/readings/latest", Method.GET);
                request.AddUrlSegment("SystemID", ObjectID.ToString());
                IRestResponse<Collection<Meter>> response = RestClient.Execute<Collection<Meter>>(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Select meter and exclude Pulse
                    Meter meter = response.Data.FirstOrDefault(f => f.SerialNumber == meterNumber && f.UsageType.ToUpper() != "P");
                    if (meter != null)
                    {
                        MeterDetails = meter;
                        LatestReadings = meter.Readings;
                        return Result.Success;
                    }
                }
            }
            return Result.Failed("Gegevens voor het gebouw zijn nog niet opgehaald.");
        }


        public Result GetDailyReadingsForMeter(string meterNumber, DateTime startDate, DateTime endDate)
        {
            if (ObjectID != 0)
            {

                // Check if authenticationtoken must renewed
                if (Authentication.Expires < DateTime.UtcNow.AddMinutes(2))
                    Authentication.RenewAuthentication();

                RestRequest request = new RestRequest("systems/{SystemID}/meters/readings/daily", Method.GET);
                request.AddUrlSegment("SystemID", ObjectID.ToString());
                request.AddQueryParameter("usageType", MeterDetails.UsageType);
                request.AddQueryParameter("start", startDate.ToShortDateString());
                request.AddQueryParameter("start", endDate.ToShortDateString());

                IRestResponse<Collection<Meter>> response = RestClient.Execute<Collection<Meter>>(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Select meter and exclude Pulse
                    Meter meter = response.Data.FirstOrDefault(f => f.SerialNumber == meterNumber && f.UsageType.ToUpper() != "P");
                    if (meter != null)
                    {
                        MeterDetails = meter;
                        DailyReadings = meter.Readings;
                        return Result.Success;
                    }
                }
                return Result.Failed("Er ging iets fout met het ophalen van de standen.");

            }
            return Result.Failed("Gegevens voor het gebouw zijn nog niet opgehaald.");
        }
        #endregion
    }
}
