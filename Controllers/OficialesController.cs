using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/oficiales")]
public class OficialesController : ControllerBase
{
    private readonly IOficialService _service;
    public OficialesController(IOficialService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<OficialResponseDto> Crear(OficialRequestDto datos)
    {
        return await _service.CrearOficialAsync(datos);
    }
}
