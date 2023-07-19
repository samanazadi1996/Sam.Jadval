using System.Collections.Generic;

namespace Jadval.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
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