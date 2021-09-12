using System.Collections.Generic;

namespace Vega.Controllers.DataTransferObjects
{
    public class QueryResultDTO<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}