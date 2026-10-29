using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/peritajes")]
public class PeritajesController : ControllerBase
{
    private readonly IPeritajeService _service;
    public PeritajesController(IPeritajeService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<PeritajeTecnicoResponseDto> Crear(PeritajeTecnicoRequestDto datos)
    {
        return await _service.CrearAsync(datos);
    }
}
