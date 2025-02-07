namespace NetCoreApp.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Sum { get; set; }
    public string Notes { get; set; } = string.Empty;

    public override string ToString() => $"Order ${Sum} (Id {Id}), UserId {UserId} {Notes}";
}
