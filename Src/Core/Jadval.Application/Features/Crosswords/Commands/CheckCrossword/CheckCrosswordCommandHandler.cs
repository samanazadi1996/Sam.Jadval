using Jadval.Application.Features.Crosswords.Commands.CheckCrossword;
using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jadval.Application.Features.Crosswords.Commands.CheckCrossword
{
    public class CheckCrosswordCommandHandler : IRequestHandler<CheckCrosswordCommand, Result<List<CheckCrosswordResponce>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICrosswordRepository crosswordRepository;

        public CheckCrosswordCommandHandler(IUnitOfWork unitOfWork, ICrosswordRepository crosswordRepository)
        {
            this.unitOfWork = unitOfWork;
            this.crosswordRepository = crosswordRepository;
        }

        public async Task<Result<List<CheckCrosswordResponce>>> Handle(CheckCrosswordCommand request, CancellationToken cancellationToken)
        {
            var temp =  await crosswordRepository.GetByLevel(request.Level);

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
            
            return new Result<List<CheckCrosswordResponce>>(result);
        }
    }
}