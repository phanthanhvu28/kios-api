namespace ApplicationCore.Entities.Common;
public sealed record ActivitiesHistory
{
    public string? UserAction { get; set; }
    public string? Action { get; set; }
    public DateTime? DateAction { get; set; }
}
