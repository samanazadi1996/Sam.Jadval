using System.Collections.Generic;

namespace Jadval.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class Question
    {
        public int id { get; set; }
        public List<Value> value { get; set; }
    }

    public class BaseModel
    {
        public List<List<string>> data { get; set; }
        public List<Question> questions { get; set; }
    }

    public class Value
    {
        public Value(string question, string position)
        {
            this.question = question;
            this.position = position;
        }
        public string question { get; set; }
        public string position { get; set; }
    }
    public class CheckModel
    {
        public List<CheckModelItem> Items { get; set; }
    }
    public class CheckModelItem
    {
        public int row { get; set; }
        public int col { get; set; }
        public string value { get; set; }
    }
    public class CheckModelResult
    {
        public int row { get; set; }
        public int col { get; set; }
        public bool result { get; set; }
    }


}