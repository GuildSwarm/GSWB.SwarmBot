﻿using MandrilAPI.Queries;
using MandrilBot;
using MediatR;
using TheGoodFramework.CA.Domain.Primitives.Result;

namespace MandrilAPI.Handlers
{
    public class ExistDiscordUserHandler : IRequestHandler<ExistDiscordUserQuery, Result<bool>>
    {
        private readonly MandrilDiscordBot _mandtrilDiscordBot;
        public ExistDiscordUserHandler(MandrilDiscordBot aMandrilDiscordBot)
        {
            _mandtrilDiscordBot = aMandrilDiscordBot;
        }

        public async Task<Result<bool>> Handle(ExistDiscordUserQuery aRequest, CancellationToken aCancellationToken)
        {
            return await _mandtrilDiscordBot.ExistUser(aRequest.UserId, aCancellationToken);
        }

    }
}

