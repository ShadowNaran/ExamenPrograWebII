public interface IEstadisticaService
{
    Task<List<PuntoCriticoResponseDto>> ObtenerPuntosCriticosAsync(int anio);
}
