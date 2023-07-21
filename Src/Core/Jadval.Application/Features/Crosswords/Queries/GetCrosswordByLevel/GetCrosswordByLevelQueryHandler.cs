﻿using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Wrappers;
using Jadval.Domain.Crosswords.Dtos;
using MediatR;
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

        public GetCrosswordByLevelQueryHandler(IAuthenticatedUserService authenticatedUser, ICrosswordRepository crosswordRepository)
        {
            this.authenticatedUser = authenticatedUser;
            this.crosswordRepository = crosswordRepository;
        }


        public async Task<Result<CrosswordDto>> Handle(GetCrosswordByLevelQuery request, CancellationToken cancellationToken)
        {
            var userId = authenticatedUser.UserId;
            var crossword = await crosswordRepository.GetByLevel(request.Level);
            var result = JsonSerializer.Deserialize<CrosswordDto>(crossword.Content);
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

            return new Result<CrosswordDto>(result);

            bool IsNum(string str)
            {
                return "123456789".Contains(str);
            }

        }
    }
}