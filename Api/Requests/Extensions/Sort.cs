namespace Api.Requests.Extensions
{
    public class Sort
    {
        public string FieldName { get; set; } = "id";

        public SortType Order { get; set; } = SortType.Asc;
    }
}