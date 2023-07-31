
using Jadval.Application.DTOs.Account.Requests;
using Jadval.Application.DTOs.Account.Responses;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jadval.Api.Controllers.v1
{
    [ApiVersion("1")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountServices accountServices;
        private readonly IGetUserServices getUserServices;

        public AccountController(IAccountServices accountServices, IGetUserServices getUserServices)
        {
            this.accountServices = accountServices;
            this.getUserServices = getUserServices;
        }

        [HttpPost]
        public async Task<Result<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
            => await accountServices.Authenticate(request);

        [HttpPut, Authorize]
        public async Task<BaseResult> ChangeUserName(ChangeUserNameRequest model)
            => await accountServices.ChangeUserName(model);

        [HttpPut, Authorize]
        public async Task<BaseResult> ChangePassword(ChangePasswordRequest model)
            => await accountServices.ChangePassword(model);

        [HttpPost]
        public async Task<Result<AuthenticationResponse>> Start()
        {
            var gostUsername = await accountServices.RegisterGostAccount();
            return await accountServices.AuthenticateByUserName(gostUsername.Data);
        }

        [HttpGet, Authorize]
        public async Task<Result<long>> GetCoins() => await getUserServices.GetUserCoins();

        [HttpGet, Authorize]
        public async Task<Result<long>> GetUserLevel() => await getUserServices.GetUserLevel();

    }
}