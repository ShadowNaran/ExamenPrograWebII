public class AccidenteResponseDto
{
    public int Id { get; set; }
    public string CodigoCaso { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string TipoAccidente { get; set; } = string.Empty;
    public string Gravedad { get; set; } = string.Empty;
    public string Distrito { get; set; } = string.Empty;
    public string ZonaBarrio { get; set; } = string.Empty;
    public string DireccionReferencia { get; set; } = string.Empty;
    public decimal? Latitud { get; set; }
    public decimal? Longitud { get; set; }
    public string? CondicionClimatica { get; set; }
    public string? EstadoCalzada { get; set; }
    public string EstadoCaso { get; set; } = "en_investigacion";
    public int? OficialAsignadoId { get; set; }
    public string? Observaciones { get; set; }
    public string? JuzgadoOInstitucion { get; set; }
    public string? NumeroCuadernoInvestigacion { get; set; }
    public string? MotivoPase { get; set; }
    public DateTime CreatedAt { get; set; }
    public OficialResponseDto? OficialAsignado { get; set; }
    public List<VehiculoInvolucradoResponseDto> Vehiculos { get; set; } = new();
    public List<PersonaInvolucradaResponseDto> Personas { get; set; } = new();
    public PeritajeTecnicoResponseDto? Peritaje { get; set; }
}
