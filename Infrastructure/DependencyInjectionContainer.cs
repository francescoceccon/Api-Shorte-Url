using Infrastructure.Repositorys;
using Infrastructure.Repositorys.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class DependencyInjectionContainer
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddTransient<IURLService,URLService>();
            service.AddSingleton<IUrlRepository,UrlRepository>();
            service.AddSingleton<UrlContext,UrlContext>();
        }
    }
}
