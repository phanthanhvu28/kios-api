namespace VELA.WebCoreBase.Core.Models;

public class PagingModel
{
    private readonly int _page = 1;
    public List<string> Includes { get; init; } = new();

    public List<FilterModel> Filters { get; init; } = new();
    public List<string> Sorts { get; init; } = new();

    public int Page
    {
        get => _page;
        init => _page = value <= 0 ? 1 : value;
    }

    public int PageSize { get; init; } = 10;
}