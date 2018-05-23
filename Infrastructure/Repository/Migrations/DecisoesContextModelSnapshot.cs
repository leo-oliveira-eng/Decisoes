﻿// <auto-generated />
using Core.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Repository;
using System;

namespace Repository.Migrations
{
    [DbContext(typeof(DecisoesContext))]
    partial class DecisoesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Core.Alternativa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("ProjetoId");

                    b.Property<double>("Score");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoId");

                    b.ToTable("Alternativa");
                });

            modelBuilder.Entity("Core.Criterio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("Peso");

                    b.Property<int?>("ProjetoId");

                    b.Property<int>("TipoDeCriterio");

                    b.Property<int>("TipoDeDados");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoId");

                    b.ToTable("Criterio");
                });

            modelBuilder.Entity("Core.ItemMatrizDeDecisao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlternativaId");

                    b.Property<int?>("CriterioId");

                    b.Property<int?>("ProjetoId");

                    b.Property<string>("Valor")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AlternativaId");

                    b.HasIndex("CriterioId");

                    b.HasIndex("ProjetoId");

                    b.ToTable("MatrizDeDecisao");
                });

            modelBuilder.Entity("Core.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2018, 2, 17, 20, 2, 21, 805, DateTimeKind.Local));

                    b.Property<string>("Descricao")
                        .HasMaxLength(255);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Projeto");
                });

            modelBuilder.Entity("Core.Alternativa", b =>
                {
                    b.HasOne("Core.Projeto", "Projeto")
                        .WithMany()
                        .HasForeignKey("ProjetoId");
                });

            modelBuilder.Entity("Core.Criterio", b =>
                {
                    b.HasOne("Core.Projeto", "Projeto")
                        .WithMany()
                        .HasForeignKey("ProjetoId");
                });

            modelBuilder.Entity("Core.ItemMatrizDeDecisao", b =>
                {
                    b.HasOne("Core.Alternativa", "Alternativa")
                        .WithMany()
                        .HasForeignKey("AlternativaId");

                    b.HasOne("Core.Criterio", "Criterio")
                        .WithMany()
                        .HasForeignKey("CriterioId");

                    b.HasOne("Core.Projeto", "Projeto")
                        .WithMany()
                        .HasForeignKey("ProjetoId");
                });
#pragma warning restore 612, 618
        }
    }
}
