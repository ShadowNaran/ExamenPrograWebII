using ApiExamen.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var conexionLocal = builder.Configuration.GetConnectionString("TransitoConnection")
    ?? throw new InvalidOperationException("Configura ConnectionStrings:TransitoConnection en appsettings.json.");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conexionLocal));

builder.Services.AddScoped<IOficialService, OficialService>();
builder.Services.AddScoped<IAccidenteService, AccidenteService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IPeritajeService, PeritajeService>();
builder.Services.AddScoped<IEstadisticaService, EstadisticaService>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DatosDePrueba.CargarAsync(context);
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();

public partial class Program { }
