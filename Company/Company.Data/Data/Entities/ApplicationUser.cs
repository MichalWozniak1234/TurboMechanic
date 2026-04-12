using Microsoft.AspNetCore.Identity;

namespace Company.Data.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    public ICollection<DamageReport> DamageReports { get; set; } = new List<DamageReport>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}