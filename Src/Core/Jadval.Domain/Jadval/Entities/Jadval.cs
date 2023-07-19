using Jadval.Domain.Common;
using System.Collections.Generic;

namespace Jadval.Domain.Jadval.Entities
{
    public class Jadval : AuditableBaseEntity
    {
        private Jadval()
        {

        }
        public Jadval(List<List<string>> data)
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
        public List<JadvalQuestion> Questions { get; set; }
    }
}
