using Application.Services.Cryptography;
using Application.Services.Interfaces;
using Infrastructure.Repositorys;
using Infrastructure.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Application.Services
{
    public static class DependencyInjectionContainer
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddTransient<IURLService,URLService>();
            service.AddScoped<IUrlRepository,UrlRepository>();
            service.AddScoped<IUrlCryptography,UrlCryptography>();
        }   

        public static void AddContext(this IServiceCollection service,string connection)
        {
            service.AddDbContext<UrlContext>(optionsAction => optionsAction.UseNpgsql(connection));
        }
    }
}
