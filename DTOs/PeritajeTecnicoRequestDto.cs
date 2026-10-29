using System.ComponentModel.DataAnnotations;

public class PeritajeTecnicoRequestDto
{
    [Range(1, int.MaxValue)]
    public int AccidenteId { get; set; }

    [Range(1, int.MaxValue)]
    public int PeritoId { get; set; }

    public DateOnly FechaPeritaje { get; set; }

    [Range(typeof(decimal), "0", "999.99", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal? VelocidadEstimadaImpactoKmh { get; set; }

    [Range(typeof(decimal), "0", "999.99", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal? HuellaFrenadoMetros { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string FactorDeterminante { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string Conclusiones { get; set; } = string.Empty;

    [StringLength(255)]
    [Url]
    public string? CroquisUrl { get; set; }
}
