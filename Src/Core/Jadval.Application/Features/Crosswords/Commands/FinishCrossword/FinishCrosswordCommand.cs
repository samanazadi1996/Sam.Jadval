using System.Collections.Generic;
using Jadval.Application.Wrappers;
using MediatR;

namespace Jadval.Application.Features.Crosswords.Commands.FinishCrossword
{
    public class FinishCrosswordCommand : IRequest<Result<long>>
    {
        public long Level { get; set; }
        public List<FinishCrosswordItem> Items { get; set; }
    }
    public class FinishCrosswordItem
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string Value { get; set; }
    }
}