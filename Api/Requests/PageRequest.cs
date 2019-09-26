using Api.Requests.Extensions;

namespace Api.Requests
{
    public class PageRequest<TF>
    {
        public int Skip { get; set; }
        public int Limit { get; set; }

        public TF Filter { get; set; }

        public Sort Sort { get; set; } = new Sort();
    }
}