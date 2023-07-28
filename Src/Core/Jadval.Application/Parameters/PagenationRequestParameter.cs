namespace Jadval.Application.Parameters
{
    public class PagenationRequestParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool Desc { get; set; }
        public string Order { get; set; }
        public PagenationRequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 20;
            this.Desc = false;
        }
        public PagenationRequestParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize;
        }
    }
}
