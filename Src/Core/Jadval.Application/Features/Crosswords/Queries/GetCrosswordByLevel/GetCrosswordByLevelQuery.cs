using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Dtos;
using MediatR;

namespace Jadval.Application.Features.Crosswords.Queries.GetCrosswordByLevel
{
    public class GetCrosswordByLevelQuery : IRequest<Result<CrosswordDto>>
    {
        public long Level { get; set; } = 0;
    }
}
