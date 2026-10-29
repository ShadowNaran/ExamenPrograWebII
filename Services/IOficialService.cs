public interface IOficialService
{
    Task<OficialResponseDto> CrearOficialAsync(OficialRequestDto datos);
}
