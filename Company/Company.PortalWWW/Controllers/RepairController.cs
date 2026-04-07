using Company.PortalWWW.Models.Repair;
using Microsoft.AspNetCore.Mvc;

namespace Company.PortalWWW.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepairController : ControllerBase
{
    [HttpGet("summary")]
    public ActionResult<RepairSummaryDto> GetSummary()
    {
        var summary = new RepairSummaryDto(
            "Sprawdz koszt naprawy zanim wybierzesz warsztat",
            "Wpisz VIN, wybierz rodzaj szkody i porownaj mechanikow wedlug ceny, odleglosci oraz oceny.",
            new[]
            {
                "Pobranie danych auta po numerze VIN",
                "Wybor szkody z przygotowanej listy",
                "Orientacyjny koszt naprawy",
                "Lista mechanikow w poblizu"
            });

        return Ok(summary);
    }

    [HttpGet("workshops")]
    public ActionResult<IReadOnlyList<WorkshopDto>> GetWorkshops()
    {
        var workshops = new List<WorkshopDto>
        {
            new(1, "Auto Serwis Centrum", "Naprawa zgodna z wybranym typem szkody", "1.8 km", "4.9", "Od 1 250 zl"),
            new(2, "Mechanika Plus", "Naprawa zgodna z wybranym typem szkody", "3.4 km", "4.7", "Od 1 420 zl"),
            new(3, "Turbo Garage", "Naprawa zgodna z wybranym typem szkody", "5.1 km", "4.8", "Od 1 390 zl")
        };

        return Ok(workshops);
    }
}
