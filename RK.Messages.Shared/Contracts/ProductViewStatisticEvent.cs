namespace RK.Messages.Shared.Contracts;

public class ProductViewStatisticEvent
{
    public string? Page { get; set; }
    public string? Production { get; set; }
    public long ProductionId { get; set; }
    public long CategoryId { get; set; }
    public string? Producer { get; set; }
    public string? UserName { get; set; }
    public DateTime Created { get; set; }
}