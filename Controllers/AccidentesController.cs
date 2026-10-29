using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/accidentes")]
public class AccidentesController : ControllerBase
{
    private readonly IAccidenteService _accidenteService;
    private readonly IVehiculoService _vehiculoService;
    public AccidentesController(IAccidenteService accidenteService, IVehiculoService vehiculoService)
    {
        this._accidenteService = accidenteService;
        this._vehiculoService = vehiculoService;
    }

    [HttpGet]
    public async Task<PaginacionResponseDto<AccidenteResumenResponseDto>> ObtenerTodos([FromQuery] AccidenteFiltroRequestDto filtros)
    {
        return await _accidenteService.ListarAsync(filtros);
    }

    [HttpGet("{id:int}")]
    public async Task<AccidenteResponseDto> BuscarPorId(int id)
    {
        return await _accidenteService.BuscarPorIdAsync(id);
    }

    [HttpPost]
    public async Task<AccidenteResponseDto> Crear(AccidenteRequestDto datos)
    {
        return await _accidenteService.CrearAsync(datos);
    }

    [HttpPatch("{id:int}/estado")]
    public async Task<AccidenteResponseDto> ActualizarEstado(int id, EstadoAccidenteRequestDto datos)
    {
        return await _accidenteService.ActualizarEstadoAsync(id, datos);
    }

    [HttpGet("{id:int}/vehiculos")]
    public async Task<List<VehiculoInvolucradoResponseDto>> ObtenerVehiculos(int id)
    {
        return await _vehiculoService.ListarPorAccidenteAsync(id);
    }
}
