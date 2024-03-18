﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Signature.Infraestructure.Persistence;

namespace Signature.Infraestructure.Migrations
{
    [DbContext(typeof(SignatureContext))]
    [Migration("20230314021806_AddedSignatureDrawingUrl")]
    partial class AddedSignatureDrawingUrl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Signature.Domain.AggregatesModel.SignatureAggregate.SignatureInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CertificateGenerationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CertificateUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DocumentFileExtension")
                        .HasColumnType("longtext");

                    b.Property<string>("DocumentNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("DocumentUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Latitude")
                        .HasColumnType("longtext");

                    b.Property<string>("Longitude")
                        .HasColumnType("longtext");

                    b.Property<string>("PostalCode")
                        .HasColumnType("longtext");

                    b.Property<int>("ProductDatabaseRecordId")
                        .HasColumnType("int");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<string>("SignatureDrawingUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserCellphone")
                        .HasColumnType("longtext");

                    b.Property<string>("UserEmail")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserIdentification")
                        .HasColumnType("longtext");

                    b.Property<string>("UserIpAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserPictureUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("UserUpdate")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("SignatureInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
