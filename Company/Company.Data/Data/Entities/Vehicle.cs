namespace Company.Data.Entities;

public class Vehicle
{
    public int Id { get; set; }

    public string Vin { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int ProductionYear { get; set; }

    public string EngineVersion { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public ICollection<DamageReport> DamageReports { get; set; } = new List<DamageReport>();
}