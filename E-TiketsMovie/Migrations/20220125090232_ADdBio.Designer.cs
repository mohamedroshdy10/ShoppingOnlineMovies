﻿// <auto-generated />
using System;
using E_TiketsMovie.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E_TiketsMovie.Migrations
{
    [DbContext(typeof(EcommercdbContext))]
    [Migration("20220125090232_ADdBio")]
    partial class ADdBio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Actor_Movies", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActorID")
                        .HasColumnType("int");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ActorID");

                    b.HasIndex("MovieID");

                    b.ToTable("Actor_Movies", "guide");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Actors", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("profile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Actors", "guide");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Cinema", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Cinema", "guide");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieCatogery")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nescripation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProdusserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StarDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CinemaId");

                    b.HasIndex("ProdusserId");

                    b.ToTable("Movie", "guide");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Produsser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("profile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Produsser", "guide");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Actor_Movies", b =>
                {
                    b.HasOne("E_TiketsMovie.Models.Tables.Actors", "Actors")
                        .WithMany("Actor_Movies")
                        .HasForeignKey("ActorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_TiketsMovie.Models.Tables.Movie", "Movie")
                        .WithMany("Actor_Movies")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actors");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Movie", b =>
                {
                    b.HasOne("E_TiketsMovie.Models.Tables.Cinema", "Cinema")
                        .WithMany("Movies")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_TiketsMovie.Models.Tables.Produsser", "Produsser")
                        .WithMany("Movies")
                        .HasForeignKey("ProdusserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");

                    b.Navigation("Produsser");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Actors", b =>
                {
                    b.Navigation("Actor_Movies");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Cinema", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Movie", b =>
                {
                    b.Navigation("Actor_Movies");
                });

            modelBuilder.Entity("E_TiketsMovie.Models.Tables.Produsser", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}