using Eisight.Site;
using Esight.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Esight.Services
{
    public class SiteServices
    {
        public EndpointAddress basicendpoint = new EndpointAddress(new Uri("https://esight.esightmonitoring.nl/eteck/ws/Site.asmx"));
        public BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
        public CookieContainer CookieContainer { get; private set; }
        public List<Site> Sites { get; private set; }
        public SiteServices()
        {
            LoginService login = new LoginService();
            //CookieContainer = login.CookieContainer;
        }

        public async Task GetSites(int? companyID)
        {
           
        Eisight.Site.SiteSoapClient siteService = new Eisight.Site.SiteSoapClient(basicHttpBinding, basicendpoint);
            //siteService.CookieContainer = CookieContainer;
            DataSet dataSet = new DataSet();
            if (companyID.HasValue)
            {
                ArrayOfXElement elements = await siteService.GetSitesForCompanyIDAsync(companyID.Value);
                dataSet = new DataSet();
                dataSet.ReadXml(new System.IO.StringReader(dataSet.ToString()));      
            }
            else
            {
                ArrayOfXElement elements = await siteService.GetSitesAsync();
                dataSet = new DataSet();
                dataSet.ReadXml(new System.IO.StringReader(dataSet.ToString()));
            }
            Sites = new List<Site>();
            if (!dataSet.HasErrors)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    
                    Sites.Add(new Site
                    {
                        ParentID = !companyID.HasValue ? int.Parse(row["ParentID"].ToString()) : 0,
                        ParentTypeID = !companyID.HasValue ? int.Parse(row["ParentTypeID"].ToString()) : 0,
                        SiteCode = !companyID.HasValue ? row["SiteCode"].ToString() : row["Code"].ToString(),
                        SiteID = !companyID.HasValue ? int.Parse(row["SiteID"].ToString()) : int.Parse(row["ObjectID"].ToString()),
                        SiteName = !companyID.HasValue ? row["SiteName"].ToString(): row["Name"].ToString(),
                    });
                }

                await Task.FromResult(1);
            }

            await Task.FromResult(0);
        }
        /*
        public Task GetSites(int? companyID)
        {
            throw new NotImplementedException();
        }
        */
    }

}
