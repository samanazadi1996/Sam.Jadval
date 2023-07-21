using System.Collections.Generic;
using Jadval.Application.Wrappers;
using MediatR;

namespace Jadval.Application.Features.Crosswords.Commands.CreateCrossword
{
    public partial class CreateCrosswordCommand : IRequest<Result<long>>
    {
        public List<List<string>> Data { get; set; }
        public List<Question> Questions { get; set; }
    }
    public class Question
    {
        public int QuestionId { get; set; }
        public List<Value> Value { get; set; }
    }

    public class Value
    {
        public Value()
        {

        }
        public Value(string question, string position)
        {
            Question = question;
            Position = position;
        }
        public string Question { get; set; }
        public string Position { get; set; }
    }

}