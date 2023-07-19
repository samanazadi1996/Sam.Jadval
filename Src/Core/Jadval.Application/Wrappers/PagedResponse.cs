﻿using System;
using System.Collections.Generic;

namespace Jadval.Application.Wrappers
{
    public class PagedResponse<T> : Result<List<T>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public PagedResponse()
        {

        }
        public PagedResponse(Error error) : base(error)
        {
        }

        public PagedResponse(Tuple<List<T>, int> data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = data.Item2;
            TotalPages = TotalItems / PageSize;
            if (TotalItems % PageSize > 0) TotalPages++;

            Data = data.Item1;
            Success = true;
        }
    }
}
