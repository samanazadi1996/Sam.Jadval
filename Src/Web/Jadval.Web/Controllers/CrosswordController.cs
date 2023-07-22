using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jadval.Application.Features.Crosswords.Commands.CheckCrossword;
using Jadval.Application.Features.Crosswords.Commands.CreateCrossword;
using Jadval.Application.Features.Crosswords.Commands.FinishCrossword;
using Jadval.Application.Features.Crosswords.Queries.GetCrosswordByLevel;
using Jadval.Application.Features.Crosswords.Queries.GetPagedListCrossword;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Dtos;
using Jadval.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Jadval.Web.Controllers
{
    public class CrosswordController : BaseController
    {
        private readonly IGetUserServices getUserServices;

        public CrosswordController(IGetUserServices getUserServices)
        {
            this.getUserServices = getUserServices;
        }

        public async Task<IActionResult> Index(GetPagedListCrosswordQuery query)
        {

            var result = await Mediator.Send(query);
            return View(result);
        }
        public async Task<IActionResult> Play()
        {
            var result = await getUserServices.GetUserLevel();
            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Play(GetCrosswordByLevelQuery model)
        {
            return View(model.Level);
        }

        [HttpPost]
        public async Task<List<CheckCrosswordResponce>> Check([FromBody] CheckCrosswordCommand query)
        {
            var result = await Mediator.Send(query);
            return result.Data;
        }

        [HttpPost]
        public async Task<Result<long>> FinishCrossword([FromBody] FinishCrosswordCommand query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<CrosswordDto> Get([FromQuery] GetCrosswordByLevelQuery query)
        {
            var model = await Mediator.Send(query);

            return model.Data;
        }
    }
}