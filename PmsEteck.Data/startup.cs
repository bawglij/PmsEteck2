using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PmsEteck.Data
{
    class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
