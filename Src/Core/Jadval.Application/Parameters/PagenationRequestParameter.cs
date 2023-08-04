using System.Collections.Generic;

namespace Jadval.Application.Parameters
{
    public class PagenationRequestParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<PagenationFilterParameter> FilterParameters { get; set; }
        public List<PagenationOrderParameter> OrderParameters { get; set; }
        public PagenationRequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 20;
        }
        public PagenationRequestParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize;
        }
    }
    public class PagenationFilterParameter
    {
        public string PropertyName { get; set; }
        public OperationTypes Operation { get; set; }
        public string PropertyValue { get; set; }
    }
    public class PagenationOrderParameter
    {
        public string PropertyName { get; set; }
        public bool Descending { get; set; }
    }
    public enum OperationTypes
    {
        Equal,
        NotEqual,
        Contains,
        StartWith,
        EndsWith,
        LessThan,
        LessThanOrEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,
    }
}
