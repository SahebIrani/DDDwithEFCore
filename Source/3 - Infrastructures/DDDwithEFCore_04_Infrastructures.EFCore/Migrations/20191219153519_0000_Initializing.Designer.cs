﻿// <auto-generated />
using System;
using DDDwithEFCore_04_Infrastructures.EFCore.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191219153519_0000_Initializing")]
    partial class _0000_Initializing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DDDwithEFCore_03_ApplicationCore.DomainModels.People.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("DDDwithEFCore_03_ApplicationCore.DomainModels.Peoples");
                });
#pragma warning restore 612, 618
        }
    }
}