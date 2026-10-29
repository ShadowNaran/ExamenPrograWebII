using System.ComponentModel.DataAnnotations;

public class AccidenteFiltroRequestDto
{
    [StringLength(50)] public string? Distrito { get; set; }
    [RegularExpression("^(solo_danos|con_heridos|fatal)$")] public string? Gravedad { get; set; }
    public DateOnly? FechaDesde { get; set; }
    public DateOnly? FechaHasta { get; set; }
    [Range(1, int.MaxValue)] public int Pagina { get; set; } = 1;
    [Range(1, 100)] public int TamanoPagina { get; set; } = 20;
}
