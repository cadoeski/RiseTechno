using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Report.Infrastructure.Settings;
using MassTransit; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Npgsql;
using Report.Infrastructure.Persistence;
using Report.Application.Contracts.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Report.Application.Contracts.Infrastructure;
using Report.Infrastructure.Queue;
using Microsoft.EntityFrameworkCore;

namespace Report.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {


            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor(); 

          

            


            services.AddSingleton<IConnectionMultiplexer>(i => ConnectionMultiplexer.Connect("redis.usharefy.com"));
             

            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetExecutingAssembly());
                configure.UsingRabbitMq((context, configurator) =>
                {

                    var rabbitMqSettings = configuration.GetSection(nameof(RabbitMqSetting)).Get<RabbitMqSetting>();
                    configurator.Host(rabbitMqSettings.Host, "/", cfg =>
                    {
                        cfg.Username(rabbitMqSettings.UserName);
                        cfg.Password(rabbitMqSettings.Password);
                    });
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("ReportService", false));


                });
            });


            services.AddDbContext<UsersDbContext>(options =>
                    options.UseNpgsql(configuration["Database:Writes"]));

            


            services.AddScoped<ILocationStatusReportWriteRepository, LocationStatusReportWriteRepository>(); 

            services.AddScoped<IMessageQueue, MessageQueue>();

            return services;
        }
    }
}
