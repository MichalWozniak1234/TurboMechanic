namespace Company.Data.Entities;

public class DamageReport
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public Vehicle Vehicle { get; set; } = null!;

    public int DamageCategoryId { get; set; }

    public DamageCategory DamageCategory { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = "New";

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public decimal? EstimatedCostMinimum { get; set; }

    public decimal? EstimatedCostMaximum { get; set; }
}