using System.ComponentModel.DataAnnotations;

public class AccidenteRequestDto
{
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(30)]
    public string CodigoCaso { get; set; } = string.Empty;

    public DateTime FechaHora { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(60)]
    public string TipoAccidente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(30)]
    [RegularExpression("^(solo_danos|con_heridos|fatal)$")]
    public string Gravedad { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(50)]
    public string Distrito { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(100)]
    public string ZonaBarrio { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(255)]
    public string DireccionReferencia { get; set; } = string.Empty;

    [Range(typeof(decimal), "-90", "90", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal? Latitud { get; set; }

    [Range(typeof(decimal), "-180", "180", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal? Longitud { get; set; }

    [StringLength(40)]
    public string? CondicionClimatica { get; set; }

    [StringLength(40)]
    public string? EstadoCalzada { get; set; }

    [Range(1, int.MaxValue)]
    public int? OficialAsignadoId { get; set; }

    public string? Observaciones { get; set; }
}
