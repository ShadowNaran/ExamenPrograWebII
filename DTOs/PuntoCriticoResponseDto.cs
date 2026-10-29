public class PuntoCriticoResponseDto
{
    public string Distrito { get; set; } = string.Empty;
    public string ZonaBarrio { get; set; } = string.Empty;
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }
    public int CantidadAccidentes { get; set; }
    public int CantidadConHeridos { get; set; }
    public int CantidadFatales { get; set; }
}
