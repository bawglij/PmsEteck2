using System;
using System.ServiceModel;
using System.Threading.Tasks;
using VolkerWesselsServices;

namespace PmsEteck.Data.Services
{
    public class MessageService : VolkerWesselsServices.MessageServiceSoap
    {
        public EndpointAddress basicendpoint = new EndpointAddress(new Uri("https://esight.esightmonitoring.nl/eteck/ws/Company.asmx"));
        public BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
        
        public MessageService()
        {
            
            //Credentials = new NetworkCredential("a_eteck@gast.volkerwessels.nl", "q8BCkH5tN5Vs1oxrouAD");

        }

        public Task<DeleteMessageResponse> DeleteMessageAsync(DeleteMessageRequest request)
        {
            throw new NotImplementedException();
        }

        /*
public override ISite Site { get => base.Site; set => base.Site = value; }

protected override bool CanRaiseEvents => base.CanRaiseEvents;

public override void Abort()
{
   base.Abort();
}
*/
        /*
        public override ObjRef CreateObjRef(Type requestedType)
        {
            return base.CreateObjRef(requestedType);
        }
        */
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public Task<GetAvailableMessagesResponse> GetAvailableMessagesAsync(GetAvailableMessagesRequest request)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Task<GetMessageResponse> GetMessageAsync(GetMessageRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteMessageResponse> PostMessageAsync(PostMessageRequest request)
        {
            throw new NotImplementedException();
        }

        /*
public override object InitializeLifetimeService()
{
   return base.InitializeLifetimeService();
}
*/
        public override string ToString()
        {
            return base.ToString();
        }
        /*
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        */
        /*
        protected override XmlReader GetReaderForMessage(SoapClientMessage message, int bufferSize)
        {
            return base.GetReaderForMessage(message, bufferSize);
        }

        protected override object GetService(Type service)
        {
            return base.GetService(service);
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            return base.GetWebRequest(uri);
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            return base.GetWebResponse(request);
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            return base.GetWebResponse(request, result);
        }

        protected override XmlWriter GetWriterForMessage(SoapClientMessage message, int bufferSize)
        {
            return base.GetWriterForMessage(message, bufferSize);
        }

        protected void PostMessage()
        {
            var a = new VolkerWesselsServices.MessageType();
            base.PostMessage(a);
        }
        */
    }
}
