namespace Company.PortalWWW.Models.Repair;

public record RepairSummaryDto(
    string Title,
    string Description,
    IReadOnlyList<string> Steps);
