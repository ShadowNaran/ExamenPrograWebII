public class AccidenteResumenResponseDto
{
    public int Id { get; set; }
    public string CodigoCaso { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string TipoAccidente { get; set; } = string.Empty;
    public string Gravedad { get; set; } = string.Empty;
    public string Distrito { get; set; } = string.Empty;
    public string ZonaBarrio { get; set; } = string.Empty;
    public string EstadoCaso { get; set; } = string.Empty;
    public int? OficialAsignadoId { get; set; }
    public string? NombreOficialAsignado { get; set; }
    public int CantidadVehiculos { get; set; }
    public int CantidadPersonas { get; set; }
}
