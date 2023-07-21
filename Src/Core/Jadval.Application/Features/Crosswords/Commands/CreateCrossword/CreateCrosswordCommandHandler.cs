using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Entities;
using MediatR;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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
            var content = JsonSerializer.Serialize(request);
            var crossword = new Crossword(content);

            await crosswordRepository.AddAsync(crossword);

            await unitOfWork.SaveChangesAsync();

            return new Result<long>(crossword.Id);
        }
    }
}