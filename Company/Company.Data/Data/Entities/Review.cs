namespace Company.Data.Entities;

public class Review
{
    public int Id { get; set; }

    public int MechanicId { get; set; }

    public Mechanic Mechanic { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public bool IsApproved { get; set; } = true;
}