﻿// <auto-generated />
using DAL.Entities.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20230327112205_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.Models.Cuisine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cuisines");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Italian"
                        },
                        new
                        {
                            Id = 2,
                            Name = "French"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Japanese"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CuisineId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CuisineId");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CuisineId = 1,
                            Description = "A classic Italian pizza",
                            Name = "Pizza Hut"
                        },
                        new
                        {
                            Id = 2,
                            CuisineId = 2,
                            Description = "A classic French bistro",
                            Name = "French Bistro"
                        },
                        new
                        {
                            Id = 3,
                            CuisineId = 3,
                            Description = "A trendy Japanese sushi bar",
                            Name = "Sushi Bar"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Models.Restaurant", b =>
                {
                    b.HasOne("DAL.Entities.Models.Cuisine", "Cuisine")
                        .WithMany()
                        .HasForeignKey("CuisineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuisine");
                });
#pragma warning restore 612, 618
        }
    }
}
