using ApiExamen.Data;
using ApiExamen.Models;
using Microsoft.EntityFrameworkCore;

public class OficialService : IOficialService
{
    private readonly AppDbContext _context;
    public OficialService(AppDbContext context) { _context = context; }

    public async Task<OficialResponseDto> CrearOficialAsync(OficialRequestDto datos)
    {
        var numero = datos.NumeroEscalafon.Trim();
        if (await _context.Oficiales.AnyAsync(p => p.NumeroEscalafon == numero))
            throw new InvalidOperationException("El número de escalafón ya está registrado.");
        var oficial = new Oficial
        {
            Nombres = datos.Nombres, Apellidos = datos.Apellidos, Grado = datos.Grado,
            NumeroEscalafon = numero, Cargo = datos.Cargo, Unidad = datos.Unidad, Activo = datos.Activo ?? true
        };
        _context.Oficiales.Add(oficial);
        await _context.SaveChangesAsync();
        return new OficialResponseDto
        {
            Id = oficial.Id, Nombres = oficial.Nombres, Apellidos = oficial.Apellidos, Grado = oficial.Grado,
            NumeroEscalafon = oficial.NumeroEscalafon, Cargo = oficial.Cargo, Unidad = oficial.Unidad,
            Activo = oficial.Activo, CreatedAt = oficial.CreatedAt
        };
    }
}
