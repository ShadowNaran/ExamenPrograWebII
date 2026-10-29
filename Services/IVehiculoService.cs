public interface IVehiculoService
{
    Task<VehiculoInvolucradoResponseDto> CrearAsync(VehiculoInvolucradoRequestDto datos);
    Task<List<VehiculoInvolucradoResponseDto>> ListarPorAccidenteAsync(int id);
}
