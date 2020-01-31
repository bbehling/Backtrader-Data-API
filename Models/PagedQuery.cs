
public class PagedQuery
{
    public int pageSize { get; set; }
    public int pageIndex { get; set; } = 10;
    public string minDate { get; set; }
    public string maxDate { get; set; }
}