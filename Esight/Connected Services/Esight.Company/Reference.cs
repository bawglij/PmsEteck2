﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Esight.Company
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.0-preview-30131-0887")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.esightenergy.com/esight/WS/", ConfigurationName="Esight.Company.CompanySoap")]
    public interface CompanySoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.esightenergy.com/esight/WS/GetCompanies", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<Esight.Company.ArrayOfXElement> GetCompaniesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.esightenergy.com/esight/WS/GetCompanyIDFromName", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<int> GetCompanyIDFromNameAsync(string companyName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.esightenergy.com/esight/WS/GetCompany", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<Esight.Company.CompanyInfo> GetCompanyAsync(int companyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.esightenergy.com/esight/WS/SetCompany", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<Esight.Company.ResultInfo> SetCompanyAsync(Esight.Company.CompanyInfo company);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.esightenergy.com/esight/WS/DeleteCompany", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<Esight.Company.ResultInfo> DeleteCompanyAsync(int companyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.esightenergy.com/esight/WS/GetSites", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<Esight.Company.ArrayOfXElement> GetSitesAsync(int companyID);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.0-preview-30131-0887")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.esightenergy.com/esight/WS/")]
    public partial class ResultInfo
    {
        
        private bool successField;
        
        private int updateIDField;
        
        private string errorMessageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool Success
        {
            get
            {
                return this.successField;
            }
            set
            {
                this.successField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int UpdateID
        {
            get
            {
                return this.updateIDField;
            }
            set
            {
                this.updateIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessageField;
            }
            set
            {
                this.errorMessageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.0-preview-30131-0887")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.esightenergy.com/esight/WS/")]
    public partial class CompanyInfo
    {
        
        private int companyIDField;
        
        private string nameField;
        
        private string address1Field;
        
        private string address2Field;
        
        private string address3Field;
        
        private string cityField;
        
        private string countyField;
        
        private string postcodeField;
        
        private int countryIDField;
        
        private string regionField;
        
        private System.Nullable<System.DateTime> startDateField;
        
        private System.Nullable<System.DateTime> endDateField;
        
        private string companyRegisteredNumberField;
        
        private string customerNumberField;
        
        private string linkURLField;
        
        private int treeViewIconIDField;
        
        private int companyGroupIDField;
        
        private int parentTypeIDField;
        
        private int parentIDField;
        
        private int autofillMaximumGapIntervalField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int CompanyID
        {
            get
            {
                return this.companyIDField;
            }
            set
            {
                this.companyIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Address1
        {
            get
            {
                return this.address1Field;
            }
            set
            {
                this.address1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Address2
        {
            get
            {
                return this.address2Field;
            }
            set
            {
                this.address2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string Address3
        {
            get
            {
                return this.address3Field;
            }
            set
            {
                this.address3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string County
        {
            get
            {
                return this.countyField;
            }
            set
            {
                this.countyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string Postcode
        {
            get
            {
                return this.postcodeField;
            }
            set
            {
                this.postcodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public int CountryID
        {
            get
            {
                return this.countryIDField;
            }
            set
            {
                this.countryIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string Region
        {
            get
            {
                return this.regionField;
            }
            set
            {
                this.regionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public System.Nullable<System.DateTime> StartDate
        {
            get
            {
                return this.startDateField;
            }
            set
            {
                this.startDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public System.Nullable<System.DateTime> EndDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string CompanyRegisteredNumber
        {
            get
            {
                return this.companyRegisteredNumberField;
            }
            set
            {
                this.companyRegisteredNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string CustomerNumber
        {
            get
            {
                return this.customerNumberField;
            }
            set
            {
                this.customerNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public string LinkURL
        {
            get
            {
                return this.linkURLField;
            }
            set
            {
                this.linkURLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=15)]
        public int TreeViewIconID
        {
            get
            {
                return this.treeViewIconIDField;
            }
            set
            {
                this.treeViewIconIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=16)]
        public int CompanyGroupID
        {
            get
            {
                return this.companyGroupIDField;
            }
            set
            {
                this.companyGroupIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=17)]
        public int ParentTypeID
        {
            get
            {
                return this.parentTypeIDField;
            }
            set
            {
                this.parentTypeIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=18)]
        public int ParentID
        {
            get
            {
                return this.parentIDField;
            }
            set
            {
                this.parentIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=19)]
        public int AutofillMaximumGapInterval
        {
            get
            {
                return this.autofillMaximumGapIntervalField;
            }
            set
            {
                this.autofillMaximumGapIntervalField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.0-preview-30131-0887")]
    public interface CompanySoapChannel : Esight.Company.CompanySoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.0-preview-30131-0887")]
    public partial class CompanySoapClient : System.ServiceModel.ClientBase<Esight.Company.CompanySoap>, Esight.Company.CompanySoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public CompanySoapClient(EndpointConfiguration endpointConfiguration) : 
                base(CompanySoapClient.GetBindingForEndpoint(endpointConfiguration), CompanySoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CompanySoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(CompanySoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CompanySoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(CompanySoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CompanySoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<Esight.Company.ArrayOfXElement> GetCompaniesAsync()
        {
            return base.Channel.GetCompaniesAsync();
        }
        
        public System.Threading.Tasks.Task<int> GetCompanyIDFromNameAsync(string companyName)
        {
            return base.Channel.GetCompanyIDFromNameAsync(companyName);
        }
        
        public System.Threading.Tasks.Task<Esight.Company.CompanyInfo> GetCompanyAsync(int companyID)
        {
            return base.Channel.GetCompanyAsync(companyID);
        }
        
        public System.Threading.Tasks.Task<Esight.Company.ResultInfo> SetCompanyAsync(Esight.Company.CompanyInfo company)
        {
            return base.Channel.SetCompanyAsync(company);
        }
        
        public System.Threading.Tasks.Task<Esight.Company.ResultInfo> DeleteCompanyAsync(int companyID)
        {
            return base.Channel.DeleteCompanyAsync(companyID);
        }
        
        public System.Threading.Tasks.Task<Esight.Company.ArrayOfXElement> GetSitesAsync(int companyID)
        {
            return base.Channel.GetSitesAsync(companyID);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.CompanySoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.CompanySoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.CompanySoap))
            {
                return new System.ServiceModel.EndpointAddress("https://esight.esightmonitoring.nl/eteck/ws/Company.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.CompanySoap12))
            {
                return new System.ServiceModel.EndpointAddress("https://esight.esightmonitoring.nl/eteck/ws/Company.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            CompanySoap,
            
            CompanySoap12,
        }
    }
    
    [System.Xml.Serialization.XmlSchemaProviderAttribute(null, IsAny=true)]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil-lib", "2.0.0.0")]
    public partial class ArrayOfXElement : object, System.Xml.Serialization.IXmlSerializable
    {
        
        private System.Collections.Generic.List<System.Xml.Linq.XElement> nodesList = new System.Collections.Generic.List<System.Xml.Linq.XElement>();
        
        public ArrayOfXElement()
        {
        }
        
        public virtual System.Collections.Generic.List<System.Xml.Linq.XElement> Nodes
        {
            get
            {
                return this.nodesList;
            }
        }
        
        public virtual System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new System.NotImplementedException();
        }
        
        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            System.Collections.Generic.IEnumerator<System.Xml.Linq.XElement> e = nodesList.GetEnumerator();
            for (
            ; e.MoveNext(); 
            )
            {
                ((System.Xml.Serialization.IXmlSerializable)(e.Current)).WriteXml(writer);
            }
        }
        
        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            for (
            ; (reader.NodeType != System.Xml.XmlNodeType.EndElement); 
            )
            {
                if ((reader.NodeType == System.Xml.XmlNodeType.Element))
                {
                    System.Xml.Linq.XElement elem = new System.Xml.Linq.XElement("default");
                    ((System.Xml.Serialization.IXmlSerializable)(elem)).ReadXml(reader);
                    Nodes.Add(elem);
                }
                else
                {
                    reader.Skip();
                }
            }
        }
    }
}
