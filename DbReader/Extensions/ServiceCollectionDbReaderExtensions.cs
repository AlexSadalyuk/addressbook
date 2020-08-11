using DbReader.Concrete;
using DbReader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace DbReader.Extensions
{
    public static class ServiceCollectionDbReaderExtensions
    {
        public static IServiceCollection AddDbReader(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbReader>(dbreader => new DefaultDbReader(connectionString));
            return services;
        }
    }
}