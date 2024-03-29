﻿using SwarmBot.Application;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SwarmBot.HealthChecks
{
    public class DiscordBotNewsService_HealthCheck : IHealthCheck
    {
        private readonly IDiscordBotNewsService _discordBotNewsService;

        public DiscordBotNewsService_HealthCheck(IDiscordBotNewsService aDiscordBotNewsService)
            => _discordBotNewsService = aDiscordBotNewsService;

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext aContext, CancellationToken aCancellationToken = default)
            => await Task.FromResult(_discordBotNewsService.GetHealthCheck(aCancellationToken));

    }

}
