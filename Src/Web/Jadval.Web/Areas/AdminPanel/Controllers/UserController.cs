using Jadval.Application.DTOs.Account.Requests;
using Jadval.Application.DTOs.Account.Responses;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Wrappers;
using Jadval.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TSPStore.WebMvc.Areas.AdminPanel.Controllers
{
    public class UserController : AdminBaseController
    {
        private readonly IGetUserServices getUserServices;

        public UserController(IGetUserServices getUserServices)
        {
            this.getUserServices = getUserServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> _Index(GetAllUsersRequest model)
        {
            var serviceResult = await getUserServices.GetPagedUsers(model);
            var pageResult = new PagentaionResult<GetAllUsersRequest, PagedResponse<UserDto>>(model, serviceResult);

            return PartialView(pageResult);
        }
    }

}
