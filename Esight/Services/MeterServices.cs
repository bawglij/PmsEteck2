//using Esight.Esight.DataExport;
//using Esight.Esight.Meter;
using Eisight.Meter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Esight.Services
{
    public class MeterServices
    {
        public EndpointAddress basicendpoint = new EndpointAddress(new Uri("https://esight.esightmonitoring.nl/eteck/ws/Meter.asmx"));
        public BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

        public EndpointAddress basicendpointDataExport = new EndpointAddress(new Uri("https://esight.esightmonitoring.nl/eteck/ws/DataExport.asmx"));
        public BasicHttpBinding basicHttpBindingDataExport = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

        private readonly CookieContainer cookieContainer = new CookieContainer();
        private MeterSoapClient meter;
        private Eisight.DataExport.DataExportSoapClient dataExport;
        public List<Models.Meter> Meters { get; private set; }
        public MeterServices()
        {
            LoginService login = new LoginService();
            //cookieContainer = login.CookieContainer;
        }
        public MeterServices(CookieContainer cookieContainer)
        {
            this.cookieContainer = cookieContainer;
        }

        public async Task GetMetersForSiteId(int? siteID)
        {
            Meters = new List<Models.Meter>();
            meter = new MeterSoapClient(basicHttpBinding, basicendpoint);
            //meter.CookieContainer = cookieContainer;

            DataSet meterDataSet = new DataSet();
            if (siteID.HasValue)
            {
                ArrayOfXElement elements = await meter.GetMetersForSiteIDAsync(siteID.Value);
                meterDataSet = new DataSet();
                meterDataSet.ReadXml(new System.IO.StringReader(elements.ToString()));
            }
            else
            {
                ArrayOfXElement elements = await meter.GetMetersAsync();
                meterDataSet = new DataSet();
                meterDataSet.ReadXml(new System.IO.StringReader(elements.ToString()));
            }

            foreach (DataRow row in meterDataSet.Tables[0].Rows)
            {
                bool Enabled = false;
                bool.TryParse(row["Enabled"].ToString(), out Enabled);
                bool AMR = false;
                bool.TryParse(row["AMR"].ToString(), out AMR);
                Meters.Add(new Models.Meter
                {
                    AMR = AMR,
                    ContractNumber = row["ContractNumber"].ToString(),
                    DataImportCode = row["DataImportCode"].ToString(),
                    Enabled = Enabled,
                    MeterCode = row["MeterCode"].ToString(),
                    MeterID = int.Parse(row["MeterID"].ToString()),
                    MeterNumber = row["MeterNumber"].ToString(),
                    MeterPrefix = row["MeterPrefix"].ToString(),
                    MeterReadFrequency = row["MeterReadFrequency"].ToString(),
                    MeterType = row["MeterType"].ToString(),
                    MeterTypeID = int.Parse(row["MeterTypeID"].ToString()),
                    Name = row["Name"].ToString(),
                    ParentID = int.Parse(row["ParentID"].ToString()),
                    ParentTypeID = int.Parse(row["ParentTypeID"].ToString()),
                    SerialNumber = row["SerialNumber"].ToString(),
                    SiteMeterCode = row["SiteMeterCode"].ToString(),
                    TypeID = int.Parse(row["TypeID"].ToString()),
                });
            }
            await Task.FromResult(1);
        }

        public async Task<MeterInfo> GetMeterDetails(int meterID)
        {
            meter = new MeterSoapClient(basicHttpBinding, basicendpoint);
            return await meter.GetMeterAsync(meterID);
        }

        public async Task<List<Models.Consumption>> GetConsumptionFromMeter(int meterID, DateTime fromDate)
        {
            List<Models.Consumption> consumptionList = new List<Models.Consumption>();
            dataExport = new Eisight.DataExport.DataExportSoapClient(basicHttpBindingDataExport, basicendpointDataExport);
            //dataExport.CookieContainer = cookieContainer;
            if (fromDate < DateTime.Today.AddDays(-1))
            {
                string meterCode = await GetSiteMeterCodeForMeter(meterID);
                double correctionFactor = await GetCorrectionFactor(meterID);
                Eisight.DataExport.ArrayOfXElement elementsDataExport = await dataExport.GetDataAsync(meterCode, fromDate, DateTime.Today.AddDays(-1), "Ex-Day_By_Date", false, true, true, true, "N");
                DataSet data = new DataSet();
                data.ReadXml(new System.IO.StringReader(data.ToString()));


                foreach (DataRow row in data.Tables[0].Rows)
                {
                    consumptionList.Add(new Models.Consumption
                    {
                        RowNumber = int.Parse(row["XaxisValue"].ToString()),
                        DateTime = DateTime.Parse(row["XaxisLabel"].ToString()),
                        DayConsumption = !string.IsNullOrEmpty(row["Value"].ToString()) ? decimal.Multiply(decimal.Parse(row["Value"].ToString()), (decimal)correctionFactor) : 0
                    });
                }
                consumptionList = consumptionList.OrderBy(o => o.RowNumber).ToList();
            }
            return await Task.FromResult(consumptionList);
        }

        public async Task<List<Models.Consumption>> GetLowHighConsumption(int meterID, DateTime fromDate)
        {
            List<Models.Consumption> consumptionList = new List<Models.Consumption>();
            dataExport = new Eisight.DataExport.DataExportSoapClient(basicHttpBindingDataExport, basicendpointDataExport);

            if (fromDate < DateTime.Today.AddDays(-1))
            {
                Eisight.DataExport.ArrayOfXElement elemntsDataExport = await dataExport.ElecContractAnalysisAsync(meterID, fromDate, DateTime.Today.AddDays(-1), 67, false);
                DataSet data = new DataSet();
                data.ReadXml(new System.IO.StringReader(data.ToString()));
                double correctionFactor = await GetCorrectionFactor(meterID);
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    consumptionList.Add(new Models.Consumption
                    {
                        RowNumber = int.Parse(row["XaxisValue"].ToString()),
                        DateTime = DateTime.Parse(row["XaxisLabel"].ToString()),
                        DayConsumption = !string.IsNullOrEmpty(row[3].ToString()) ? decimal.Multiply(decimal.Parse(row[3].ToString()), (decimal)correctionFactor) : 0,
                        NightConsumption = !string.IsNullOrEmpty(row[4].ToString()) ? decimal.Multiply(decimal.Parse(row[4].ToString()), (decimal)correctionFactor) : 0
                    });
                }
                consumptionList = consumptionList.OrderBy(o => o.RowNumber).ToList();
            }
            return await Task.FromResult(consumptionList);
        }

        public async Task<string> GetSiteMeterCodeForMeter(int meterID)
        {
            meter = new MeterSoapClient(basicHttpBinding, basicendpoint);
            return await meter.GetSiteCodeForMeterAsync(meterID);
        }

        public async Task<bool> MeterHasContract(int meterID)
        {
            meter = new MeterSoapClient(basicHttpBinding, basicendpoint);
            MeterInfo meterInfo = await meter.GetMeterAsync(meterID);

            return await Task.FromResult(meterInfo.CostTariffID != 0);
        }

        public async Task<double> GetCorrectionFactor(int meterID)
        {
            meter = new MeterSoapClient(basicHttpBinding, basicendpoint);
            MeterInfo meterInfo = await meter.GetMeterAsync(meterID);

            return await Task.FromResult(meterInfo.CorrectionFactor > 0 ? meterInfo.CorrectionFactor : 1);
        }
         
    }

}
