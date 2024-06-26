﻿using DSharpPlus.Entities;
using Common.Application.DTOs.Discord;

namespace SwarmBot.Application.Mapping
{
    public static class DiscordRoleMapping
    {
        public static DiscordRoleDTO ToDto(this DiscordRole aDiscordRole)
            => new(aDiscordRole.Id.ToString(), aDiscordRole.Name, (byte)aDiscordRole.Position);
    }
}
