using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using api_db.Models;

namespace api_db.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<aerolinea> aerolineas { get; set; }

    public virtual DbSet<aeropuerto> aeropuertos { get; set; }

    public virtual DbSet<avion> avions { get; set; }

    public virtual DbSet<ciudad> ciudads { get; set; }

    public virtual DbSet<correo_electronico> correo_electronicos { get; set; }

    public virtual DbSet<pai> pais { get; set; }

    public virtual DbSet<pasajero> pasajeros { get; set; }

    public virtual DbSet<plaza> plazas { get; set; }

    public virtual DbSet<reserva> reservas { get; set; }

    public virtual DbSet<telefono> telefonos { get; set; }

    public virtual DbSet<vuelo> vuelos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<aerolinea>(entity =>
        {
            entity.HasKey(e => e.id_aerolinea).HasName("PRIMARY");

            entity.ToTable("aerolinea");

            entity.Property(e => e.id_aerolinea).HasMaxLength(3);
            entity.Property(e => e.nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<aeropuerto>(entity =>
        {
            entity.HasKey(e => e.id_aeropuerto).HasName("PRIMARY");

            entity.ToTable("aeropuerto");

            entity.HasIndex(e => e.codigo_ciudad, "codigo_ciudad");

            entity.HasIndex(e => e.codigo_pais, "codigo_pais");

            entity.Property(e => e.id_aeropuerto)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.codigo_ciudad).HasMaxLength(5);
            entity.Property(e => e.codigo_pais).HasMaxLength(3);
            entity.Property(e => e.nombre).HasMaxLength(255);


        });

        modelBuilder.Entity<avion>(entity =>
        {
            entity.HasKey(e => e.id_avion).HasName("PRIMARY");

            entity.ToTable("avion");

            entity.HasIndex(e => e.id_aerolinea, "id_aerolinea");

            entity.HasIndex(e => e.matricula, "matricula").IsUnique();

            entity.Property(e => e.id_avion)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.capacidad_total).HasColumnType("int(11)");
            entity.Property(e => e.id_aerolinea).HasMaxLength(3);
            entity.Property(e => e.matricula).HasMaxLength(20);
            entity.Property(e => e.modelo).HasMaxLength(100);

        });

        modelBuilder.Entity<ciudad>(entity =>
        {
            entity.HasKey(e => e.codigo_ciudad).HasName("PRIMARY");

            entity.ToTable("ciudad");

            entity.HasIndex(e => e.codigo_pais, "codigo_pais");

            entity.Property(e => e.codigo_ciudad).HasMaxLength(5);
            entity.Property(e => e.codigo_pais).HasMaxLength(3);
            entity.Property(e => e.nombre_ciudad).HasMaxLength(255);

        });

        modelBuilder.Entity<correo_electronico>(entity =>
        {
            entity.HasKey(e => e.correo).HasName("PRIMARY");

            entity.ToTable("correo_electronico");

            entity.HasIndex(e => e.id_pasajero, "id_pasajero");

            entity.Property(e => e.id_pasajero).HasColumnType("int(10) unsigned");


        });

        modelBuilder.Entity<pai>(entity =>
        {
            entity.HasKey(e => e.codigo_pais).HasName("PRIMARY");

            entity.Property(e => e.codigo_pais).HasMaxLength(3);
            entity.Property(e => e.nombre_pais).HasMaxLength(255);
        });

        modelBuilder.Entity<pasajero>(entity =>
        {
            entity.HasKey(e => e.id_pasajero).HasName("PRIMARY");

            entity.ToTable("pasajero");

            entity.HasIndex(e => e.codigo_ciudad, "codigo_ciudad");

            entity.HasIndex(e => e.codigo_pais, "codigo_pais");

            entity.HasIndex(e => e.pasaporte, "pasaporte").IsUnique();

            entity.Property(e => e.id_pasajero)
                .ValueGeneratedNever()
                .HasColumnType("int(10) unsigned");
            entity.Property(e => e.codigo_ciudad).HasMaxLength(5);
            entity.Property(e => e.codigo_pais).HasMaxLength(3);
            entity.Property(e => e.pasaporte).HasMaxLength(20);
            entity.Property(e => e.primer_apellido).HasMaxLength(255);
            entity.Property(e => e.primer_nombre).HasMaxLength(255);
            entity.Property(e => e.segundo_apellido).HasMaxLength(255);
            entity.Property(e => e.segundo_nombre).HasMaxLength(255);
            entity.Property(e => e.tercer_nombre).HasMaxLength(255);


        });

        modelBuilder.Entity<plaza>(entity =>
        {
            entity.HasKey(e => new { e.letra_fila, e.numero_plaza })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("plaza");

            entity.Property(e => e.letra_fila).HasMaxLength(4);
            entity.Property(e => e.numero_plaza).HasColumnType("int(11)");
        });

        modelBuilder.Entity<reserva>(entity =>
        {
            entity.HasKey(e => e.id_reserva).HasName("PRIMARY");

            entity.ToTable("reserva");

            entity.HasIndex(e => new { e.letra_fila, e.numero_plaza }, "letra_fila");

            entity.HasIndex(e => e.numero_vuelo, "numero_vuelo");

            entity.Property(e => e.id_reserva)
                .ValueGeneratedNever()
                .HasColumnType("int(10) unsigned");
            entity.Property(e => e.estado)
                .HasDefaultValueSql("'confirmado'")
                .HasColumnType("enum('confirmado','cancelado')");
            entity.Property(e => e.letra_fila).HasMaxLength(4);
            entity.Property(e => e.numero_plaza).HasColumnType("int(11)");
            entity.Property(e => e.numero_vuelo).HasMaxLength(10);


        });

        modelBuilder.Entity<telefono>(entity =>
        {
            entity.HasKey(e => e.numero_telefono).HasName("PRIMARY");

            entity.ToTable("telefono");

            entity.HasIndex(e => e.id_pasajero, "id_pasajero");

            entity.Property(e => e.numero_telefono).HasMaxLength(20);
            entity.Property(e => e.id_pasajero).HasColumnType("int(10) unsigned");


        });

        modelBuilder.Entity<vuelo>(entity =>
        {
            entity.HasKey(e => e.numero_vuelo).HasName("PRIMARY");

            entity.ToTable("vuelo");

            entity.HasIndex(e => e.aeropuerto_destino, "aeropuerto_destino");

            entity.HasIndex(e => e.aeropuerto_origen, "aeropuerto_origen");

            entity.HasIndex(e => e.id_avion, "id_avion");

            entity.Property(e => e.numero_vuelo).HasMaxLength(10);
            entity.Property(e => e.aeropuerto_destino).HasColumnType("int(11)");
            entity.Property(e => e.aeropuerto_origen).HasColumnType("int(11)");
            entity.Property(e => e.hora_llegada).HasColumnType("datetime");
            entity.Property(e => e.hora_salida).HasColumnType("datetime");
            entity.Property(e => e.id_avion).HasColumnType("int(11)");


        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
