
namespace ApiCatalogo.Pagination;

public class PagedResponse<T>
{
  public int TotalCount { get; set; }
  public int PageSize { get; set; }
  public int CurrentPage { get; set; }
  public int TotalPages { get; set; }
  public bool HasPrevious { get; set; }
  public bool HasNext { get; set; }
  public IEnumerable<T> List { get; set; } = [];
}

