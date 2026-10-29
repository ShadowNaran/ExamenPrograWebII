using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/vehiculos-involucrados")]
public class VehiculosInvolucradosController : ControllerBase
{
    private readonly IVehiculoService _service;
    public VehiculosInvolucradosController(IVehiculoService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<VehiculoInvolucradoResponseDto> Crear(VehiculoInvolucradoRequestDto datos)
    {
        return await _service.CrearAsync(datos);
    }
}
