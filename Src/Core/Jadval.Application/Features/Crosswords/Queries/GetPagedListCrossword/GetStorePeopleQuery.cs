using System.Threading;
using System.Threading.Tasks;
using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Wrappers;
using MediatR;

namespace Jadval.Application.Features.Crosswords.Queries.GetPagedListCrossword
{
    public class GetPagedListCrosswordQuery : IRequest<PagedResponse<bool>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
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

            var result = await crosswordRepository.GetPaged(
                request.PageNumber,
                request.PageSize,
                level.Data
                );

            return new PagedResponse<bool>(result, request.PageNumber, request.PageSize);
        }
    }
}
