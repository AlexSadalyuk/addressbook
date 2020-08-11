using AutoMapper;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Concrete;
using Services.Options;
using System.Reflection;

namespace Services.Extensions
{
    public static class ServiceSettingsExtensions
    {
        public static IServiceCollection AddAddressBookServices(this IServiceCollection services, IConfigurationSection section)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<UserOptions>(section);
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
