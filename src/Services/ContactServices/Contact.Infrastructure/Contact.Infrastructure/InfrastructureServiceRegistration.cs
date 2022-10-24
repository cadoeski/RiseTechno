using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Contact.Infrastructure.Settings;
using MassTransit; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Npgsql;
using Contact.Infrastructure.Persistence;
using Contact.Application.Contracts.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Contact.Application.Contracts.Infrastructure;
using Contact.Infrastructure.Queue;
using Microsoft.EntityFrameworkCore;

namespace Contact.Infrastructure
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
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("MusteriService", false));


                });
            });


            services.AddDbContext<UsersDbContext>(options =>
                    options.UseNpgsql(configuration["Database:Writes"]));

            services.AddDbContext<ContactDbContext>(options =>
               options.UseNpgsql(configuration["Database:Writes"]));


            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IContactWriteRepository, ContactWriteRepository>();
            services.AddScoped<IUsersReadRepository, UserReadRepository>();

            services.AddScoped<IMessageQueue, MessageQueue>();

            return services;
        }
    }
}
