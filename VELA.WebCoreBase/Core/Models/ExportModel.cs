namespace VELA.WebCoreBase.Core.Models;

public record ExportModel
{
    public List<FilterModel> Filters { get; init; } = new();
    public List<string> Sorts { get; init; } = new();
    public List<string> Columns { get; init; } = new();

    public string? SheetTitle { get; set; }
}