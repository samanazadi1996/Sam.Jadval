using Jadval.Application.DTOs.Account.Requests;
using Jadval.Application.Wrappers;
using System.Threading.Tasks;

namespace Jadval.Application.Interfaces.UserInterfaces
{
    public interface IAccountServices
    {
        Task<Result<string>> RegisterGostAccount();
        Task<BaseResult> ChangePassword(ChangePasswordRequest model);
        Task<BaseResult> ChangeUserName(ChangeUserNameRequest model);
    }
}
