using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Parameters;
using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jadval.Application.Features.Crosswords.Queries.GetPagedListCrossword2
{
    public class GetPagedListCrossword2Query : PagenationRequestParameter, IRequest<PagedResponse<Crossword>>
    {
    }

    public class GetPagedListCrossword2QueryHandler : IRequestHandler<GetPagedListCrossword2Query, PagedResponse<Crossword>>
    {
        private readonly ICrosswordRepository crosswordRepository;
        private readonly IGetUserServices getUserServices;

        public GetPagedListCrossword2QueryHandler(ICrosswordRepository crosswordRepository, IGetUserServices getUserServices)
        {
            this.crosswordRepository = crosswordRepository;
            this.getUserServices = getUserServices;
        }


        public async Task<PagedResponse<Crossword>> Handle(GetPagedListCrossword2Query request, CancellationToken cancellationToken)
        {
            var level = await getUserServices.GetUserLevel();

            var result = await crosswordRepository.GetPaged2(request, level.Data);

            return new PagedResponse<Crossword>(result, request.PageNumber, request.PageSize);
        }
    }
}
