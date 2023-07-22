using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Wrappers;
using Jadval.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Jadval.Infrastructure.Identity.Services
{
    public class UpdateUserServices : IUpdateUserServices
    {
        private readonly IdentityContext identityContext;
        private readonly IAuthenticatedUserService authenticatedUser;
        private readonly ICrosswordRepository crosswordRepository;

        public UpdateUserServices(IdentityContext identityContext, IAuthenticatedUserService authenticatedUser, ICrosswordRepository crosswordRepository)
        {
            this.identityContext = identityContext;
            this.authenticatedUser = authenticatedUser;
            this.crosswordRepository = crosswordRepository;
        }
        public async Task<Result<long>> ChangeUserCoins(long coins)
        {
            var userId = new Guid(authenticatedUser.UserId);
            var user = await identityContext.Users.FirstOrDefaultAsync(p => p.Id == userId);

            if (user.Coins <= 0)
                return new Result<long>(new Error(ErrorCode.NotInRange, ""));

            user.AddCoins(coins);
            await identityContext.SaveChangesAsync();

            return new Result<long>(user.Coins);
        }

        public async Task<Result<long>> LevelUp(long level)
        {
            var userId = new Guid(authenticatedUser.UserId);
            var user = await identityContext.Users.FirstOrDefaultAsync(p => p.Id == userId);

            var maxlevel = await crosswordRepository.GetMaxLevel();

            if (user.Level > level)
                return new Result<long>(++level);

            if (user.Level >= maxlevel)
                return new Result<long>(user.Level);

            user.LevelUp();
            await identityContext.SaveChangesAsync();

            return new Result<long>(user.Level);
        }
    }
}
