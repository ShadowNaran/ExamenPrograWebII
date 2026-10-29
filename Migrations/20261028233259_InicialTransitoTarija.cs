using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiExamen.Migrations
{
    /// <inheritdoc />
    public partial class InicialTransitoTarija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "oficiales",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombres = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellidos = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    grado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero_escalafon = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    cargo = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    unidad = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true),
                    password_hash = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oficiales", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accidentes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_caso = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    fecha_hora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tipo_accidente = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    gravedad = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    distrito = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    zona_barrio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    direccion_referencia = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    latitud = table.Column<decimal>(type: "numeric(10,8)", precision: 10, scale: 8, nullable: true),
                    longitud = table.Column<decimal>(type: "numeric(11,8)", precision: 11, scale: 8, nullable: true),
                    condicion_climatica = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    estado_calzada = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    estado_caso = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    oficial_asignado_id = table.Column<int>(type: "integer", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    juzgado_oinstitucion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    numero_cuaderno_investigacion = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    motivo_pase = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accidentes", x => x.id);
                    table.ForeignKey(
                        name: "FK_accidentes_oficiales_oficial_asignado_id",
                        column: x => x.oficial_asignado_id,
                        principalTable: "oficiales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "peritajes_tecnicos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accidente_id = table.Column<int>(type: "integer", nullable: false),
                    perito_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_peritaje = table.Column<DateOnly>(type: "date", nullable: false),
                    velocidad_estimada_impacto_kmh = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    huella_frenado_metros = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    factor_determinante = table.Column<string>(type: "text", nullable: false),
                    conclusiones = table.Column<string>(type: "text", nullable: false),
                    croquis_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peritajes_tecnicos", x => x.id);
                    table.ForeignKey(
                        name: "FK_peritajes_tecnicos_accidentes_accidente_id",
                        column: x => x.accidente_id,
                        principalTable: "accidentes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_peritajes_tecnicos_oficiales_perito_id",
                        column: x => x.perito_id,
                        principalTable: "oficiales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vehiculos_involucrados",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accidente_id = table.Column<int>(type: "integer", nullable: false),
                    placa = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    marca = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    modelo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tipo_vehiculo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    numero_chasis = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    soat_vigente = table.Column<bool>(type: "boolean", nullable: true),
                    aseguradora = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    descripcion_danios = table.Column<string>(type: "text", nullable: true),
                    remolcado_a = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehiculos_involucrados", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehiculos_involucrados_accidentes_accidente_id",
                        column: x => x.accidente_id,
                        principalTable: "accidentes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personas_involucradas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accidente_id = table.Column<int>(type: "integer", nullable: false),
                    vehiculo_id = table.Column<int>(type: "integer", nullable: true),
                    nombres = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellidos = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ci = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    rol = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    numero_licencia = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    categoria_licencia = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    grado_alcoholemia_gr_l = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true),
                    estado_alcoholemia = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    estado_salud = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    centro_medico_traslado = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    diagnostico_preliminar = table.Column<string>(type: "text", nullable: true),
                    numero_acta_dosaje = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    laboratorio = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas_involucradas", x => x.id);
                    table.ForeignKey(
                        name: "FK_personas_involucradas_accidentes_accidente_id",
                        column: x => x.accidente_id,
                        principalTable: "accidentes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personas_involucradas_vehiculos_involucrados_vehiculo_id",
                        column: x => x.vehiculo_id,
                        principalTable: "vehiculos_involucrados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accidentes_codigo_caso",
                table: "accidentes",
                column: "codigo_caso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accidentes_distrito",
                table: "accidentes",
                column: "distrito");

            migrationBuilder.CreateIndex(
                name: "IX_accidentes_fecha_hora",
                table: "accidentes",
                column: "fecha_hora");

            migrationBuilder.CreateIndex(
                name: "IX_accidentes_oficial_asignado_id",
                table: "accidentes",
                column: "oficial_asignado_id");

            migrationBuilder.CreateIndex(
                name: "IX_oficiales_numero_escalafon",
                table: "oficiales",
                column: "numero_escalafon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_peritajes_tecnicos_accidente_id",
                table: "peritajes_tecnicos",
                column: "accidente_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_peritajes_tecnicos_perito_id",
                table: "peritajes_tecnicos",
                column: "perito_id");

            migrationBuilder.CreateIndex(
                name: "IX_personas_involucradas_accidente_id",
                table: "personas_involucradas",
                column: "accidente_id");

            migrationBuilder.CreateIndex(
                name: "IX_personas_involucradas_vehiculo_id",
                table: "personas_involucradas",
                column: "vehiculo_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehiculos_involucrados_accidente_id",
                table: "vehiculos_involucrados",
                column: "accidente_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "peritajes_tecnicos");

            migrationBuilder.DropTable(
                name: "personas_involucradas");

            migrationBuilder.DropTable(
                name: "vehiculos_involucrados");

            migrationBuilder.DropTable(
                name: "accidentes");

            migrationBuilder.DropTable(
                name: "oficiales");
        }
    }
}
