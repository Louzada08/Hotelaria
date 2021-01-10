﻿// <auto-generated />
using System;
using LZMotel.Clientes.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LZMotel.Clientes.API.Migrations
{
    [DbContext(typeof(ClienteContext))]
    [Migration("20201228014722_Inicio")]
    partial class Inicio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("LZMotel.Clientes.API.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Excluido")
                        .HasColumnType("boolean");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Turno")
                        .IsRequired()
                        .HasColumnType("char(02)");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("LZMotel.Clientes.API.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(70)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(70)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("char(10)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("LZMotel.Clientes.API.Models.Cliente", b =>
                {
                    b.OwnsOne("LZMotel.Core.DomainObjects.Cpf", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("ClienteId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasColumnName("Cpf")
                                .HasColumnType("varchar(11)")
                                .HasMaxLength(11);

                            b1.HasKey("ClienteId");

                            b1.ToTable("Cliente");

                            b1.WithOwner()
                                .HasForeignKey("ClienteId");
                        });

                    b.OwnsOne("LZMotel.Core.DomainObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("ClienteId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Endereco")
                                .IsRequired()
                                .HasColumnName("Email")
                                .HasColumnType("varchar(254)");

                            b1.HasKey("ClienteId");

                            b1.ToTable("Cliente");

                            b1.WithOwner()
                                .HasForeignKey("ClienteId");
                        });
                });

            modelBuilder.Entity("LZMotel.Clientes.API.Models.Endereco", b =>
                {
                    b.HasOne("LZMotel.Clientes.API.Models.Cliente", "Cliente")
                        .WithOne("Endereco")
                        .HasForeignKey("LZMotel.Clientes.API.Models.Endereco", "ClienteId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
