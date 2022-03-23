using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend_Recyclo_dotnet.Models
{
    public partial class dct8nq053j6k6dContext : DbContext
    {
        public dct8nq053j6k6dContext()
        {
        }

        public dct8nq053j6k6dContext(DbContextOptions<dct8nq053j6k6dContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbDenuncium> TbDenuncia { get; set; } = null!;
        public virtual DbSet<TbDiscarteIlegal> TbDiscarteIlegals { get; set; } = null!;
        public virtual DbSet<TbEmpresa> TbEmpresas { get; set; } = null!;
        public virtual DbSet<TbPontoColetum> TbPontoColeta { get; set; } = null!;
        public virtual DbSet<TbUsuario> TbUsuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=ec2-54-197-100-79.compute-1.amazonaws.com;Database=dct8nq053j6k6d;Username=zozpujqxeubpha;Password=a61992ba0d8f928b5f77c6b69943eb716ff3de0bc8188675a76c84727a44fa9f");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbDenuncium>(entity =>
            {
                entity.HasKey(e => e.CdDenuncia)
                    .HasName("pk_denuncia");

                entity.ToTable("tb_denuncia");

                entity.Property(e => e.CdDenuncia)
                    .ValueGeneratedNever()
                    .HasColumnName("cd_denuncia");

                entity.Property(e => e.DsComentario)
                    .HasMaxLength(500)
                    .HasColumnName("ds_comentario");

                entity.Property(e => e.DtDenuncia).HasColumnName("dt_denuncia");

                entity.Property(e => e.FkCdDiscarteIlegal).HasColumnName("fk_cd_discarte_ilegal");

                entity.Property(e => e.FkCdUsuario).HasColumnName("fk_cd_usuario");

                entity.Property(e => e.NmLogradouro)
                    .HasMaxLength(150)
                    .HasColumnName("nm_logradouro");

                entity.HasOne(d => d.FkCdDiscarteIlegalNavigation)
                    .WithMany(p => p.TbDenuncia)
                    .HasForeignKey(d => d.FkCdDiscarteIlegal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cd_discarte_ilegal");

                entity.HasOne(d => d.FkCdUsuarioNavigation)
                    .WithMany(p => p.TbDenuncia)
                    .HasForeignKey(d => d.FkCdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cd_usuario");
            });

            modelBuilder.Entity<TbDiscarteIlegal>(entity =>
            {
                entity.HasKey(e => e.CdDiscarte)
                    .HasName("pk_discarte");

                entity.ToTable("tb_discarte_ilegal");

                entity.Property(e => e.CdDiscarte)
                    .ValueGeneratedNever()
                    .HasColumnName("cd_discarte");

                entity.Property(e => e.CdLocalizacao)
                    .HasMaxLength(50)
                    .HasColumnName("cd_localizacao");
            });

            modelBuilder.Entity<TbEmpresa>(entity =>
            {
                entity.HasKey(e => e.CdEmpresa)
                    .HasName("tb_empresa_pkey");

                entity.ToTable("tb_empresa");

                entity.Property(e => e.CdEmpresa)
                    .ValueGeneratedNever()
                    .HasColumnName("cd_empresa");

                entity.Property(e => e.CdCnpj)
                    .HasColumnType("character varying")
                    .HasColumnName("cd_cnpj");

                entity.Property(e => e.CdSenhaEmpresa)
                    .HasMaxLength(255)
                    .HasColumnName("cd_senha_empresa");

                entity.Property(e => e.CdTelefone)
                    .HasColumnType("character varying")
                    .HasColumnName("cd_telefone");

                entity.Property(e => e.NmEmail)
                    .HasMaxLength(255)
                    .HasColumnName("nm_email");

                entity.Property(e => e.NmEmpresa)
                    .HasMaxLength(45)
                    .HasColumnName("nm_empresa");
            });

            modelBuilder.Entity<TbPontoColetum>(entity =>
            {
                entity.HasKey(e => e.CdPontoColeta)
                    .HasName("tb_ponto_coleta_pkey");

                entity.ToTable("tb_ponto_coleta");

                entity.Property(e => e.CdPontoColeta)
                    .ValueGeneratedNever()
                    .HasColumnName("cd_ponto_coleta");

                entity.Property(e => e.CdLatitudePonto)
                    .HasColumnType("character varying")
                    .HasColumnName("cd_latitude_ponto");

                entity.Property(e => e.CdLongitudePonto)
                    .HasColumnType("character varying")
                    .HasColumnName("cd_longitude_ponto");

                entity.Property(e => e.FkCdEmpresa).HasColumnName("fk_cd_empresa");

                entity.Property(e => e.NmLogradouro)
                    .HasMaxLength(500)
                    .HasColumnName("nm_logradouro");

                entity.Property(e => e.NmPonto)
                    .HasMaxLength(500)
                    .HasColumnName("nm_ponto");

                entity.HasOne(d => d.FkCdEmpresaNavigation)
                    .WithMany(p => p.TbPontoColeta)
                    .HasForeignKey(d => d.FkCdEmpresa)
                    .HasConstraintName("fk_cd_empresa");
            });

            modelBuilder.Entity<TbUsuario>(entity =>
            {
                entity.HasKey(e => e.CdUsuario)
                    .HasName("tb_usuario_pkey");

                entity.ToTable("tb_usuario");

                entity.HasIndex(e => e.DsEmail, "tb_usuario_ds_email_key")
                    .IsUnique();

                entity.Property(e => e.CdUsuario)
                    .ValueGeneratedNever()
                    .HasColumnName("cd_usuario");

                entity.Property(e => e.CdCpf)
                    .HasColumnType("character varying")
                    .HasColumnName("cd_cpf");

                entity.Property(e => e.CdSenha)
                    .HasMaxLength(500)
                    .HasColumnName("cd_senha");

                entity.Property(e => e.CdTelefone)
                    .HasColumnType("character varying")
                    .HasColumnName("cd_telefone");

                entity.Property(e => e.DsEmail)
                    .HasMaxLength(255)
                    .HasColumnName("ds_email");

                entity.Property(e => e.NmUsuario)
                    .HasMaxLength(50)
                    .HasColumnName("nm_usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
