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

    public virtual DbSet<test_rep> test_reps { get; set; }

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

            entity.HasOne(d => d.codigo_ciudadNavigation).WithMany(p => p.aeropuertos)
                .HasForeignKey(d => d.codigo_ciudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aeropuerto_ibfk_2");

            entity.HasOne(d => d.codigo_paisNavigation).WithMany(p => p.aeropuertos)
                .HasForeignKey(d => d.codigo_pais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aeropuerto_ibfk_1");
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

            entity.HasOne(d => d.id_aerolineaNavigation).WithMany(p => p.avions)
                .HasForeignKey(d => d.id_aerolinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("avion_ibfk_1");
        });

        modelBuilder.Entity<ciudad>(entity =>
        {
            entity.HasKey(e => e.codigo_ciudad).HasName("PRIMARY");

            entity.ToTable("ciudad");

            entity.HasIndex(e => e.codigo_pais, "codigo_pais");

            entity.Property(e => e.codigo_ciudad).HasMaxLength(5);
            entity.Property(e => e.codigo_pais).HasMaxLength(3);
            entity.Property(e => e.nombre_ciudad).HasMaxLength(255);

            entity.HasOne(d => d.codigo_paisNavigation).WithMany(p => p.ciudads)
                .HasForeignKey(d => d.codigo_pais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ciudad_ibfk_1");
        });

        modelBuilder.Entity<correo_electronico>(entity =>
        {
            entity.HasKey(e => e.correo).HasName("PRIMARY");

            entity.ToTable("correo_electronico");

            entity.HasIndex(e => e.id_pasajero, "id_pasajero");

            entity.Property(e => e.id_pasajero).HasColumnType("int(10) unsigned");

            entity.HasOne(d => d.id_pasajeroNavigation).WithMany(p => p.correo_electronicos)
                .HasForeignKey(d => d.id_pasajero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("correo_electronico_ibfk_1");
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

            entity.HasOne(d => d.codigo_ciudadNavigation).WithMany(p => p.pasajeros)
                .HasForeignKey(d => d.codigo_ciudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pasajero_ibfk_2");

            entity.HasOne(d => d.codigo_paisNavigation).WithMany(p => p.pasajeros)
                .HasForeignKey(d => d.codigo_pais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pasajero_ibfk_1");
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

            entity.HasOne(d => d.numero_vueloNavigation).WithMany(p => p.reservas)
                .HasForeignKey(d => d.numero_vuelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reserva_ibfk_2");

            entity.HasOne(d => d.plaza).WithMany(p => p.reservas)
                .HasForeignKey(d => new { d.letra_fila, d.numero_plaza })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reserva_ibfk_1");

            entity.HasMany(d => d.id_pasajeros).WithMany(p => p.id_reservas)
                .UsingEntity<Dictionary<string, object>>(
                    "reserva_pasajero",
                    r => r.HasOne<pasajero>().WithMany()
                        .HasForeignKey("id_pasajero")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("reserva_pasajero_ibfk_2"),
                    l => l.HasOne<reserva>().WithMany()
                        .HasForeignKey("id_reserva")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("reserva_pasajero_ibfk_1"),
                    j =>
                    {
                        j.HasKey("id_reserva", "id_pasajero")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("reserva_pasajero");
                        j.HasIndex(new[] { "id_pasajero" }, "id_pasajero");
                        j.IndexerProperty<uint>("id_reserva").HasColumnType("int(10) unsigned");
                        j.IndexerProperty<uint>("id_pasajero").HasColumnType("int(10) unsigned");
                    });
        });

        modelBuilder.Entity<telefono>(entity =>
        {
            entity.HasKey(e => e.numero_telefono).HasName("PRIMARY");

            entity.ToTable("telefono");

            entity.HasIndex(e => e.id_pasajero, "id_pasajero");

            entity.Property(e => e.numero_telefono).HasMaxLength(20);
            entity.Property(e => e.id_pasajero).HasColumnType("int(10) unsigned");

            entity.HasOne(d => d.id_pasajeroNavigation).WithMany(p => p.telefonos)
                .HasForeignKey(d => d.id_pasajero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("telefono_ibfk_1");
        });

        modelBuilder.Entity<test_rep>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("test_rep");

            entity.Property(e => e.id).HasColumnType("int(11)");
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

            entity.HasOne(d => d.aeropuerto_destinoNavigation).WithMany(p => p.vueloaeropuerto_destinoNavigations)
                .HasForeignKey(d => d.aeropuerto_destino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vuelo_ibfk_2");

            entity.HasOne(d => d.aeropuerto_origenNavigation).WithMany(p => p.vueloaeropuerto_origenNavigations)
                .HasForeignKey(d => d.aeropuerto_origen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vuelo_ibfk_1");

            entity.HasOne(d => d.id_avionNavigation).WithMany(p => p.vuelos)
                .HasForeignKey(d => d.id_avion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vuelo_ibfk_3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
