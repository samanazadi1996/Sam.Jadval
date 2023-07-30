using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Parameters;
using Jadval.Domain.Crosswords.Entities;
using Jadval.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jadval.Infrastructure.Persistence.Repositories
{
    public class CrosswordRepository : GenericRepository<Crossword>, ICrosswordRepository
    {
        private readonly DbSet<Crossword> crosswords;

        public CrosswordRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            crosswords = dbContext.Set<Crossword>();

        }
        public async Task<long> GetMaxLevel()
        {
            var query = await crosswords.CountAsync();
            return query;

        }
        public async Task<Tuple<List<bool>, int>> GetPaged(PagenationRequestParameter requestParameter, long level)
        {
            var query = crosswords.AsQueryable();

            var count = query.Count();

            var result = await Paged(query, requestParameter).Select(p => true).ToListAsync();

            var finalyResult = new List<bool>();
            var i = 0;

            foreach (var item in result)
            {
                i++;
                finalyResult.Add((requestParameter.PageNumber - 1) * requestParameter.PageSize + i <= level);

            }


            return new Tuple<List<bool>, int>(finalyResult, count);
        }

        public async Task<Crossword> GetByLevel(long level)
        {
            var crossword = await crosswords
                .Skip((int)level - 1)
                .Take(1)
                .FirstOrDefaultAsync();
            return crossword;
        }
    }
}
