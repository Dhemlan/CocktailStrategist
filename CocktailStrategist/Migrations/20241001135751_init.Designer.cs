﻿// <auto-generated />
using System;
using CocktailStrategist.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CocktailStrategist.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241001135751_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CocktailStrategist.Data.Drink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("CocktailStrategist.Data.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("DrinkIngredient", b =>
                {
                    b.Property<Guid>("DrinksId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IngredientsId")
                        .HasColumnType("uuid");

                    b.HasKey("DrinksId", "IngredientsId");

                    b.HasIndex("IngredientsId");

                    b.ToTable("DrinkIngredient");
                });

            modelBuilder.Entity("DrinkIngredient", b =>
                {
                    b.HasOne("CocktailStrategist.Data.Drink", null)
                        .WithMany()
                        .HasForeignKey("DrinksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailStrategist.Data.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
