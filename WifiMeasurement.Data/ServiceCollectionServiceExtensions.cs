using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiMeasurement.Data
{
    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services, string connectionString)
        {
            return services
                .AddRepository<IMeasurementRepository, MeasurementRepository, MeasurementContext>(connectionString);
        }
        public static IServiceCollection AddRepository<TRepositoryService, TRepositoryImplementation, TDbContext>(this IServiceCollection services, string connectionString)
            where TRepositoryService : class
            where TRepositoryImplementation : class, TRepositoryService
            where TDbContext : DbContext
        {
            return services
                .AddTransient<TRepositoryService, TRepositoryImplementation>()
                .AddDbContext<TDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
