namespace ApiExamen.Models;

public class Oficial
{
    public int Id { get; set; }

    public string Nombres { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public string Grado { get; set; } = string.Empty;

    public string NumeroEscalafon { get; set; } = string.Empty;

    public string Cargo { get; set; } = string.Empty;

    public string? Unidad { get; set; }

    public bool? Activo { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Accidente> AccidentesAsignados { get; set; } = new List<Accidente>();

    public ICollection<PeritajeTecnico> PeritajesRealizados { get; set; } = new List<PeritajeTecnico>();
}
