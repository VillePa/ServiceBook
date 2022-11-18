using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public partial class DbHuoltokirjaContext : DbContext
{
    public DbHuoltokirjaContext()
    {
    }

    public DbHuoltokirjaContext(DbContextOptions<DbHuoltokirjaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditointi> Auditointis { get; set; }

    public virtual DbSet<Auditointipohja> Auditointipohjas { get; set; }

    public virtual DbSet<Kayttaja> Kayttajas { get; set; }

    public virtual DbSet<Kohde> Kohdes { get; set; }

    public virtual DbSet<Kohderyhma> Kohderyhmas { get; set; }

    public virtual DbSet<Liite> Liites { get; set; }

    public virtual DbSet<Tarkastu> Tarkastus { get; set; }

    public virtual DbSet<Vaatimu> Vaatimus { get; set; }

    public virtual DbSet<Vaatimuspohja> Vaatimuspohjas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:huoltokirjaserver.database.windows.net,1433;Initial Catalog=DbHuoltokirja;Persist Security Info=False;User ID=groupB;Password=RyhmaB@22!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditointi>(entity =>
        {
            entity.HasKey(e => new { e.Idauditointi, e.Idkohde, e.Idkayttaja }).HasName("PK_auditointi_idauditointi");

            entity.ToTable("auditointi", "huoltokirja");

            entity.HasIndex(e => e.Idkayttaja, "fk_auditointi_kayttaja1_idx");

            entity.HasIndex(e => e.Idkohde, "fk_auditointi_kohde1_idx");

            entity.Property(e => e.Idauditointi)
                .ValueGeneratedOnAdd()
                .HasColumnName("idauditointi");
            entity.Property(e => e.Idkohde).HasColumnName("idkohde");
            entity.Property(e => e.Idkayttaja).HasColumnName("idkayttaja");
            entity.Property(e => e.Lopputulos).HasColumnName("lopputulos");
            entity.Property(e => e.Luotu)
                .HasPrecision(0)
                .HasColumnName("luotu");
            entity.Property(e => e.Selite)
                .HasMaxLength(45)
                .HasColumnName("selite");

            entity.HasOne(d => d.IdkayttajaNavigation).WithMany(p => p.Auditointis)
                .HasForeignKey(d => d.Idkayttaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auditointi$fk_auditointi_kayttaja1");

            entity.HasOne(d => d.IdkohdeNavigation).WithMany(p => p.Auditointis)
                .HasPrincipalKey(p => p.Idkohde)
                .HasForeignKey(d => d.Idkohde)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auditointi$fk_auditointi_kohde1");
        });

        modelBuilder.Entity<Auditointipohja>(entity =>
        {
            entity.HasKey(e => new { e.Idauditointipohja, e.Idkayttaja, e.Idkohderyhma }).HasName("PK_auditointipohja_idauditointipohja");

            entity.ToTable("auditointipohja", "huoltokirja");

            entity.HasIndex(e => e.Idkayttaja, "fk_auditointipohja_kayttaja1_idx");

            entity.HasIndex(e => e.Idkohderyhma, "fk_auditointipohja_kohderyhma1_idx");

            entity.Property(e => e.Idauditointipohja)
                .ValueGeneratedOnAdd()
                .HasColumnName("idauditointipohja");
            entity.Property(e => e.Idkayttaja).HasColumnName("idkayttaja");
            entity.Property(e => e.Idkohderyhma).HasColumnName("idkohderyhma");
            entity.Property(e => e.Luontiaika)
                .HasPrecision(0)
                .HasColumnName("luontiaika");
            entity.Property(e => e.Selite)
                .HasMaxLength(45)
                .HasColumnName("selite");

            entity.HasOne(d => d.IdkayttajaNavigation).WithMany(p => p.Auditointipohjas)
                .HasForeignKey(d => d.Idkayttaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auditointipohja$fk_auditointipohja_kayttaja1");

            entity.HasOne(d => d.IdkohderyhmaNavigation).WithMany(p => p.Auditointipohjas)
                .HasForeignKey(d => d.Idkohderyhma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auditointipohja$fk_auditointipohja_kohderyhma1");
        });

        modelBuilder.Entity<Kayttaja>(entity =>
        {
            entity.HasKey(e => e.Idkayttaja).HasName("PK_kayttaja_idkayttaja");

            entity.ToTable("kayttaja", "huoltokirja");

            entity.HasIndex(e => e.Idkayttaja, "kayttaja$idkayttaja_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Kayttajatunnus, "kayttaja$kayttajatunnus_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Salasana, "kayttaja$salasana_UNIQUE").IsUnique();

            entity.Property(e => e.Idkayttaja).HasColumnName("idkayttaja");
            entity.Property(e => e.Kayttajatunnus)
                .HasMaxLength(45)
                .HasColumnName("kayttajatunnus");
            entity.Property(e => e.Luotu)
                .HasPrecision(0)
                .HasColumnName("luotu");
            entity.Property(e => e.Nimi)
                .HasMaxLength(45)
                .HasColumnName("nimi");
            entity.Property(e => e.Poistettu).HasColumnName("poistettu");
            entity.Property(e => e.Rooli)
                .HasMaxLength(10)
                .HasColumnName("rooli");
            entity.Property(e => e.Salasana)
                .HasMaxLength(45)
                .HasColumnName("salasana");
            entity.Property(e => e.SalasanaSalt)
                .HasMaxLength(100)
                .HasColumnName("salasanaSalt");
            entity.Property(e => e.ViimeisinKirjautuminen)
                .HasPrecision(0)
                .HasColumnName("viimeisin_kirjautuminen");
        });

        modelBuilder.Entity<Kohde>(entity =>
        {
            entity.HasKey(e => new { e.Idkohde, e.Idkayttaja, e.Idkohderyhma }).HasName("PK_kohde_idkohde");

            entity.ToTable("kohde", "huoltokirja");

            entity.HasIndex(e => e.Idkayttaja, "fk_kohde_kayttaja_idx");

            entity.HasIndex(e => e.Idkohderyhma, "fk_kohde_kohderyhma1_idx");

            entity.HasIndex(e => e.Idkohde, "kohde$idkohde_UNIQUE").IsUnique();

            entity.Property(e => e.Idkohde)
                .ValueGeneratedOnAdd()
                .HasColumnName("idkohde");
            entity.Property(e => e.Idkayttaja).HasColumnName("idkayttaja");
            entity.Property(e => e.Idkohderyhma).HasColumnName("idkohderyhma");
            entity.Property(e => e.Kuvaus)
                .HasMaxLength(45)
                .HasColumnName("kuvaus");
            entity.Property(e => e.Luotu)
                .HasPrecision(0)
                .HasColumnName("luotu");
            entity.Property(e => e.Malli)
                .HasMaxLength(45)
                .HasColumnName("malli");
            entity.Property(e => e.Nimi)
                .HasMaxLength(45)
                .HasColumnName("nimi");
            entity.Property(e => e.Sijainti)
                .HasMaxLength(45)
                .HasColumnName("sijainti");
            entity.Property(e => e.Tila)
                .HasMaxLength(45)
                .HasColumnName("tila");
            entity.Property(e => e.Tunnus)
                .HasMaxLength(45)
                .HasColumnName("tunnus");
            entity.Property(e => e.Tyyppi)
                .HasMaxLength(45)
                .HasColumnName("tyyppi");

            entity.HasOne(d => d.IdkayttajaNavigation).WithMany(p => p.Kohdes)
                .HasForeignKey(d => d.Idkayttaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kohde$fk_kohde_kayttaja");

            entity.HasOne(d => d.IdkohderyhmaNavigation).WithMany(p => p.Kohdes)
                .HasForeignKey(d => d.Idkohderyhma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kohde$fk_kohde_kohderyhma1");
        });

        modelBuilder.Entity<Kohderyhma>(entity =>
        {
            entity.HasKey(e => e.Idkohderyhma).HasName("PK_kohderyhma_idkohderyhma");

            entity.ToTable("kohderyhma", "huoltokirja");

            entity.Property(e => e.Idkohderyhma).HasColumnName("idkohderyhma");
            entity.Property(e => e.Nimi)
                .HasMaxLength(45)
                .HasColumnName("nimi");
        });

        modelBuilder.Entity<Liite>(entity =>
        {
            entity.HasKey(e => new { e.Idliite, e.Idtarkastus }).HasName("PK_liite_idliite");

            entity.ToTable("liite", "huoltokirja");

            entity.HasIndex(e => e.Idtarkastus, "fk_liite_tarkastus1_idx");

            entity.Property(e => e.Idliite)
                .ValueGeneratedOnAdd()
                .HasColumnName("idliite");
            entity.Property(e => e.Idtarkastus).HasColumnName("idtarkastus");
            entity.Property(e => e.Sijainti)
                .HasMaxLength(100)
                .HasColumnName("sijainti");
        });

        modelBuilder.Entity<Tarkastu>(entity =>
        {
            entity.HasKey(e => new { e.Idtarkastus, e.Idkayttaja, e.Idkohde }).HasName("PK_tarkastus_idtarkastus");

            entity.ToTable("tarkastus", "huoltokirja");

            entity.HasIndex(e => e.Idkayttaja, "fk_tarkastus_kayttaja1_idx");

            entity.HasIndex(e => e.Idkohde, "fk_tarkastus_kohde1_idx");

            entity.Property(e => e.Idtarkastus)
                .ValueGeneratedOnAdd()
                .HasColumnName("idtarkastus");
            entity.Property(e => e.Idkayttaja).HasColumnName("idkayttaja");
            entity.Property(e => e.Idkohde).HasColumnName("idkohde");
            entity.Property(e => e.Aikaleima)
                .HasPrecision(0)
                .HasColumnName("aikaleima");
            entity.Property(e => e.Havainnot)
                .HasMaxLength(100)
                .HasColumnName("havainnot");
            entity.Property(e => e.Syy)
                .HasMaxLength(45)
                .HasColumnName("syy");
            entity.Property(e => e.TilanMuutos).HasColumnName("tilan_muutos");

            entity.HasOne(d => d.IdkayttajaNavigation).WithMany(p => p.Tarkastus)
                .HasForeignKey(d => d.Idkayttaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tarkastus$fk_tarkastus_kayttaja1");

            entity.HasOne(d => d.IdkohdeNavigation).WithMany(p => p.Tarkastus)
                .HasPrincipalKey(p => p.Idkohde)
                .HasForeignKey(d => d.Idkohde)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tarkastus$fk_tarkastus_kohde1");
        });

        modelBuilder.Entity<Vaatimu>(entity =>
        {
            entity.HasKey(e => new { e.Idvaatimus, e.Idauditointi }).HasName("PK_vaatimus_idvaatimus");

            entity.ToTable("vaatimus", "huoltokirja");

            entity.HasIndex(e => e.Idauditointi, "fk_vaatimus_auditointi1_idx");

            entity.Property(e => e.Idvaatimus)
                .ValueGeneratedOnAdd()
                .HasColumnName("idvaatimus");
            entity.Property(e => e.Idauditointi).HasColumnName("idauditointi");
            entity.Property(e => e.Kuvaus)
                .HasMaxLength(45)
                .HasColumnName("kuvaus");
            entity.Property(e => e.Pakollisuus)
                .HasMaxLength(45)
                .HasColumnName("pakollisuus");
            entity.Property(e => e.Taytetty).HasColumnName("taytetty");
        });

        modelBuilder.Entity<Vaatimuspohja>(entity =>
        {
            entity.HasKey(e => new { e.Idvaatimuspohja, e.Idauditointipohja }).HasName("PK_vaatimuspohja_idvaatimuspohja");

            entity.ToTable("vaatimuspohja", "huoltokirja");

            entity.HasIndex(e => e.Idauditointipohja, "fk_vaatimus_auditointipohja1_idx");

            entity.Property(e => e.Idvaatimuspohja)
                .ValueGeneratedOnAdd()
                .HasColumnName("idvaatimuspohja");
            entity.Property(e => e.Idauditointipohja).HasColumnName("idauditointipohja");
            entity.Property(e => e.Kuvaus)
                .HasMaxLength(45)
                .HasColumnName("kuvaus");
            entity.Property(e => e.Pakollisuus)
                .HasMaxLength(45)
                .HasColumnName("pakollisuus");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
