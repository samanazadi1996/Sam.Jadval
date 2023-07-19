using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jadval.Application.Features.Jadvals.Commands.CreateJadval;
using Jadval.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jadval.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JadvalController : BaseController
    {
        [HttpPost("Check")]
        public List<CheckModelResult> Check(CheckModel model)
        {
            var temp = GetModel();
            List<CheckModelResult> result = new();
            foreach (var item in model.Items)
            {
                result.Add(new CheckModelResult
                {
                    col = item.col,
                    row = item.row,
                    result = temp.Data[item.row][item.col] == item.value
                });
            }

            return result;
        }
        [HttpGet("get")]
        public async Task<CreateJadvalCommand> GetLevel(int level)
        {
            var rnd = new Random();
            var model = GetModel();
            await Mediator.Send(model);

            for (int i = 0; i < 200; i++)
            {

                var row = rnd.Next(model.Data.Count);
                var col = rnd.Next(model.Data.First().Count);

                var row2 = rnd.Next(model.Data.Count);
                var col2 = rnd.Next(model.Data.First().Count);

                if (!IsNum(model.Data[row][col]) && !IsNum(model.Data[row2][col2]))
                {
                    var temp = model.Data[row][col];

                    model.Data[row][col] = model.Data[row2][col2];

                    model.Data[row2][col2] = temp;

                }

            }

            return model;
            bool IsNum(string str)
            {
                return "123456789".Contains(str);
            }
        }
        private CreateJadvalCommand GetModel()
        {
            var model = new CreateJadvalCommand();
            model.Data = new List<List<string>>();
            model.Questions = new List<Question>();

            model.Data.Add(new List<string> { "م", "1", "2", "ف" });
            model.Data.Add(new List<string> { "ذ", "ف", "ا", "ن" });
            model.Data.Add(new List<string> { "ک", "ر", "ک", "3" });
            model.Data.Add(new List<string> { "ی", "ع", "س", "4" });

            model.Questions.Add(new Question
            {
                Id = 1,
                Value = new List<Value>()
                {
                    new Value("پاکیزهتر","left_bottom"),
                    new Value("غیر اصل","bottom")
                }
            });

            model.Questions.Add(new Question
            {
                Id = 2,
                Value = new List<Value>()
                {
                    new Value("شگرد","right_bottom"),
                    new Value("قرص روانگردان","bottom")
                }
            });
            model.Questions.Add(new Question
            {
                Id = 3,
                Value = new List<Value>()
                {
                    new Value("نفوذ کننده","top_left"),
                    new Value("پشم نرم","left")
                }
            });
            model.Questions.Add(new Question
            {
                Id = 4,
                Value = new List<Value>()
                {
                    new Value("تلاش","left")
                }
            });
            return model;
        }
    }
}