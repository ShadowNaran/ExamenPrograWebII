using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/estadisticas")]
public class EstadisticasController : ControllerBase
{
    private readonly IEstadisticaService _estadisticaService;
    public EstadisticasController(IEstadisticaService estadisticaService)
    {
        this._estadisticaService = estadisticaService;
    }

    [HttpGet("puntos-criticos")]
    public async Task<List<PuntoCriticoResponseDto>> ObtenerPuntosCriticos([FromQuery, Range(1, 9998)] int? anio = null)
    {
        return await _estadisticaService.ObtenerPuntosCriticosAsync(anio ?? DateTime.UtcNow.Year);
    }
}
