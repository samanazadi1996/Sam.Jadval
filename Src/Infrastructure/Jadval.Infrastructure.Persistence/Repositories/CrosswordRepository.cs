using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Domain.Crosswords.Dtos;
using Jadval.Domain.Crosswords.Entities;
using Jadval.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jadval.Infrastructure.Persistence.Repositories
{
    public class CrosswordRepository : GenericRepository<Crossword>, ICrosswordRepository
    {
        private readonly DbSet<Crossword> crosswords;

        public CrosswordRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            crosswords = dbContext.Set<Crossword>();

        }

        public async Task<Tuple<List<bool>, int>> GetPaged(int requestPageNumber, int requestPageSize, int level)
        {
            var query = crosswords
                .OrderByDescending(p => p.Created)
                .AsQueryable();

            var count = query.Count();

            var result = await query
                .Skip((requestPageNumber - 1) * requestPageSize)
                .Take(requestPageSize)
                .Select(p => true)
                .ToListAsync();

            return new Tuple<List<bool>, int>(result, count);
        }

        public async Task<CrosswordDto> GetByLevel(int level)
        {
            var crossword = await crosswords
                .Skip(level)
                .Take(1)
                .Include(p => p.Questions)
                .ThenInclude(p => p.Value)

                .FirstOrDefaultAsync();


            var crosswordQuestions = new List<QuestionDto>();
            foreach (var question in crossword.Questions)
            {

                var crosswordQuestion = new QuestionDto()
                {
                    QuestionId = question.QuestionId
                };
                crosswordQuestion.Value = question.Value.Select(p => new ValueDto()
                {

                    Position = p.Position,
                    Question = p.Question
                }).ToList();
                crosswordQuestions.Add(crosswordQuestion);
            }

            var result = new CrosswordDto()
            {
                Data = crossword.GetData(),
                Questions = crosswordQuestions
            };

            return result;
        }
    }
}
