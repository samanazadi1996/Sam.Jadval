using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Dtos;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Jadval.Application.Features.Crosswords.Queries.GetCrosswordByLevel
{

    public class GetCrosswordByLevelQueryHandler : IRequestHandler<GetCrosswordByLevelQuery, Result<CrosswordDto>>
    {
        private readonly ICrosswordRepository crosswordRepository;
        private readonly IAuthenticatedUserService authenticatedUser;
        private readonly IDistributedCache distributedCache;
        public GetCrosswordByLevelQueryHandler(IAuthenticatedUserService authenticatedUser, ICrosswordRepository crosswordRepository, IDistributedCache distributedCache)
        {
            this.authenticatedUser = authenticatedUser;
            this.crosswordRepository = crosswordRepository;
            this.distributedCache = distributedCache;
        }


        public async Task<Result<CrosswordDto>> Handle(GetCrosswordByLevelQuery request, CancellationToken cancellationToken)
        {
            var cachestring = await distributedCache.GetStringAsync(GetCacheKey());
            if (!string.IsNullOrEmpty(cachestring))
                return JsonSerializer.Deserialize<Result<CrosswordDto>>(cachestring);

            var userId = authenticatedUser.UserId;
            var crossword = await crosswordRepository.GetByLevel(request.Level);
            var result = JsonSerializer.Deserialize<CrosswordDto>(crossword.Content);
            var result2 = JsonSerializer.Deserialize<CrosswordDto>(crossword.Content);
            do
            {
                Randomize();

            } while (IsNotRandomaize());

            var finalyResult = new Result<CrosswordDto>(result);
            await distributedCache.SetStringAsync(GetCacheKey(), JsonSerializer.Serialize(finalyResult));

            return finalyResult;

            bool IsNum(string input)
            {
                int test;
                return int.TryParse(input, out test);
            }
            bool IsNotRandomaize()
            {
                for (int r = 0; r < result.Data.Count; r++)
                {
                    for (int c = 0; c < result.Data[0].Count; c++)
                    {
                        if (!IsNum(result.Data[r][c]) && result.Data[r][c] == result2.Data[r][c])
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            void Randomize()
            {
                var rnd = new Random();
                for (int i = 0; i < 200; i++)
                {

                    var row = rnd.Next(result.Data.Count);
                    var col = rnd.Next(result.Data.First().Count);

                    var row2 = rnd.Next(result.Data.Count);
                    var col2 = rnd.Next(result.Data.First().Count);

                    if (!IsNum(result.Data[row][col]) && !IsNum(result.Data[row2][col2]))
                    {
                        var temp = result.Data[row][col];
                        result.Data[row][col] = result.Data[row2][col2];

                        result.Data[row2][col2] = temp;

                    }

                }

            }
            string GetCacheKey()
            {
                return $"{nameof(GetCrosswordByLevelQuery)}-{request.Level}";
            }

        }
    }
}
