using System.ComponentModel.DataAnnotations;

public class OficialRequestDto
{
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(100)]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(50)]
    public string Grado { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(30)]
    public string NumeroEscalafon { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(80)]
    public string Cargo { get; set; } = string.Empty;

    [StringLength(80)]
    public string? Unidad { get; set; }

    public bool? Activo { get; set; } = true;

}
