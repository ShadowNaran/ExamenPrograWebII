using ApiExamen.Data;
using ApiExamen.Models;
using Microsoft.EntityFrameworkCore;

public class AccidenteService : IAccidenteService
{
    private readonly AppDbContext _context;
    public AccidenteService(AppDbContext context) { _context = context; }

    public async Task<AccidenteResponseDto> CrearAsync(AccidenteRequestDto datos)
    {
        if (datos.FechaHora == default) throw new ArgumentException("La fecha y hora son obligatorias.");
        var codigo = datos.CodigoCaso.Trim();
        if (await _context.Accidentes.AnyAsync(p => p.CodigoCaso == codigo)) throw new InvalidOperationException("El codigo del caso ya existe.");
        if (datos.OficialAsignadoId.HasValue && !await _context.Oficiales.AnyAsync(p => p.Id == datos.OficialAsignadoId && p.Activo == true))
            throw new ArgumentException("El oficial asignado no existe o no esta activo.");
        var fechaUtc = datos.FechaHora.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(datos.FechaHora, DateTimeKind.Utc) : datos.FechaHora.ToUniversalTime();
        var accidente = new Accidente
        {
            CodigoCaso = codigo,
            FechaHora = fechaUtc,
            TipoAccidente = datos.TipoAccidente,
            Gravedad = datos.Gravedad,
            Distrito = datos.Distrito,
            ZonaBarrio = datos.ZonaBarrio,
            DireccionReferencia = datos.DireccionReferencia,
            Latitud = datos.Latitud,
            Longitud = datos.Longitud,
            CondicionClimatica = datos.CondicionClimatica,
            EstadoCalzada = datos.EstadoCalzada,
            OficialAsignadoId = datos.OficialAsignadoId,
            Observaciones = datos.Observaciones,
        };
        _context.Accidentes.Add(accidente);
        await _context.SaveChangesAsync();
        return await BuscarPorIdAsync(accidente.Id);
    }

    public async Task<PaginacionResponseDto<AccidenteResumenResponseDto>> ListarAsync(AccidenteFiltroRequestDto filtros)
    {
        if (filtros.FechaDesde > filtros.FechaHasta) throw new ArgumentException("FechaDesde no puede ser posterior a FechaHasta.");
        IQueryable<Accidente> consulta = _context.Accidentes;
        if (!string.IsNullOrWhiteSpace(filtros.Distrito)) consulta = consulta.Where(p => p.Distrito == filtros.Distrito);
        if (!string.IsNullOrWhiteSpace(filtros.Gravedad)) consulta = consulta.Where(p => p.Gravedad == filtros.Gravedad);
        if (filtros.FechaDesde.HasValue)
        {
            var desde = filtros.FechaDesde.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            consulta = consulta.Where(p => p.FechaHora >= desde);
        }
        if (filtros.FechaHasta.HasValue)
        {
            if (filtros.FechaHasta == DateOnly.MaxValue) throw new ArgumentException("FechaHasta excede el limite permitido.");
            var hasta = filtros.FechaHasta.Value.AddDays(1).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            consulta = consulta.Where(p => p.FechaHora < hasta);
        }
        var salto = ((long)filtros.Pagina - 1) * filtros.TamanoPagina;
        if (salto > int.MaxValue) throw new ArgumentException("La pagina solicitada es demasiado grande.");
        var total = await consulta.CountAsync();
        var items = await consulta.OrderByDescending(p => p.FechaHora).ThenByDescending(p => p.Id)
            .Skip((int)salto).Take(filtros.TamanoPagina)
            .Select(p => new AccidenteResumenResponseDto
            {
                Id = p.Id, CodigoCaso = p.CodigoCaso, FechaHora = p.FechaHora, TipoAccidente = p.TipoAccidente,
                Gravedad = p.Gravedad, Distrito = p.Distrito, ZonaBarrio = p.ZonaBarrio, EstadoCaso = p.EstadoCaso,
                OficialAsignadoId = p.OficialAsignadoId,
                NombreOficialAsignado = p.OficialAsignado == null ? null : p.OficialAsignado.Nombres + " " + p.OficialAsignado.Apellidos,
                CantidadVehiculos = p.Vehiculos.Count, CantidadPersonas = p.Personas.Count
            }).ToListAsync();
        return new PaginacionResponseDto<AccidenteResumenResponseDto> { Items = items, Total = total, Pagina = filtros.Pagina, TamanoPagina = filtros.TamanoPagina };
    }

    public async Task<AccidenteResponseDto> BuscarPorIdAsync(int id)
    {
        var existe = await _context.Accidentes
            .Include(p => p.OficialAsignado).Include(p => p.Vehiculos)
            .Include(p => p.Personas).ThenInclude(p => p.Vehiculo)
            .Include(p => p.Peritaje).ThenInclude(p => p!.Perito)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (existe is null) throw new KeyNotFoundException("El accidente no existe.");
        return new AccidenteResponseDto
        {
            Id = existe.Id,
            CodigoCaso = existe.CodigoCaso,
            FechaHora = existe.FechaHora,
            TipoAccidente = existe.TipoAccidente,
            Gravedad = existe.Gravedad,
            Distrito = existe.Distrito,
            ZonaBarrio = existe.ZonaBarrio,
            DireccionReferencia = existe.DireccionReferencia,
            Latitud = existe.Latitud,
            Longitud = existe.Longitud,
            CondicionClimatica = existe.CondicionClimatica,
            EstadoCalzada = existe.EstadoCalzada,
            EstadoCaso = existe.EstadoCaso,
            OficialAsignadoId = existe.OficialAsignadoId,
            Observaciones = existe.Observaciones,
            JuzgadoOInstitucion = existe.JuzgadoOInstitucion,
            NumeroCuadernoInvestigacion = existe.NumeroCuadernoInvestigacion,
            MotivoPase = existe.MotivoPase,
            CreatedAt = existe.CreatedAt,
            OficialAsignado = existe.OficialAsignado == null ? null : new OficialResponseDto
        {
            Id = existe.OficialAsignado.Id,
            Nombres = existe.OficialAsignado.Nombres,
            Apellidos = existe.OficialAsignado.Apellidos,
            Grado = existe.OficialAsignado.Grado,
            NumeroEscalafon = existe.OficialAsignado.NumeroEscalafon,
            Cargo = existe.OficialAsignado.Cargo,
            Unidad = existe.OficialAsignado.Unidad,
            Activo = existe.OficialAsignado.Activo,
            CreatedAt = existe.OficialAsignado.CreatedAt,
        },
            Vehiculos = existe.Vehiculos.OrderBy(v => v.Id).Select(v => new VehiculoInvolucradoResponseDto
        {
            Id = v.Id,
            AccidenteId = v.AccidenteId,
            Placa = v.Placa,
            Marca = v.Marca,
            Modelo = v.Modelo,
            TipoVehiculo = v.TipoVehiculo,
            Color = v.Color,
            NumeroChasis = v.NumeroChasis,
            SoatVigente = v.SoatVigente,
            Aseguradora = v.Aseguradora,
            DescripcionDanios = v.DescripcionDanios,
            RemolcadoA = v.RemolcadoA,
            CreatedAt = v.CreatedAt,
        }).ToList(),
            Personas = existe.Personas.OrderBy(p => p.Id).Select(p => new PersonaInvolucradaResponseDto
        {
            Id = p.Id,
            AccidenteId = p.AccidenteId,
            VehiculoId = p.VehiculoId,
            Nombres = p.Nombres,
            Apellidos = p.Apellidos,
            Ci = p.Ci,
            Telefono = p.Telefono,
            Rol = p.Rol,
            NumeroLicencia = p.NumeroLicencia,
            CategoriaLicencia = p.CategoriaLicencia,
            GradoAlcoholemiaGral = p.GradoAlcoholemiaGral,
            EstadoAlcoholemia = p.EstadoAlcoholemia,
            EstadoSalud = p.EstadoSalud,
            CentroMedicoTraslado = p.CentroMedicoTraslado,
            DiagnosticoPreliminar = p.DiagnosticoPreliminar,
            NumeroActaDosaje = p.NumeroActaDosaje,
            Laboratorio = p.Laboratorio,
            CreatedAt = p.CreatedAt,
            PlacaVehiculo = p.Vehiculo == null ? null : p.Vehiculo.Placa,
        }).ToList(),
            Peritaje = existe.Peritaje == null ? null : new PeritajeTecnicoResponseDto
        {
            Id = existe.Peritaje.Id,
            AccidenteId = existe.Peritaje.AccidenteId,
            PeritoId = existe.Peritaje.PeritoId,
            FechaPeritaje = existe.Peritaje.FechaPeritaje,
            VelocidadEstimadaImpactoKmh = existe.Peritaje.VelocidadEstimadaImpactoKmh,
            HuellaFrenadoMetros = existe.Peritaje.HuellaFrenadoMetros,
            FactorDeterminante = existe.Peritaje.FactorDeterminante,
            Conclusiones = existe.Peritaje.Conclusiones,
            CroquisUrl = existe.Peritaje.CroquisUrl,
            CreatedAt = existe.Peritaje.CreatedAt,
            NombrePerito = existe.Peritaje.Perito.Nombres + " " + existe.Peritaje.Perito.Apellidos,
        },
        };
    }

    public async Task<AccidenteResponseDto> ActualizarEstadoAsync(int id, EstadoAccidenteRequestDto datos)
    {
        var existe = await _context.Accidentes.FindAsync(id);
        if (existe is null) throw new KeyNotFoundException("El accidente no existe.");
        existe.EstadoCaso = datos.EstadoCaso;
        if (datos.JuzgadoOInstitucion is not null) existe.JuzgadoOInstitucion = datos.JuzgadoOInstitucion;
        if (datos.NumeroCuadernoInvestigacion is not null) existe.NumeroCuadernoInvestigacion = datos.NumeroCuadernoInvestigacion;
        if (datos.MotivoPase is not null) existe.MotivoPase = datos.MotivoPase;
        await _context.SaveChangesAsync();
        return await BuscarPorIdAsync(id);
    }
}
