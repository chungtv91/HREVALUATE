using System.Collections.Generic;

namespace HRE.Core.Shared.Dtos
{
    public class PagedResultDto<T>
    {
        public PagedResultDto()
        {
        }

        public PagedResultDto(int count, IEnumerable<T> items)
        {
            Count = count;
            Items = items;
        }

        public int Count { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
