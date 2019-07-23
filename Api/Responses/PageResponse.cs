using System.Collections.Generic;

namespace Api.Responses
{
    public class PageResponse<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
    }
}