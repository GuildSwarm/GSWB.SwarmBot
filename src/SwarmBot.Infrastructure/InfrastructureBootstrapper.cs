﻿using SwarmBot.Application;
using SwarmBot.Infrastructure.Communication.MessageProducer;
using SwarmBot.Infrastructure.Services;
using SwarmBot;
using SwarmBot.BackgroundServices.NewMemberManager;
using SwarmBot.BackgroundServices.News;
using SwarmBot.HealthChecks;
using SwarmBot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TGF.CA.Infrastructure.Communication.RabbitMQ;
using TGF.CA.Infrastructure.Discovery;
using TGF.CA.Infrastructure.Security.Secrets;

namespace SwarmBot.Infrastructure
{
    /// <summary>
    /// Provides methods for configuring and using the application specific infrastructure layer components.
    /// </summary>
    public static class InfrastructureBootstrapper
    {
        /// <summary>
        /// Configures the necessary infrastructure services for the application.
        /// </summary>
        /// <param name="aWebApplicationBuilder">The web application builder.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static void ConfigureInfrastructure(this WebApplicationBuilder aWebApplicationBuilder)
        {
            aWebApplicationBuilder.Services.AddDiscoveryService(aWebApplicationBuilder.Configuration);
            aWebApplicationBuilder.Services.AddVaultSecretsManager();
            aWebApplicationBuilder.Services.AddMemoryCache();

            aWebApplicationBuilder.AddCommunicationServices();

            aWebApplicationBuilder.Services.AddSingleton<ISwarmBotDiscordBot, SwarmBotDiscordBot>()
                .AddSwarmBotPassiveServices()
                .AddSwarmBotActiveServices()
                .AddSwarmBotHealthChceckServices();
            aWebApplicationBuilder.Services.AddHostedService<ScToolsBackgroundTasks>();
        }

        /// <summary>
        /// Add SwarmBot related passive services. Require <see cref="ISwarmBotDiscordBot"/>.
        /// </summary>
        public static IServiceCollection AddSwarmBotPassiveServices(this IServiceCollection aServiceList)
        {
            //Required by DiscordBotNewsService.
            aServiceList.AddHttpClient()
            .AddSingleton<IDiscordBotNewsService, DiscordBotNewsMasterService>();

            aServiceList.AddSingleton<INewMemberManagementService, NewMemberManagementService>();
            aServiceList.AddHostedService<SwarmBotBackgroundTasks>();
            aServiceList.AddHostedService<SwarmBotIntegrationMessageProducer>();

            return aServiceList;

        }

        /// <summary>
        /// Add SwarmBot related active services. Require <see cref="ISwarmBotDiscordBot"/>.
        /// </summary>
        public static IServiceCollection AddSwarmBotActiveServices(this IServiceCollection aServiceList)
        {
            aServiceList.AddScoped<ISwarmBotUsersService, SwarmBotUsersService>();
            aServiceList.AddScoped<ISwarmBotChannelsService, SwarmBotChannelsService>();
            aServiceList.AddScoped<ISwarmBotMembersService, SwarmBotMembersService>();
            aServiceList.AddScoped<ISwarmBotRolesService, SwarmBotRolesService>();
            aServiceList.AddScoped<IScToolsService, ScToolsService>();

            return aServiceList;

        }

        /// <summary>
        /// Adds SwarmBot related health checks.
        /// </summary>
        /// <param name="aServiceList"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwarmBotHealthChceckServices(this IServiceCollection aServiceList)
        {
            aServiceList
                .AddHealthChecks()
                .AddCheck<SwarmBot_HealthCheck>(nameof(SwarmBot_HealthCheck))
                .AddCheck<SwarmBotAPI_HealthCheck>(nameof(SwarmBotAPI_HealthCheck))
                .AddCheck<DiscordBotNewsService_HealthCheck>(nameof(DiscordBotNewsService_HealthCheck));
            return aServiceList;
        }

        public static WebApplicationBuilder AddCommunicationServices(this WebApplicationBuilder aWebApplicationBuilder)
        {
            aWebApplicationBuilder.Services.AddServiceBusIntegrationPublisher();
            return aWebApplicationBuilder;
        }


        /// <summary>
        /// Applies the infrastructure configurations to the web application.
        /// </summary>
        /// <param name="aWebApplication">The Web application instance.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static void UseInfrastructure(this WebApplication aWebApplication)
        {
            aWebApplication.UseCookiePolicy(new CookiePolicyOptions()//call before any middelware with auth
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });
        }

    }
}
