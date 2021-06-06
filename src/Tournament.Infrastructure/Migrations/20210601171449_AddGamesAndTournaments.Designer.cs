﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Tournament.Infrastructure.Migrations
{
    [DbContext(typeof(TournamentContext))]
    [Migration("20210601171449_AddGamesAndTournaments")]
    partial class AddGamesAndTournaments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Tournament.Data.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Player1Id")
                        .HasColumnType("integer");

                    b.Property<int>("Player2Id")
                        .HasColumnType("integer");

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Player1Id");

                    b.HasIndex("Player2Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Tournament.Data.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("Tournament.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int?>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tournament.Data.Entities.Game", b =>
                {
                    b.HasOne("Tournament.Data.Entities.User", "Player1")
                        .WithMany("Player1Games")
                        .HasForeignKey("Player1Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Tournament.Data.Entities.User", "Player2")
                        .WithMany("Player2Games")
                        .HasForeignKey("Player2Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Tournament.Data.Entities.Tournament", "Tournament")
                        .WithMany("Games")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player1");

                    b.Navigation("Player2");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("Tournament.Data.Entities.User", b =>
                {
                    b.HasOne("Tournament.Data.Entities.Game", null)
                        .WithMany("Players")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("Tournament.Data.Entities.Game", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("Tournament.Data.Entities.Tournament", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("Tournament.Data.Entities.User", b =>
                {
                    b.Navigation("Player1Games");

                    b.Navigation("Player2Games");
                });
#pragma warning restore 612, 618
        }
    }
}
