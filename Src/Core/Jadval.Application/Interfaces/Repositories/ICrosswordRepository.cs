using Jadval.Application.Parameters;
using Jadval.Domain.Crosswords.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jadval.Application.Interfaces.Repositories
{
    public interface ICrosswordRepository : IGenericRepository<Crossword>
    {
        Task<Tuple<List<bool>, int>> GetPaged(PagenationRequestParameter requestParameter, long level);
        Task<Crossword> GetByLevel(long level);
        Task<long> GetMaxLevel();
    }
}