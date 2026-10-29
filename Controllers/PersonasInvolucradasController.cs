using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/personas-involucradas")]
public class PersonasInvolucradasController : ControllerBase
{
    private readonly IPersonaService _service;
    public PersonasInvolucradasController(IPersonaService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<PersonaInvolucradaResponseDto> Crear(PersonaInvolucradaRequestDto datos)
    {
        return await _service.CrearAsync(datos);
    }

    [HttpPatch("{id:int}/alcoholemia")]
    public async Task<PersonaInvolucradaResponseDto> ActualizarAlcoholemia(int id, AlcoholemiaRequestDto datos)
    {
        return await _service.ActualizarAlcoholemiaAsync(id, datos);
    }
}
