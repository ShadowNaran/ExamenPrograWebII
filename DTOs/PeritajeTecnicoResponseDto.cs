public class PeritajeTecnicoResponseDto
{
    public int Id { get; set; }
    public int AccidenteId { get; set; }
    public int PeritoId { get; set; }
    public DateOnly FechaPeritaje { get; set; }
    public decimal? VelocidadEstimadaImpactoKmh { get; set; }
    public decimal? HuellaFrenadoMetros { get; set; }
    public string FactorDeterminante { get; set; } = string.Empty;
    public string Conclusiones { get; set; } = string.Empty;
    public string? CroquisUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public string NombrePerito { get; set; } = string.Empty;
}
