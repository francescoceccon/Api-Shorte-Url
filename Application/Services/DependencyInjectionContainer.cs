using Application.Services.Cryptography;
using Application.Services.Interfaces;
using Infrastructure.Repositorys;
using Infrastructure.Repositorys.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public static class DependencyInjectionContainer
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddTransient<IURLService,URLService>();
            service.AddSingleton<IUrlRepository,UrlRepository>();
            service.AddSingleton<IUrlCryptography,UrlCryptography>();
            service.AddSingleton<UrlContext,UrlContext>();
        }
    }
}
