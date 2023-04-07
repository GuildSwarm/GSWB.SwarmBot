﻿using MediatR;
using TGF.CA.Domain.Primitives.Result;
using TGF.Common.ROP.HttpResult;
using TGF.Common.ROP.Result;

namespace MandrilAPI.Commands
{
    public class DeleteCategoryCommand : IRequest<IResult<Unit>>
    {
        public ulong CategoryId { get; private set; }

        public DeleteCategoryCommand(ulong aCategoryId)
        {
            CategoryId = aCategoryId;

        }

    }
}
