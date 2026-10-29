public interface IAccidenteService
{
    Task<PaginacionResponseDto<AccidenteResumenResponseDto>> ListarAsync(AccidenteFiltroRequestDto filtros);
    Task<AccidenteResponseDto> BuscarPorIdAsync(int id);
    Task<AccidenteResponseDto> CrearAsync(AccidenteRequestDto datos);
    Task<AccidenteResponseDto> ActualizarEstadoAsync(int id, EstadoAccidenteRequestDto datos);
}
