using System.Collections.Generic;

namespace Project.Model.Dtos
{
    public class PageModel<T>
    {
        public PageModel()
        {
        }

        public PageModel(int total, IEnumerable<T> data)
        {
            Total = total;
            Data = data;
        }

        public int Total { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}