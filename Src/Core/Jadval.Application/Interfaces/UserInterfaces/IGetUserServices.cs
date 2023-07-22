using Jadval.Application.DTOs.Account.Requests;
using Jadval.Application.DTOs.Account.Responses;
using Jadval.Application.Wrappers;
using System.Threading.Tasks;

namespace Jadval.Application.Interfaces.UserInterfaces
{
    public interface IGetUserServices
    {
        Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model);
        Task<Result<long>> GetUserCoins();
        Task<Result<long>> GetUserLevel();
    }
}
