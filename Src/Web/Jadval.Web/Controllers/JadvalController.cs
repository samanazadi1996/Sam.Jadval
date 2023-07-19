using System;
using System.Collections.Generic;
using System.Linq;
using Jadval.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jadval.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JadvalController : ControllerBase
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
                    result = temp.data[item.row][item.col] == item.value
                });
            }

            return result;
        }
        [HttpGet("get")]
        public BaseModel GetLevel(int level)
        {
            var rnd = new Random();
            var model = GetModel();
            for (int i = 0; i < 200; i++)
            {

                var row = rnd.Next(model.data.Count);
                var col = rnd.Next(model.data.First().Count);

                var row2 = rnd.Next(model.data.Count);
                var col2 = rnd.Next(model.data.First().Count);

                if (!IsNum(model.data[row][col]) && !IsNum(model.data[row2][col2]))
                {
                    var temp = model.data[row][col];

                    model.data[row][col] = model.data[row2][col2];

                    model.data[row2][col2] = temp;

                }

            }

            return model;
            bool IsNum(string str)
            {
                return "123456789".Contains(str);
            }
        }
        private BaseModel GetModel()
        {
            var model = new BaseModel();
            model.data = new List<List<string>>();
            model.questions = new List<Question>();

            model.data.Add(new List<string> { "م", "1", "2", "ف" });
            model.data.Add(new List<string> { "ذ", "ف", "ا", "ن" });
            model.data.Add(new List<string> { "ک", "ر", "ک", "3" });
            model.data.Add(new List<string> { "ی", "ع", "س", "4" });

            model.questions.Add(new Question
            {
                id = 1,
                value = new List<Value>()
                {
                    new Value("پاکیزهتر","left_bottom"),
                    new Value("غیر اصل","bottom")
                }
            });

            model.questions.Add(new Question
            {
                id = 2,
                value = new List<Value>()
                {
                    new Value("شگرد","right_bottom"),
                    new Value("قرص روانگردان","bottom")
                }
            });
            model.questions.Add(new Question
            {
                id = 3,
                value = new List<Value>()
                {
                    new Value("نفوذ کننده","top_left"),
                    new Value("پشم نرم","left")
                }
            });
            model.questions.Add(new Question
            {
                id = 4,
                value = new List<Value>()
                {
                    new Value("تلاش","left")
                }
            });
            return model;
        }
    }
}