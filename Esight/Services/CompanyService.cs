using Esight.Company;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml.Linq;

namespace Esight.Services
{
    public class CompanyService
    {
        readonly CompanySoap serviceProxy = null;
        public EndpointAddress basicendpoint = new EndpointAddress(new Uri("https://esight.esightmonitoring.nl/eteck/ws/Company.asmx"));
        public BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

        public List<Models.Company> Companies { get; private set; }
        public CompanyService()
        {
            ChannelFactory<CompanySoap> channelFactory = new ChannelFactory<CompanySoap>(basicHttpBinding, basicendpoint);

            channelFactory.Credentials.UserName.UserName = "API_PMS";
            channelFactory.Credentials.UserName.Password = "30083008";

            serviceProxy = channelFactory.CreateChannel();
        }

        public async Task GetCompanies()
        {
            ArrayOfXElement elements = await serviceProxy.GetCompaniesAsync();
           
            foreach(XElement el in elements.Nodes)
            {
                var test = el.Value;
            }

            DataSet dataset = new DataSet();

            dataset.ReadXml(new System.IO.StringReader(elements.ToString()));

            if (!dataset.HasErrors)
            {
                Companies = new List<Models.Company>();
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    Companies.Add(new Models.Company
                    {
                        ObjectID = int.Parse(row["ObjectID"].ToString()),
                        Name = row["Name"].ToString(),
                        NumberOfChildren = int.Parse(row["NumberOfChildren"].ToString()),
                    });
                }

                await Task.FromResult(1);
            }
            await Task.FromResult(0);

        }

    }
}
