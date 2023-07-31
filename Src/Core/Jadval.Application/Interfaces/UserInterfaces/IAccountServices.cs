using Jadval.Application.DTOs.Account.Requests;
using Jadval.Application.Wrappers;
using System.Threading.Tasks;
using Jadval.Application.DTOs.Account.Responses;

namespace Jadval.Application.Interfaces.UserInterfaces
{
    public interface IAccountServices
    {
        Task<Result<string>> RegisterGostAccount();
        Task<BaseResult> ChangePassword(ChangePasswordRequest model);
        Task<BaseResult> ChangeUserName(ChangeUserNameRequest model);
        Task<Result<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
        Task<Result<AuthenticationResponse>> AuthenticateByUserName(string username);

    }
}
