using System.ComponentModel.DataAnnotations;

public class PersonaInvolucradaRequestDto
{
    [Range(1, int.MaxValue)]
    public int AccidenteId { get; set; }

    [Range(1, int.MaxValue)]
    public int? VehiculoId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(100)]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(25)]
    public string Ci { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(30)]
    public string? Rol { get; set; }

    [StringLength(30)]
    public string? NumeroLicencia { get; set; }

    [StringLength(10)]
    public string? CategoriaLicencia { get; set; }

    [Range(typeof(decimal), "0", "99.99", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal? GradoAlcoholemiaGral { get; set; }

    [StringLength(20)]
    public string? EstadoAlcoholemia { get; set; }

    [StringLength(30)]
    public string? EstadoSalud { get; set; }

    [StringLength(120)]
    public string? CentroMedicoTraslado { get; set; }

    public string? DiagnosticoPreliminar { get; set; }
}
