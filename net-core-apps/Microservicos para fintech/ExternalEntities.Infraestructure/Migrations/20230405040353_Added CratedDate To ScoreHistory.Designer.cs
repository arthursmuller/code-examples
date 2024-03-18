﻿// <auto-generated />
using System;
using ExternalEntities.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExternalEntities.Infraestructure.Migrations
{
    [DbContext(typeof(ExternalEntitiesContext))]
    [Migration("20230405040353_Added CratedDate To ScoreHistory")]
    partial class AddedCratedDateToScoreHistory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.BusinessAggregate.Business", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Cnpj")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserUpdate")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.BusinessScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PaymentCommitmentScore")
                        .HasColumnType("int");

                    b.Property<int?>("ProfileScore")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Segment")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserUpdate")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessScore");
                });

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.UserScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<int?>("ApplicationUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserUpdate")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserScore");
                });

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.UserAggregate.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Cpf")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal?>("TotalCurrentDebitAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("TotalCurrentDebitDelinquentAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("TotalDelinquencyAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("TotalFinancialOperations")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserUpdate")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cpf = "02981603078",
                            CreatedDate = new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(177),
                            UpdateDate = new DateTime(2023, 4, 5, 1, 3, 52, 361, DateTimeKind.Unspecified).AddTicks(2175)
                        },
                        new
                        {
                            Id = 2,
                            Cpf = "77735936044",
                            CreatedDate = new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1279),
                            UpdateDate = new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1251)
                        },
                        new
                        {
                            Id = 3,
                            Cpf = "84509848072",
                            CreatedDate = new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1292),
                            UpdateDate = new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1287)
                        },
                        new
                        {
                            Id = 4,
                            Cpf = "71096627051",
                            CreatedDate = new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1304),
                            UpdateDate = new DateTime(2023, 4, 5, 1, 3, 52, 370, DateTimeKind.Unspecified).AddTicks(1299)
                        });
                });

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.BusinessAggregate.Business", b =>
                {
                    b.OwnsMany("ExternalEntities.Domain.AggregatesModel.BusinessAggregate.BusinessOwner", "Owners", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<int>("_businessId")
                                .HasColumnType("int");

                            b1.Property<int>("_userId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("_businessId");

                            b1.HasIndex("_userId");

                            b1.ToTable("BusinessOwners");

                            b1.WithOwner()
                                .HasForeignKey("_businessId");

                            b1.HasOne("ExternalEntities.Domain.AggregatesModel.UserAggregate.ApplicationUser", "User")
                                .WithMany()
                                .HasForeignKey("_userId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("User");
                        });

                    b.Navigation("Owners");
                });

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.BusinessScore", b =>
                {
                    b.HasOne("ExternalEntities.Domain.AggregatesModel.BusinessAggregate.Business", null)
                        .WithMany("Scores")
                        .HasForeignKey("BusinessId");
                });

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.UserScore", b =>
                {
                    b.HasOne("ExternalEntities.Domain.AggregatesModel.UserAggregate.ApplicationUser", null)
                        .WithMany("Scores")
                        .HasForeignKey("ApplicationUserId");

                    b.OwnsMany("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ScoreFilterHistory", "History", b1 =>
                        {
                            b1.Property<int>("UserScoreId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<DateTime>("createdDate")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("description")
                                .HasColumnType("longtext");

                            b1.HasKey("UserScoreId", "Id");

                            b1.ToTable("ScoreFilterHistory");

                            b1.WithOwner()
                                .HasForeignKey("UserScoreId");
                        });

                    b.Navigation("History");
                });

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.UserAggregate.ApplicationUser", b =>
                {
                    b.OwnsMany("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodDataRecord", "QuodDataRecords", b1 =>
                        {
                            b1.Property<int>("ApplicationUserId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.HasKey("ApplicationUserId", "Id");

                            b1.ToTable("QuodDataRecords");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationUserId");

                            b1.OwnsOne("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodData", "ExternalQuodData", b2 =>
                                {
                                    b2.Property<int>("ExternalQuodDataRecordApplicationUserId")
                                        .HasColumnType("int");

                                    b2.Property<int>("ExternalQuodDataRecordId")
                                        .HasColumnType("int");

                                    b2.Property<DateTime>("BirthDate")
                                        .HasColumnType("datetime(6)");

                                    b2.Property<string>("Email")
                                        .HasColumnType("longtext");

                                    b2.Property<string>("MobilePhoneNumber")
                                        .HasColumnType("longtext");

                                    b2.Property<string>("PhoneNumber")
                                        .HasColumnType("longtext");

                                    b2.HasKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");

                                    b2.ToTable("QuodDataRecords");

                                    b2.WithOwner()
                                        .HasForeignKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");

                                    b2.OwnsMany("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodAddress", "Addresses", b3 =>
                                        {
                                            b3.Property<int>("ExternalQuodDataRecordApplicationUserId")
                                                .HasColumnType("int");

                                            b3.Property<int>("ExternalQuodDataRecordId")
                                                .HasColumnType("int");

                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("int");

                                            b3.Property<string>("City")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("Complement")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("Neighborhood")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("Number")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("PostalCode")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("State")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("Street")
                                                .HasColumnType("longtext");

                                            b3.HasKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId", "Id");

                                            b3.ToTable("QuodDataAddresses");

                                            b3.WithOwner()
                                                .HasForeignKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");
                                        });

                                    b2.OwnsOne("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodAddress", "CurrentAddress", b3 =>
                                        {
                                            b3.Property<int>("ExternalQuodDataRecordApplicationUserId")
                                                .HasColumnType("int");

                                            b3.Property<int>("ExternalQuodDataRecordId")
                                                .HasColumnType("int");

                                            b3.Property<string>("City")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("Complement")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("Neighborhood")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("Number")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("PostalCode")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("State")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("Street")
                                                .HasColumnType("longtext");

                                            b3.HasKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");

                                            b3.ToTable("QuodDataCurrentAddress");

                                            b3.WithOwner()
                                                .HasForeignKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");
                                        });

                                    b2.OwnsMany("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodEmail", "Emails", b3 =>
                                        {
                                            b3.Property<int>("ExternalQuodDataRecordApplicationUserId")
                                                .HasColumnType("int");

                                            b3.Property<int>("ExternalQuodDataRecordId")
                                                .HasColumnType("int");

                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("int");

                                            b3.Property<string>("Email")
                                                .HasColumnType("longtext");

                                            b3.Property<DateTime>("LastSeen")
                                                .HasColumnType("datetime(6)");

                                            b3.HasKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId", "Id");

                                            b3.ToTable("QuodDataEmails");

                                            b3.WithOwner()
                                                .HasForeignKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");
                                        });

                                    b2.OwnsOne("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodNegative", "Negative", b3 =>
                                        {
                                            b3.Property<int>("ExternalQuodDataRecordApplicationUserId")
                                                .HasColumnType("int");

                                            b3.Property<int>("ExternalQuodDataRecordId")
                                                .HasColumnType("int");

                                            b3.Property<decimal>("PendenciesControlCred")
                                                .HasColumnType("decimal(65,30)");

                                            b3.HasKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");

                                            b3.ToTable("QuodDataNegative");

                                            b3.WithOwner()
                                                .HasForeignKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");
                                        });

                                    b2.OwnsMany("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodPhoneNumber", "MobilePhoneNumbers", b3 =>
                                        {
                                            b3.Property<int>("ExternalQuodDataRecordApplicationUserId")
                                                .HasColumnType("int");

                                            b3.Property<int>("ExternalQuodDataRecordId")
                                                .HasColumnType("int");

                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("int");

                                            b3.Property<string>("Email")
                                                .HasColumnType("longtext");

                                            b3.Property<DateTime>("LastSeen")
                                                .HasColumnType("datetime(6)");

                                            b3.HasKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId", "Id");

                                            b3.ToTable("QuodDataMobilePhoneNumbers");

                                            b3.WithOwner()
                                                .HasForeignKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");
                                        });

                                    b2.OwnsMany("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodPhoneNumber", "PhoneNumbers", b3 =>
                                        {
                                            b3.Property<int>("ExternalQuodDataRecordApplicationUserId")
                                                .HasColumnType("int");

                                            b3.Property<int>("ExternalQuodDataRecordId")
                                                .HasColumnType("int");

                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("int");

                                            b3.Property<string>("Email")
                                                .HasColumnType("longtext");

                                            b3.Property<DateTime>("LastSeen")
                                                .HasColumnType("datetime(6)");

                                            b3.HasKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId", "Id");

                                            b3.ToTable("QuodDataPhoneNumbers");

                                            b3.WithOwner()
                                                .HasForeignKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");
                                        });

                                    b2.OwnsMany("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodProtest", "Protests", b3 =>
                                        {
                                            b3.Property<int>("ExternalQuodDataRecordApplicationUserId")
                                                .HasColumnType("int");

                                            b3.Property<int>("ExternalQuodDataRecordId")
                                                .HasColumnType("int");

                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("int");

                                            b3.Property<string>("Situacao")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("TotalProtestos")
                                                .HasColumnType("longtext");

                                            b3.Property<string>("ValorProtestadosTotal")
                                                .HasColumnType("longtext");

                                            b3.HasKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId", "Id");

                                            b3.ToTable("QuodDataProtests");

                                            b3.WithOwner()
                                                .HasForeignKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");
                                        });

                                    b2.OwnsOne("ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate.ExternalQuodScore", "Score", b3 =>
                                        {
                                            b3.Property<int>("ExternalQuodDataRecordApplicationUserId")
                                                .HasColumnType("int");

                                            b3.Property<int>("ExternalQuodDataRecordId")
                                                .HasColumnType("int");

                                            b3.Property<int?>("PaymentCommitmentScore")
                                                .HasColumnType("int");

                                            b3.Property<int?>("ProfileScore")
                                                .HasColumnType("int");

                                            b3.Property<int>("Score")
                                                .HasColumnType("int");

                                            b3.Property<string>("Segment")
                                                .HasColumnType("longtext");

                                            b3.Property<DateTime>("UpdateDate")
                                                .HasColumnType("datetime(6)");

                                            b3.Property<string>("UserUpdate")
                                                .HasColumnType("longtext");

                                            b3.HasKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");

                                            b3.ToTable("QuodDataScore");

                                            b3.WithOwner()
                                                .HasForeignKey("ExternalQuodDataRecordApplicationUserId", "ExternalQuodDataRecordId");
                                        });

                                    b2.Navigation("Addresses");

                                    b2.Navigation("CurrentAddress");

                                    b2.Navigation("Emails");

                                    b2.Navigation("MobilePhoneNumbers");

                                    b2.Navigation("Negative");

                                    b2.Navigation("PhoneNumbers");

                                    b2.Navigation("Protests");

                                    b2.Navigation("Score");
                                });

                            b1.Navigation("ExternalQuodData");
                        });

                    b.OwnsOne("ExternalEntities.Domain.AggregatesModel.UserAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<int>("ApplicationUserId")
                                .HasColumnType("int");

                            b1.Property<string>("Cep")
                                .HasColumnType("longtext");

                            b1.Property<string>("City")
                                .HasColumnType("longtext");

                            b1.Property<string>("Complement")
                                .HasColumnType("longtext");

                            b1.Property<string>("Country")
                                .HasColumnType("longtext");

                            b1.Property<string>("Neighborhood")
                                .HasColumnType("longtext");

                            b1.Property<int?>("Number")
                                .HasColumnType("int");

                            b1.Property<string>("ResidenceType")
                                .HasColumnType("longtext");

                            b1.Property<string>("State")
                                .HasColumnType("longtext");

                            b1.Property<string>("Street")
                                .HasColumnType("longtext");

                            b1.HasKey("ApplicationUserId");

                            b1.ToTable("UserAddress");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationUserId");
                        });

                    b.Navigation("Address");

                    b.Navigation("QuodDataRecords");
                });

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.BusinessAggregate.Business", b =>
                {
                    b.Navigation("Scores");
                });

            modelBuilder.Entity("ExternalEntities.Domain.AggregatesModel.UserAggregate.ApplicationUser", b =>
                {
                    b.Navigation("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}
