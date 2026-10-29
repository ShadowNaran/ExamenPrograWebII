public interface IPersonaService
{
    Task<PersonaInvolucradaResponseDto> CrearAsync(PersonaInvolucradaRequestDto datos);
    Task<PersonaInvolucradaResponseDto> ActualizarAlcoholemiaAsync(int id, AlcoholemiaRequestDto datos);
}
