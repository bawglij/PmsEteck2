//using Esight.Esight.UserLogin;
using Esight.UserLogin;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Esight.Services
{
    public class LoginService
    {
        //private UserLogin userLogin;
        //UserLogin.UserLoginSoapChannel;
        readonly UserLoginSoap serviceProxy = null;
        public EndpointAddress basicendpoint = new EndpointAddress(new Uri("https://esight.esightmonitoring.nl/eteck/ws/UserLogin.asmx"));
        public BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
        public bool IsAuthentiated { get; private set; }
        //public CookieContainer CookieContainer { get; private set; }

        public LoginService()
        {

            ChannelFactory<UserLoginSoap> channelFactory = new ChannelFactory<UserLoginSoap>(basicHttpBinding, basicendpoint);

            channelFactory.Credentials.UserName.UserName = "API_PMS";
            channelFactory.Credentials.UserName.Password = "30083008";

            serviceProxy = channelFactory.CreateChannel();

        }

        public async Task<bool> LoginAsync()
        {

            //userLogin = new UserLogin();
            IsAuthentiated = await serviceProxy.LoginUserUsernamePasswordAsync("API_PMS", "30083008", false);
            //DateTime a = await serviceProxy.GetTimeStampAsync();
            return IsAuthentiated;


        }

    }
}
