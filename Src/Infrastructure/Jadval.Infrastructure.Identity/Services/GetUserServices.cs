using Jadval.Application.DTOs.Account.Requests;
using Jadval.Application.DTOs.Account.Responses;
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

        public GetUserServices(IdentityContext identityContext)
        {
            this.identityContext = identityContext;
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
                }).ToListAsync();

            var result = Tuple.Create(users, await identityContext.Users.CountAsync());

            return new PagedResponse<UserDto>(result, model.PageNumber, model.PageSize);
        }
    }
}
