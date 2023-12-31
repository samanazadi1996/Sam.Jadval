﻿using Jadval.Application.Features.Crosswords.Commands.CheckCrossword;
using Jadval.Application.Features.Crosswords.Commands.FinishCrossword;
using Jadval.Application.Features.Crosswords.Queries.GetCrosswordByLevel;
using Jadval.Application.Features.Crosswords.Queries.GetPagedListCrossword;
using Jadval.Application.Features.Crosswords.Queries.GetPagedListCrossword2;
using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Dtos;
using Jadval.Domain.Crosswords.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jadval.Api.Controllers.v1
{
    public class CrosswordController : BaseApiController
    {
        [HttpPost, Authorize]
        public async Task<PagedResponse<bool>> GetPagedListCrossword(GetPagedListCrosswordQuery query)
            => await Mediator.Send(query);

        [HttpPost, Authorize]
        public async Task<PagedResponse<Crossword>> GetPagedListCrossword2(GetPagedListCrossword2Query query)
            => await Mediator.Send(query);

        [HttpPost, Authorize]
        public async Task<Result<List<CheckCrosswordResponce>>> CheckCrossword(CheckCrosswordCommand query)
            => await Mediator.Send(query);

        [HttpPost, Authorize]
        public async Task<Result<long>> FinishCrossword([FromBody] FinishCrosswordCommand query)
            => await Mediator.Send(query);

        [HttpGet, Authorize]
        public async Task<Result<CrosswordDto>> GetCrosswordByLevel([FromQuery] GetCrosswordByLevelQuery query)
            => await Mediator.Send(query);

    }
}