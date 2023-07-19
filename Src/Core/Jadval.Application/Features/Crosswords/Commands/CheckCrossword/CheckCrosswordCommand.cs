using System.Collections.Generic;
using Jadval.Application.Wrappers;
using MediatR;

namespace Jadval.Application.Features.Crosswords.Commands.CheckCrossword
{
    public class CheckCrosswordCommand : IRequest<Result<List<CheckCrosswordResponce>>>
    {
        public int Level{ get; set; }
        public List<CheckCrosswordItem> Items { get; set; }
    }
    public class CheckCrosswordItem
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string Value { get; set; }
    }
}