using ApiExamen.Data;
using Microsoft.EntityFrameworkCore;

public class EstadisticaService : IEstadisticaService
{
    private readonly AppDbContext _context;

    public EstadisticaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PuntoCriticoResponseDto>> ObtenerPuntosCriticosAsync(int anio)
    {
        if (anio < 1 || anio > 9998)
            throw new ArgumentException("El año debe estar entre 1 y 9998.");

        var accidentes = await _context.Accidentes
            .Where(accidente => accidente.FechaHora.Year == anio
                && accidente.Latitud.HasValue
                && accidente.Longitud.HasValue)
            .ToListAsync();

        var puntos = new List<PuntoCriticoResponseDto>();

        foreach (var accidente in accidentes)
        {
            var punto = puntos.FirstOrDefault(p =>
                p.Distrito == accidente.Distrito
                && p.ZonaBarrio == accidente.ZonaBarrio
                && p.Latitud == accidente.Latitud!.Value
                && p.Longitud == accidente.Longitud!.Value);

            if (punto is null)
            {
                punto = new PuntoCriticoResponseDto
                {
                    Distrito = accidente.Distrito,
                    ZonaBarrio = accidente.ZonaBarrio,
                    Latitud = accidente.Latitud!.Value,
                    Longitud = accidente.Longitud!.Value
                };

                puntos.Add(punto);
            }

            punto.CantidadAccidentes++;

            if (accidente.Gravedad == "con_heridos")
                punto.CantidadConHeridos++;

            if (accidente.Gravedad == "fatal")
                punto.CantidadFatales++;
        }

        return puntos
            .OrderByDescending(punto => punto.CantidadAccidentes)
            .ToList();
    }
}
