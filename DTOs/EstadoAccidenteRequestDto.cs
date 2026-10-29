using System.ComponentModel.DataAnnotations;

public class EstadoAccidenteRequestDto
{
    [Required, RegularExpression("^(en_investigacion|conciliado|remitido_fiscalia|cerrado)$")]
    public string EstadoCaso { get; set; } = string.Empty;
    [StringLength(255)]
    public string? JuzgadoOInstitucion { get; set; }
    [StringLength(80)]
    public string? NumeroCuadernoInvestigacion { get; set; }
    public string? MotivoPase { get; set; }
}
