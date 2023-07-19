using Jadval.Application.Interfaces.Repositories;
using Jadval.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Jadval.Infrastructure.Persistence.Repositories
{
    public class JadvalRepository : GenericRepository<Jadval.Domain.Jadval.Entities.Jadval>, IJadvalRepository
    {
        private readonly DbSet<Jadval.Domain.Jadval.Entities.Jadval> jadvals;

        public JadvalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            jadvals = dbContext.Set<Jadval.Domain.Jadval.Entities.Jadval>();

        }

    }
}
