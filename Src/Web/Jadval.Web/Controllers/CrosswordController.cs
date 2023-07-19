using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jadval.Application.Features.Crosswords.Commands.CheckCrossword;
using Jadval.Application.Features.Crosswords.Commands.CreateCrossword;
using Jadval.Application.Features.Crosswords.Queries.GetCrosswordByLevel;
using Jadval.Application.Features.Crosswords.Queries.GetPagedListCrossword;
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
        public async Task<IActionResult> Index(GetPagedListCrosswordQuery query)
        {

            var result = await Mediator.Send(query);
            return View(result);
        }
        public IActionResult Play(int level) => View(level);

        [HttpPost]
        public async Task<List<CheckCrosswordResponce>> Check([FromBody] CheckCrosswordCommand query)
        {
            var result = await Mediator.Send(query);
            return result.Data;
        }

        [HttpGet]
        public async Task<CrosswordDto> Get([FromQuery] GetCrosswordByLevelQuery query)
        {
            var model = await Mediator.Send(query);

            return model.Data;
        }
    }
}