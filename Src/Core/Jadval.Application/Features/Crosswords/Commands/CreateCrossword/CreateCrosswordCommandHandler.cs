using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Entities;
using MediatR;

namespace Jadval.Application.Features.Crosswords.Commands.CreateCrossword
{
    public class CreateCrosswordCommandHandler : IRequestHandler<CreateCrosswordCommand, Result<long>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICrosswordRepository crosswordRepository;

        public CreateCrosswordCommandHandler(IUnitOfWork unitOfWork, ICrosswordRepository crosswordRepository)
        {
            this.unitOfWork = unitOfWork;
            this.crosswordRepository = crosswordRepository;
        }

        public async Task<Result<long>> Handle(CreateCrosswordCommand request, CancellationToken cancellationToken)
        {
            var crosswordQuestions = new List<CrosswordQuestion>();
            foreach (var question in request.Questions)
            {

                var crosswordQuestion = new CrosswordQuestion()
                {
                    QuestionId = question.QuestionId
                };
                crosswordQuestion.Value=question.Value.Select(p=>new CrosswordQuestionValue(p.Question,p.Position)).ToList();
                crosswordQuestions.Add(crosswordQuestion);
            }

            var crossword = new Crossword(request.Data)
            {
                Questions = crosswordQuestions
            };


            await crosswordRepository.AddAsync(crossword);

            await unitOfWork.SaveChangesAsync();

            return new Result<long>(crossword.Id);
        }
    }
}