namespace ApiExamen.Models;

public class VehiculoInvolucrado
{
    public int Id { get; set; }

    public int AccidenteId { get; set; }

    public string Placa { get; set; } = string.Empty;

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public string? TipoVehiculo { get; set; }

    public string? Color { get; set; }

    public string? NumeroChasis { get; set; }

    public bool? SoatVigente { get; set; }

    public string? Aseguradora { get; set; }

    public string? DescripcionDanios { get; set; }

    public string? RemolcadoA { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Accidente Accidente { get; set; } = null!;

    public ICollection<PersonaInvolucrada> Personas { get; set; } = new List<PersonaInvolucrada>();
}
