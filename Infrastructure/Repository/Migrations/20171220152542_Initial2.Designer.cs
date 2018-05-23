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
    [Migration("20171220152542_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("ProjetoId");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoId");

                    b.ToTable("Criterio");
                });

            modelBuilder.Entity("Core.MatrizDeDecisao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlternativaId");

                    b.Property<int?>("CriterioId");

                    b.Property<int?>("ProjetoId");

                    b.Property<string>("Valor")
                        .IsRequired()
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
                        .HasDefaultValue(new DateTime(2017, 12, 20, 13, 25, 40, 910, DateTimeKind.Local));

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

            modelBuilder.Entity("Core.MatrizDeDecisao", b =>
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
