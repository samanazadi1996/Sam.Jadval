using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Wrappers;
using Jadval.Domain.Jadval.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jadval.Application.Features.Jadvals.Commands.CreateJadval
{
    public class CreateJadvalCommandHandler : IRequestHandler<CreateJadvalCommand, Result<long>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJadvalRepository jadvalRepository;

        public CreateJadvalCommandHandler(IUnitOfWork unitOfWork, IJadvalRepository jadvalRepository)
        {
            this.unitOfWork = unitOfWork;
            this.jadvalRepository = jadvalRepository;
        }

        public async Task<Result<long>> Handle(CreateJadvalCommand request, CancellationToken cancellationToken)
        {
            var jadval = new Domain.Jadval.Entities.Jadval(request.Data)
            {
                Questions = new List<JadvalQuestion>()

            };
            foreach (var item in request.Questions)
            {
                var eeee = new JadvalQuestion()
                {
                    Id = item.Id,
                    Value = new List<JadvalQuestionValue>()
                };
                eeee.Value=new List<JadvalQuestionValue>();
                foreach (var item2 in item.Value)
                {
                    eeee.Value.Add(new JadvalQuestionValue(item2.Question,item2.Position));
                }
                jadval.Questions.Add(eeee);
            }

           await jadvalRepository.AddAsync(jadval);

            await unitOfWork.SaveChangesAsync();

            return new Result<long>(jadval.Id);
        }
    }
    public partial class CreateJadvalCommand : IRequest<Result<long>>
    {
        public List<List<string>> Data { get; set; }
        public List<Question> Questions { get; set; }
    }
    public class Question
    {
        public int Id { get; set; }
        public List<Value> Value { get; set; }
    }

    public class Value
    {
        public Value(string question, string position)
        {
            this.Question = question;
            this.Position = position;
        }
        public string Question { get; set; }
        public string Position { get; set; }
    }


}
