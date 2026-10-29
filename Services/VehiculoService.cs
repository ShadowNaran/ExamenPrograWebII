using ApiExamen.Data;
using ApiExamen.Models;
using Microsoft.EntityFrameworkCore;

public class VehiculoService : IVehiculoService
{
    private readonly AppDbContext _context;
    public VehiculoService(AppDbContext context) { _context = context; }

    public async Task<VehiculoInvolucradoResponseDto> CrearAsync(VehiculoInvolucradoRequestDto datos)
    {
        if (!await _context.Accidentes.AnyAsync(p => p.Id == datos.AccidenteId))
            throw new KeyNotFoundException("El accidente no existe.");
        var registro = new VehiculoInvolucrado
        {
            AccidenteId = datos.AccidenteId,
            Placa = datos.Placa,
            Marca = datos.Marca,
            Modelo = datos.Modelo,
            TipoVehiculo = datos.TipoVehiculo,
            Color = datos.Color,
            NumeroChasis = datos.NumeroChasis,
            SoatVigente = datos.SoatVigente,
            Aseguradora = datos.Aseguradora,
            DescripcionDanios = datos.DescripcionDanios,
            RemolcadoA = datos.RemolcadoA,
        };
        _context.VehiculosInvolucrados.Add(registro);
        await _context.SaveChangesAsync();
        return await BuscarPorIdAsync(registro.Id);
    }

    private async Task<VehiculoInvolucradoResponseDto> BuscarPorIdAsync(int id)
    {
        var existe = await _context.VehiculosInvolucrados.FirstOrDefaultAsync(p => p.Id == id);
        if (existe is null) throw new KeyNotFoundException("El registro no existe.");
        return new VehiculoInvolucradoResponseDto
        {
            Id = existe.Id,
            AccidenteId = existe.AccidenteId,
            Placa = existe.Placa,
            Marca = existe.Marca,
            Modelo = existe.Modelo,
            TipoVehiculo = existe.TipoVehiculo,
            Color = existe.Color,
            NumeroChasis = existe.NumeroChasis,
            SoatVigente = existe.SoatVigente,
            Aseguradora = existe.Aseguradora,
            DescripcionDanios = existe.DescripcionDanios,
            RemolcadoA = existe.RemolcadoA,
            CreatedAt = existe.CreatedAt,
        };
    }

    public async Task<List<VehiculoInvolucradoResponseDto>> ListarPorAccidenteAsync(int id)
    {
        if (!await _context.Accidentes.AnyAsync(p => p.Id == id)) throw new KeyNotFoundException("El accidente no existe.");
        return await _context.VehiculosInvolucrados.Where(p => p.AccidenteId == id).OrderBy(p => p.Id)
            .Select(existe => new VehiculoInvolucradoResponseDto
        {
            Id = existe.Id,
            AccidenteId = existe.AccidenteId,
            Placa = existe.Placa,
            Marca = existe.Marca,
            Modelo = existe.Modelo,
            TipoVehiculo = existe.TipoVehiculo,
            Color = existe.Color,
            NumeroChasis = existe.NumeroChasis,
            SoatVigente = existe.SoatVigente,
            Aseguradora = existe.Aseguradora,
            DescripcionDanios = existe.DescripcionDanios,
            RemolcadoA = existe.RemolcadoA,
            CreatedAt = existe.CreatedAt,
        }).ToListAsync();
    }
}
