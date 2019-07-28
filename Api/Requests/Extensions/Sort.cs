namespace Api.Requests.Extensions
{
    public class Sort
    {
        public string FieldName { get; set; }

        public SortType Order { get; set; } = SortType.Asc;
    }
}