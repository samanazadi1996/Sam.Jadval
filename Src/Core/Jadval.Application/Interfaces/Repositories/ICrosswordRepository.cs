using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jadval.Domain.Crosswords.Dtos;
using Jadval.Domain.Crosswords.Entities;

namespace Jadval.Application.Interfaces.Repositories
{
    public interface ICrosswordRepository : IGenericRepository<Crossword>
    {
        Task<Tuple<List<bool>, int>> GetPaged(int requestPageNumber, int requestPageSize, long level);
        Task<Crossword> GetByLevel(long level);
        Task<long> GetMaxLevel();
    }
}