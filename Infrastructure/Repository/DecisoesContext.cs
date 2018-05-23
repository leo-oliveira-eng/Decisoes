using System;
using Core;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DecisoesContext : DbContext
    {
        public DecisoesContext(DbContextOptions<DecisoesContext> options) : base(options)
        {
        }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }
        public DbSet<Criterio> Criterios { get; set; }
        public DbSet<ItemMatrizDeDecisao> MatrizDeDecisoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurarProjeto(modelBuilder);
            ConfigurarAlternativa(modelBuilder);
            ConfiguracaoCriterio(modelBuilder);
            ConfiguracaoMatrizDeDecisao(modelBuilder);
        }

        private static void ConfiguracaoMatrizDeDecisao(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ItemMatrizDeDecisao>()
                .ToTable("MatrizDeDecisao");
            modelBuilder
                .Entity<ItemMatrizDeDecisao>()
                .Property(p => p.Valor)
                .HasMaxLength(50);
        }

        private static void ConfiguracaoCriterio(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Criterio>()
                .ToTable("Criterio");
            modelBuilder
                .Entity<Criterio>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);
        }

        private static void ConfigurarAlternativa(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Alternativa>()
                .ToTable("Alternativa");
            modelBuilder
                .Entity<Alternativa>()
                .Property(p => p.Nome)
                .HasMaxLength(50)
                .IsRequired();
        }

        private static void ConfigurarProjeto(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Projeto>()
                .ToTable("Projeto");
            modelBuilder
                .Entity<Projeto>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder
                .Entity<Projeto>()
                .Property(p => p.Descricao)
                .HasMaxLength(255);
            modelBuilder
                .Entity<Projeto>()
                .Property(p => p.DataCadastro)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);
        }

    }
}
