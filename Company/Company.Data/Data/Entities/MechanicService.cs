namespace Company.Data.Entities;

public class MechanicService
{
    public int Id { get; set; }

    public int MechanicId { get; set; }

    public Mechanic Mechanic { get; set; } = null!;

    public int DamageCategoryId { get; set; }

    public DamageCategory DamageCategory { get; set; } = null!;

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }

    public int EstimatedRepairDays { get; set; }
}