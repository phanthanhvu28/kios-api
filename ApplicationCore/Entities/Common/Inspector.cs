namespace ApplicationCore.Entities.Common;
public class Inspector
{
    public ProcessFlow? User { get; set; }

    public string Status { get; set; } = Constants.ProcessFlow.Status.New;
}
