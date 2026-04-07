namespace Company.PortalWWW.Models.Repair;

public record WorkshopDto(
    int Id,
    string Name,
    string Description,
    string Distance,
    string Rating,
    string PriceFrom);
