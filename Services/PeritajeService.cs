using ApiExamen.Data;
using ApiExamen.Models;
using Microsoft.EntityFrameworkCore;

public class PeritajeService : IPeritajeService
{
    private readonly AppDbContext _context;

    public PeritajeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PeritajeTecnicoResponseDto> CrearAsync(PeritajeTecnicoRequestDto datos)
    {
        var accidenteExiste = await _context.Accidentes
            .AnyAsync(accidente => accidente.Id == datos.AccidenteId);

        if (!accidenteExiste)
            throw new KeyNotFoundException("El accidente no existe.");

        if (datos.FechaPeritaje == default)
            throw new ArgumentException("La fecha del peritaje es obligatoria.");

        var perito = await _context.Oficiales
            .FirstOrDefaultAsync(oficial => oficial.Id == datos.PeritoId && oficial.Activo == true);

        if (perito is null)
            throw new ArgumentException("El perito no existe o no está activo.");

        var peritajeExiste = await _context.PeritajesTecnicos
            .AnyAsync(peritaje => peritaje.AccidenteId == datos.AccidenteId);

        if (peritajeExiste)
            throw new InvalidOperationException("El accidente ya tiene un peritaje.");

        var peritajeNuevo = new PeritajeTecnico
        {
            AccidenteId = datos.AccidenteId,
            PeritoId = datos.PeritoId,
            FechaPeritaje = datos.FechaPeritaje,
            VelocidadEstimadaImpactoKmh = datos.VelocidadEstimadaImpactoKmh,
            HuellaFrenadoMetros = datos.HuellaFrenadoMetros,
            FactorDeterminante = datos.FactorDeterminante,
            Conclusiones = datos.Conclusiones,
            CroquisUrl = datos.CroquisUrl
        };

        _context.PeritajesTecnicos.Add(peritajeNuevo);
        await _context.SaveChangesAsync();

        return new PeritajeTecnicoResponseDto
        {
            Id = peritajeNuevo.Id,
            AccidenteId = peritajeNuevo.AccidenteId,
            PeritoId = peritajeNuevo.PeritoId,
            FechaPeritaje = peritajeNuevo.FechaPeritaje,
            VelocidadEstimadaImpactoKmh = peritajeNuevo.VelocidadEstimadaImpactoKmh,
            HuellaFrenadoMetros = peritajeNuevo.HuellaFrenadoMetros,
            FactorDeterminante = peritajeNuevo.FactorDeterminante,
            Conclusiones = peritajeNuevo.Conclusiones,
            CroquisUrl = peritajeNuevo.CroquisUrl,
            CreatedAt = peritajeNuevo.CreatedAt,
            NombrePerito = perito.Nombres + " " + perito.Apellidos
        };
    }
}
