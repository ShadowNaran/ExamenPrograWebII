using ApiExamen.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExamen.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Oficial> Oficiales => Set<Oficial>();
    public DbSet<Accidente> Accidentes => Set<Accidente>();
    public DbSet<VehiculoInvolucrado> VehiculosInvolucrados => Set<VehiculoInvolucrado>();
    public DbSet<PersonaInvolucrada> PersonasInvolucradas => Set<PersonaInvolucrada>();
    public DbSet<PeritajeTecnico> PeritajesTecnicos => Set<PeritajeTecnico>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Oficial>().ToTable("oficiales");
        modelBuilder.Entity<Accidente>().ToTable("accidentes");
        modelBuilder.Entity<VehiculoInvolucrado>().ToTable("vehiculos_involucrados");
        modelBuilder.Entity<PersonaInvolucrada>().ToTable("personas_involucradas");
        modelBuilder.Entity<PeritajeTecnico>().ToTable("peritajes_tecnicos");

        modelBuilder.Entity<Oficial>().HasIndex(p => p.NumeroEscalafon).IsUnique();
        modelBuilder.Entity<Accidente>().HasIndex(p => p.CodigoCaso).IsUnique();
        modelBuilder.Entity<Accidente>().HasIndex(p => p.FechaHora);
        modelBuilder.Entity<Accidente>().HasIndex(p => p.Distrito);

        modelBuilder.Entity<Accidente>().HasOne(p => p.OficialAsignado)
            .WithMany(p => p.AccidentesAsignados).HasForeignKey(p => p.OficialAsignadoId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<PeritajeTecnico>().HasOne(p => p.Perito)
            .WithMany(p => p.PeritajesRealizados).HasForeignKey(p => p.PeritoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PeritajeTecnico>().HasOne(p => p.Accidente)
            .WithOne(p => p.Peritaje).HasForeignKey<PeritajeTecnico>(p => p.AccidenteId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<PeritajeTecnico>().HasIndex(p => p.AccidenteId).IsUnique();

        modelBuilder.Entity<VehiculoInvolucrado>().HasOne(p => p.Accidente)
            .WithMany(p => p.Vehiculos).HasForeignKey(p => p.AccidenteId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<PersonaInvolucrada>().HasOne(p => p.Accidente)
            .WithMany(p => p.Personas).HasForeignKey(p => p.AccidenteId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<PersonaInvolucrada>().HasOne(p => p.Vehiculo)
            .WithMany(p => p.Personas).HasForeignKey(p => p.VehiculoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Accidente>().Property(p => p.Latitud).HasPrecision(10, 8);
        modelBuilder.Entity<Accidente>().Property(p => p.Longitud).HasPrecision(11, 8);
        modelBuilder.Entity<PeritajeTecnico>().Property(p => p.VelocidadEstimadaImpactoKmh).HasPrecision(5, 2);
        modelBuilder.Entity<PeritajeTecnico>().Property(p => p.HuellaFrenadoMetros).HasPrecision(5, 2);
        modelBuilder.Entity<PersonaInvolucrada>().Property(p => p.GradoAlcoholemiaGral).HasPrecision(4, 2);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
            foreach (var property in entity.GetProperties())
                property.SetColumnName(System.Text.RegularExpressions.Regex.Replace(property.Name, "([a-z0-9])([A-Z])", "$1_$2").ToLowerInvariant());
        modelBuilder.Entity<PersonaInvolucrada>().Property(p => p.GradoAlcoholemiaGral)
            .HasColumnName("grado_alcoholemia_gr_l");
    }
}
