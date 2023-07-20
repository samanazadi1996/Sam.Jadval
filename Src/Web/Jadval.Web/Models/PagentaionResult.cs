namespace Jadval.Web.Models
{
    public class PagentaionResult<Tquery, Tresult>
    {
        public PagentaionResult(Tquery query, Tresult result)
        {
            Query = query;
            Result = result;
        }
        public Tquery Query { get; set; }
        public Tresult Result { get; set; }
    }

}