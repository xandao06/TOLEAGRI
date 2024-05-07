﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TOLEAGRI.Model.Persistence;

#nullable disable

namespace TOLEAGRI.Migrations
{
    [DbContext(typeof(TOLEDbContext))]
    partial class TOLEDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TOLEAGRI.Model.Domain.Estoque", b =>
                {
                    b.Property<int>("EstoqueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstoqueId"));

                    b.Property<string>("CodigoSistema")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataSaida")
                        .HasColumnType("datetime2");

                    b.Property<string>("Locacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadeEntrada")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadeSaida")
                        .HasColumnType("int");

                    b.HasKey("EstoqueId");

                    b.ToTable("Estoques");
                });
#pragma warning restore 612, 618
        }
    }
}
