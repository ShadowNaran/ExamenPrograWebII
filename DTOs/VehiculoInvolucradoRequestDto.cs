using System.ComponentModel.DataAnnotations;

public class VehiculoInvolucradoRequestDto
{
    [Range(1, int.MaxValue)]
    public int AccidenteId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [StringLength(15)]
    public string Placa { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Marca { get; set; }

    [StringLength(50)]
    public string? Modelo { get; set; }

    [StringLength(50)]
    public string? TipoVehiculo { get; set; }

    [StringLength(30)]
    public string? Color { get; set; }

    [StringLength(50)]
    public string? NumeroChasis { get; set; }

    public bool? SoatVigente { get; set; }

    [StringLength(80)]
    public string? Aseguradora { get; set; }

    public string? DescripcionDanios { get; set; }

    [StringLength(120)]
    public string? RemolcadoA { get; set; }
}
