using System.Collections.Generic;
using Jadval.Domain.Common;

namespace Jadval.Domain.Crosswords.Entities
{
    public class Crossword : AuditableBaseEntity
    {
        private Crossword()
        {

        }
        public Crossword(List<List<string>> data)
        {
            SetData(data);
        }

        public List<List<string>> GetData()
        {
            var model = new List<List<string>>();

            foreach (var item in Data.Split("_"))
            {
                var temp = new List<string>();
                foreach (var ss in item.Split(","))
                    temp.Add(ss);

                model.Add(temp);

            }

            return model;
        }
        public void SetData(List<List<string>> data)
        {
            var temp =new  List<string>();

            foreach (var ss in data)
                temp.Add(string.Join(",", ss));

            Data = string.Join("_", temp);
        }

        public string Data { get; set; }
        public List<CrosswordQuestion> Questions { get; set; }
    }
}
