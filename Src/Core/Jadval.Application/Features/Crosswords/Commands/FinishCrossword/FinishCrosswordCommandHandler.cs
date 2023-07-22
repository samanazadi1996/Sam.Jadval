using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Dtos;
using MediatR;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Jadval.Application.Features.Crosswords.Commands.FinishCrossword
{
    public class FinishCrosswordCommandHandler : IRequestHandler<FinishCrosswordCommand, Result<long>>
    {
        private readonly ICrosswordRepository crosswordRepository;
        private readonly IUpdateUserServices updateUserServices;

        public FinishCrosswordCommandHandler(ICrosswordRepository crosswordRepository, IUpdateUserServices updateUserServices)
        {
            this.crosswordRepository = crosswordRepository;
            this.updateUserServices = updateUserServices;
        }

        public async Task<Result<long>> Handle(FinishCrosswordCommand request, CancellationToken cancellationToken)
        {
            var crossword = await crosswordRepository.GetByLevel(request.Level);
            var crosswordDto = JsonSerializer.Deserialize<CrosswordDto>(crossword.Content);

            for (int r = 0; r < crosswordDto.Data.Count; r++)
            {
                for (int c = 0; c < crosswordDto.Data[0].Count; c++)
                {
                    if (!IsNum(crosswordDto.Data[r][c]))
                    {
                        if (crosswordDto.Data[r][c] != request.Items.FirstOrDefault(p => p.Col == c && p.Row == r)?.Value)
                        {
                            return new Result<long>(new Error(ErrorCode.InvalidOperation));
                        }
                    }
                }
            }
            var newLevel = await updateUserServices.LevelUp(request.Level);

            return new Result<long>(newLevel.Data);

            bool IsNum(string input)
            {
                int test;
                return int.TryParse(input, out test);
            }

        }
    }
}