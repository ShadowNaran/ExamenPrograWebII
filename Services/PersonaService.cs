using ApiExamen.Data;
using ApiExamen.Models;
using Microsoft.EntityFrameworkCore;

public class PersonaService : IPersonaService
{
    private readonly AppDbContext _context;
    public PersonaService(AppDbContext context) { _context = context; }

    public async Task<PersonaInvolucradaResponseDto> CrearAsync(PersonaInvolucradaRequestDto datos)
    {
        if (!await _context.Accidentes.AnyAsync(p => p.Id == datos.AccidenteId))
            throw new KeyNotFoundException("El accidente no existe.");
        if (datos.VehiculoId.HasValue)
        {
            var vehiculo = await _context.VehiculosInvolucrados.FindAsync(datos.VehiculoId.Value);
            if (vehiculo is null) throw new KeyNotFoundException("El vehiculo no existe.");
            if (vehiculo.AccidenteId != datos.AccidenteId) throw new ArgumentException("El vehiculo pertenece a otro accidente.");
        }
        var registro = new PersonaInvolucrada
        {
            AccidenteId = datos.AccidenteId,
            VehiculoId = datos.VehiculoId,
            Nombres = datos.Nombres,
            Apellidos = datos.Apellidos,
            Ci = datos.Ci,
            Telefono = datos.Telefono,
            Rol = datos.Rol,
            NumeroLicencia = datos.NumeroLicencia,
            CategoriaLicencia = datos.CategoriaLicencia,
            GradoAlcoholemiaGral = datos.GradoAlcoholemiaGral,
            EstadoAlcoholemia = datos.EstadoAlcoholemia,
            EstadoSalud = datos.EstadoSalud,
            CentroMedicoTraslado = datos.CentroMedicoTraslado,
            DiagnosticoPreliminar = datos.DiagnosticoPreliminar,
        };
        _context.PersonasInvolucradas.Add(registro);
        await _context.SaveChangesAsync();
        return await BuscarPorIdAsync(registro.Id);
    }

    private async Task<PersonaInvolucradaResponseDto> BuscarPorIdAsync(int id)
    {
        var existe = await _context.PersonasInvolucradas.Include(p => p.Vehiculo).FirstOrDefaultAsync(p => p.Id == id);
        if (existe is null) throw new KeyNotFoundException("El registro no existe.");
        return new PersonaInvolucradaResponseDto
        {
            Id = existe.Id,
            AccidenteId = existe.AccidenteId,
            VehiculoId = existe.VehiculoId,
            Nombres = existe.Nombres,
            Apellidos = existe.Apellidos,
            Ci = existe.Ci,
            Telefono = existe.Telefono,
            Rol = existe.Rol,
            NumeroLicencia = existe.NumeroLicencia,
            CategoriaLicencia = existe.CategoriaLicencia,
            GradoAlcoholemiaGral = existe.GradoAlcoholemiaGral,
            EstadoAlcoholemia = existe.EstadoAlcoholemia,
            EstadoSalud = existe.EstadoSalud,
            CentroMedicoTraslado = existe.CentroMedicoTraslado,
            DiagnosticoPreliminar = existe.DiagnosticoPreliminar,
            NumeroActaDosaje = existe.NumeroActaDosaje,
            Laboratorio = existe.Laboratorio,
            CreatedAt = existe.CreatedAt,
            PlacaVehiculo = existe.Vehiculo == null ? null : existe.Vehiculo.Placa,
        };
    }

    public async Task<PersonaInvolucradaResponseDto> ActualizarAlcoholemiaAsync(int id, AlcoholemiaRequestDto datos)
    {
        var existe = await _context.PersonasInvolucradas.FindAsync(id);
        if (existe is null) throw new KeyNotFoundException("La persona no existe.");
        existe.GradoAlcoholemiaGral = datos.GradoAlcoholemiaGral;
        existe.EstadoAlcoholemia = datos.EstadoAlcoholemia;
        if (datos.NumeroActaDosaje is not null) existe.NumeroActaDosaje = datos.NumeroActaDosaje;
        if (datos.Laboratorio is not null) existe.Laboratorio = datos.Laboratorio;
        await _context.SaveChangesAsync();
        return await BuscarPorIdAsync(id);
    }
}
