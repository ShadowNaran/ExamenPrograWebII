using ApiExamen.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace ApiExamen.Data;

public static class DatosDePrueba
{
    public static async Task CargarAsync(AppDbContext context)
    {
        if (await context.Accidentes.AnyAsync())
            return;

        var faker = new Faker("es");
        var oficiales = new List<Oficial>();

        for (var i = 1; i <= 5; i++)
        {
            oficiales.Add(new Oficial
            {
                Nombres = faker.Name.FirstName(),
                Apellidos = faker.Name.LastName(),
                Grado = faker.PickRandom("Sargento", "Teniente", "Cabo"),
                NumeroEscalafon = "DEMO-" + Guid.NewGuid().ToString("N").Substring(0, 12),
                Cargo = faker.PickRandom("Oficial de tránsito", "Investigador"),
                Unidad = "Tránsito Tarija",
                Activo = true
            });
        }

        context.Oficiales.AddRange(oficiales);
        await context.SaveChangesAsync();

        var tiposAccidente = new[] { "colision", "choque", "atropello", "vuelco" };
        var gravedades = new[] { "solo_danos", "con_heridos", "fatal" };
        var distritos = new[] { "1", "2", "3", "4" };
        var roles = new[] { "conductor", "pasajero", "peaton" };

        for (var i = 1; i <= 20; i++)
        {
            var accidente = new Accidente
            {
                CodigoCaso = $"DEMO-2026-{i:000}",
                FechaHora = DateTime.UtcNow.AddDays(-faker.Random.Int(1, 365)),
                TipoAccidente = faker.PickRandom(tiposAccidente),
                Gravedad = faker.PickRandom(gravedades),
                Distrito = faker.PickRandom(distritos),
                ZonaBarrio = faker.Address.StreetName(),
                DireccionReferencia = $"{faker.Address.StreetAddress()}, Tarija",
                Latitud = Math.Round((decimal)faker.Random.Double(-21.56, -21.48), 8),
                Longitud = Math.Round((decimal)faker.Random.Double(-64.78, -64.70), 8),
                CondicionClimatica = faker.PickRandom("despejado", "nublado", "lluvia"),
                EstadoCalzada = faker.PickRandom("seca", "mojada", "con_baches"),
                OficialAsignado = faker.PickRandom(oficiales),
                Observaciones = faker.Lorem.Sentence()
            };

            var cantidadVehiculos = faker.Random.Int(1, 3);
            var vehiculos = new List<VehiculoInvolucrado>();

            for (var numero = 1; numero <= cantidadVehiculos; numero++)
            {
                var vehiculo = new VehiculoInvolucrado
                {
                    Placa = $"TJA-{i:000}-{numero:000}",
                    Marca = faker.Vehicle.Manufacturer(),
                    Modelo = faker.Vehicle.Model(),
                    TipoVehiculo = faker.PickRandom("automovil", "motocicleta", "camioneta"),
                    Color = faker.Commerce.Color(),
                    SoatVigente = faker.Random.Bool(),
                    DescripcionDanios = faker.Lorem.Sentence()
                };

                vehiculos.Add(vehiculo);
                accidente.Vehiculos.Add(vehiculo);
            }

            var cantidadPersonas = faker.Random.Int(2, 5);

            for (var numero = 1; numero <= cantidadPersonas; numero++)
            {
                var rol = faker.PickRandom(roles);
                VehiculoInvolucrado? vehiculo = null;

                if (rol != "peaton")
                    vehiculo = faker.PickRandom(vehiculos);

                accidente.Personas.Add(new PersonaInvolucrada
                {
                    Nombres = faker.Name.FirstName(),
                    Apellidos = faker.Name.LastName(),
                    Ci = faker.Random.Int(1000000, 99999999).ToString(),
                    Telefono = faker.Phone.PhoneNumber("7#######"),
                    Rol = rol,
                    NumeroLicencia = rol == "conductor" ? faker.Random.Int(10000, 99999).ToString() : null,
                    CategoriaLicencia = rol == "conductor" ? faker.PickRandom("A", "B", "C") : null,
                    GradoAlcoholemiaGral = Math.Round((decimal)faker.Random.Double(0, 1.5), 2),
                    EstadoAlcoholemia = faker.PickRandom("pendiente", "negativo", "positivo"),
                    EstadoSalud = faker.PickRandom("estable", "herido", "grave"),
                    Vehiculo = vehiculo
                });
            }

            if (i % 3 == 0)
            {
                var perito = faker.PickRandom(oficiales);
                accidente.Peritaje = new PeritajeTecnico
                {
                    Perito = perito,
                    FechaPeritaje = DateOnly.FromDateTime(accidente.FechaHora),
                    VelocidadEstimadaImpactoKmh = Math.Round((decimal)faker.Random.Double(10, 100), 2),
                    HuellaFrenadoMetros = Math.Round((decimal)faker.Random.Double(1, 30), 2),
                    FactorDeterminante = faker.PickRandom("exceso_velocidad", "falta_de_atencion", "clima"),
                    Conclusiones = faker.Lorem.Sentence()
                };
            }

            context.Accidentes.Add(accidente);
        }

        await context.SaveChangesAsync();
    }
}
