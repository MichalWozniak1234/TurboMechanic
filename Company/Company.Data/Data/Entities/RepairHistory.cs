namespace Company.Data.Entities;

public class RepairHistory
{
    public int Id { get; set; }

    public string VehicleBrand { get; set; } = string.Empty;

    public string VehicleModel { get; set; } = string.Empty;

    public int VehicleProductionYear { get; set; }

    public int DamageCategoryId { get; set; }

    public DamageCategory DamageCategory { get; set; } = null!;

    public decimal FinalRepairPrice { get; set; }

    public int RepairDurationDays { get; set; }

    public string Notes { get; set; } = string.Empty;
}