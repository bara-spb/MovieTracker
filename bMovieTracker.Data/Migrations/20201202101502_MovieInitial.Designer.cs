﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bMovieTracker.Data;

namespace bMovieTracker.Data.Migrations
{
    [DbContext(typeof(MovieTrackerDbContext))]
    [Migration("20201202101502_MovieInitial")]
    partial class MovieInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("bMovieTracker.Domain.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Genre");

                    b.Property<string>("Rate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int?>("Year");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Genre = "Drama",
                            Rate = "FiveStars",
                            Title = "The Green Mile",
                            Year = 1999
                        },
                        new
                        {
                            Id = 2,
                            Genre = "Fantasy",
                            Rate = "FiveStars",
                            Title = "Interstellar",
                            Year = 2014
                        },
                        new
                        {
                            Id = 3,
                            Genre = "Animation",
                            Rate = "FiveStars",
                            Title = "The Lion King",
                            Year = 1994
                        },
                        new
                        {
                            Id = 4,
                            Genre = "Fantasy",
                            Rate = "FourStars",
                            Title = "The Matrix",
                            Year = 1999
                        },
                        new
                        {
                            Id = 5,
                            Genre = "Action",
                            Rate = "ThreeStars",
                            Title = "Terminator Salvation",
                            Year = 2009
                        });
                });
#pragma warning restore 612, 618
        }
    }
}