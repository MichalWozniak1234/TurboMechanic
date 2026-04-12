namespace Company.Data.Entities;

public class Mechanic
{
    public int Id { get; set; }

    public string WorkshopName { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string AddressLine { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<MechanicService> MechanicServices { get; set; } = new List<MechanicService>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}