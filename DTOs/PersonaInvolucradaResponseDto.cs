public class PersonaInvolucradaResponseDto
{
    public int Id { get; set; }
    public int AccidenteId { get; set; }
    public int? VehiculoId { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Ci { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Rol { get; set; }
    public string? NumeroLicencia { get; set; }
    public string? CategoriaLicencia { get; set; }
    public decimal? GradoAlcoholemiaGral { get; set; }
    public string? EstadoAlcoholemia { get; set; }
    public string? EstadoSalud { get; set; }
    public string? CentroMedicoTraslado { get; set; }
    public string? DiagnosticoPreliminar { get; set; }
    public string? NumeroActaDosaje { get; set; }
    public string? Laboratorio { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? PlacaVehiculo { get; set; }
}
