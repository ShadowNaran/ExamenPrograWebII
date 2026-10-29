using System.ComponentModel.DataAnnotations;

public class AlcoholemiaRequestDto
{
    [Required, Range(typeof(decimal), "0", "99.99", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal? GradoAlcoholemiaGral { get; set; }
    [Required, StringLength(20)]
    public string EstadoAlcoholemia { get; set; } = string.Empty;
    [StringLength(80)]
    public string? NumeroActaDosaje { get; set; }
    [StringLength(150)]
    public string? Laboratorio { get; set; }
}
