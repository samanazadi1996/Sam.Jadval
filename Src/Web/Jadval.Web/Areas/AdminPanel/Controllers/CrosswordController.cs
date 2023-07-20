using Jadval.Application.Features.Crosswords.Queries.GetPagedListCrossword;
using Jadval.Application.Wrappers;
using Jadval.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TSPStore.WebMvc.Areas.AdminPanel.Controllers
{
    public class CrosswordController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> _Index(GetPagedListCrosswordQuery model)
        {
            var serviceResult = await Mediator.Send(model);
            var pageResult = new PagentaionResult<GetPagedListCrosswordQuery, PagedResponse<bool>>(model, serviceResult);

            return PartialView(pageResult);
        }

    }

}
