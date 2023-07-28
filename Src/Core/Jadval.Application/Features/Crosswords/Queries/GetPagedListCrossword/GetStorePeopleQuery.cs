using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Parameters;
using Jadval.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jadval.Application.Features.Crosswords.Queries.GetPagedListCrossword
{
    public class GetPagedListCrosswordQuery : PagenationRequestParameter, IRequest<PagedResponse<bool>>
    {
    }

    public class GetPagedListCrosswordQueryHandler : IRequestHandler<GetPagedListCrosswordQuery, PagedResponse<bool>>
    {
        private readonly ICrosswordRepository crosswordRepository;
        private readonly IGetUserServices getUserServices;

        public GetPagedListCrosswordQueryHandler(ICrosswordRepository crosswordRepository, IGetUserServices getUserServices)
        {
            this.crosswordRepository = crosswordRepository;
            this.getUserServices = getUserServices;
        }


        public async Task<PagedResponse<bool>> Handle(GetPagedListCrosswordQuery request, CancellationToken cancellationToken)
        {
            var level = await getUserServices.GetUserLevel();

            var result = await crosswordRepository.GetPaged(request, level.Data);

            return new PagedResponse<bool>(result, request.PageNumber, request.PageSize);
        }
    }
}
