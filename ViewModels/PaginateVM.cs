namespace Business.ViewModels
{
    public class PaginateVM<T>
    {
        public List<T>? Items { get; set; }
        public int Currentpage { get; set; }
        public int PageCount { get; set; }
    }
}
