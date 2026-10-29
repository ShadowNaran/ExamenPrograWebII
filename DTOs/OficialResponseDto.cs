public class OficialResponseDto
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Grado { get; set; } = string.Empty;
    public string NumeroEscalafon { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string? Unidad { get; set; }
    public bool? Activo { get; set; } = true;
    public DateTime CreatedAt { get; set; }
}
