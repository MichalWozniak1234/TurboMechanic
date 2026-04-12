namespace Company.Data.Entities;

public class DamageCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<DamageReport> DamageReports { get; set; } = new List<DamageReport>();

    public ICollection<MechanicService> MechanicServices { get; set; } = new List<MechanicService>();

    public ICollection<RepairHistory> RepairHistories { get; set; } = new List<RepairHistory>();
}