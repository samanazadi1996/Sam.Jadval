using Jadval.Application.DTOs.Account.Requests;
using Jadval.Application.DTOs.Account.Responses;
using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Wrappers;
using Jadval.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jadval.Infrastructure.Identity.Services
{
    public class GetUserServices : IGetUserServices
    {
        private readonly IdentityContext identityContext;
        private readonly IAuthenticatedUserService authenticatedUser;

        public GetUserServices(IdentityContext identityContext, IAuthenticatedUserService authenticatedUser)
        {
            this.identityContext = identityContext;
            this.authenticatedUser = authenticatedUser;
        }
        public async Task<Result<long>> GetUserCoins()
        {
            var userId = new Guid(authenticatedUser.UserId);
            var user = await identityContext.Users.FirstOrDefaultAsync(p => p.Id == userId);

            return new Result<long>(user.Coins);
        }

        public async Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model)
        {
            var skip = (model.PageNumber - 1) * model.PageSize;

            var users = await identityContext.Users
                .Skip(skip)
                .Take(model.PageSize)
                .Select(p => new UserDto()
                {
                    Name = p.Name,
                    Email = p.Email,
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    Id = p.Id,
                    Coins = p.Coins,
                    Created = p.Created,
                    Level = p.Level
                }).ToListAsync();

            var result = Tuple.Create(users, await identityContext.Users.CountAsync());

            return new PagedResponse<UserDto>(result, model.PageNumber, model.PageSize);
        }

        public async Task<Result<long>> GetUserLevel()
        {
            var userId = new Guid(authenticatedUser.UserId);
            var user = await identityContext.Users.FirstOrDefaultAsync(p => p.Id == userId);

            return new Result<long>(user.Level);
        }
    }
}
