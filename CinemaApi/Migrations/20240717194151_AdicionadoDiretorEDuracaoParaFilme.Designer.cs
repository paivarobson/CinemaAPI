﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaApi.Migrations
{
    [DbContext(typeof(CinemaContext))]
    [Migration("20240717194151_AdicionadoDiretorEDuracaoParaFilme")]
    partial class AdicionadoDiretorEDuracaoParaFilme
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AnoLancamento")
                        .HasColumnType("int");

                    b.Property<string>("Diretor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Duracao")
                        .HasColumnType("int");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Filmes");
                });

            modelBuilder.Entity("Sala", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacidade")
                        .HasColumnType("int");

                    b.Property<string>("NomeSala")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Salas");
                });
#pragma warning restore 612, 618
        }
    }
}