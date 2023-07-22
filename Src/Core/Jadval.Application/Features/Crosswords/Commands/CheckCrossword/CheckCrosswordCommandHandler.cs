using Jadval.Application.Features.Crosswords.Commands.CheckCrossword;
using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Jadval.Application.Features.Crosswords.Commands.CheckCrossword
{
    public class CheckCrosswordCommandHandler : IRequestHandler<CheckCrosswordCommand, Result<List<CheckCrosswordResponce>>>
    {
        private readonly IGetUserServices getUserServices;
        private readonly ICrosswordRepository crosswordRepository;
        private readonly IUpdateUserServices updateUserServices;

        public CheckCrosswordCommandHandler(ICrosswordRepository crosswordRepository, IUpdateUserServices updateUserServices, IGetUserServices getUserServices)
        {
            this.crosswordRepository = crosswordRepository;
            this.updateUserServices = updateUserServices;
            this.getUserServices = getUserServices;
        }

        public async Task<Result<List<CheckCrosswordResponce>>> Handle(CheckCrosswordCommand request, CancellationToken cancellationToken)
        {
            var crossword = await crosswordRepository.GetByLevel(request.Level);
            var temp = JsonSerializer.Deserialize<CrosswordDto>(crossword.Content);

            List<CheckCrosswordResponce> result = new List<CheckCrosswordResponce>();
            foreach (var item in request.Items)
            {
                result.Add(new CheckCrosswordResponce
                {
                    Col = item.Col,
                    Row = item.Row,
                    Result = temp.Data[item.Row][item.Col] == item.Value
                });
            }

            var getUserCoins = await getUserServices.GetUserCoins();
            var changeUserCoinsResult = await updateUserServices.ChangeUserCoins(result.Any(p => p.Result) ? 1 : -5);
            if (!changeUserCoinsResult.Success)
            {
                return new Result<List<CheckCrosswordResponce>>(changeUserCoinsResult.Errors);
            }

            return new Result<List<CheckCrosswordResponce>>(result);
        }
    }
}