﻿// <auto-generated />
using System;
using JeBalance.SQLLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JeBalance.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240130232553_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("JeBalance.SQLLite.Model.CalomniateurSQLS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Calomniateur");
                });

            modelBuilder.Entity("JeBalance.SQLLite.Model.DenonciationSQLS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<Guid>("DenonciationId")
                        .HasColumnType("TEXT")
                        .HasColumnName("denonciation_id");

                    b.Property<int>("DenonciationResponseId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EvasionCountry")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("evasion_country");

                    b.Property<int>("InformantId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Offense")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("offense");

                    b.Property<int>("SuspectId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT")
                        .HasColumnName("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("DenonciationId")
                        .IsUnique();

                    b.HasIndex("DenonciationResponseId");

                    b.HasIndex("InformantId");

                    b.HasIndex("SuspectId");

                    b.ToTable("Denonciation");
                });

            modelBuilder.Entity("JeBalance.SQLLite.Model.PersonSQLS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CityName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("city_name");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_admin");

                    b.Property<bool>("IsFisc")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_fisc");

                    b.Property<bool>("IsVIP")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_vip");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasColumnName("postal_code");

                    b.Property<int?>("Rejection")
                        .HasColumnType("INTEGER")
                        .HasColumnName("rejection");

                    b.Property<string>("StreetName")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasColumnName("street_name");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("TEXT")
                        .HasColumnName("street_number");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("JeBalance.SQLLite.Model.ResponseSQLS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("TEXT")
                        .HasColumnName("amount");

                    b.Property<Guid>("DenonciationId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ResponseType")
                        .HasColumnType("INTEGER")
                        .HasColumnName("response_type");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT")
                        .HasColumnName("timestamp");

                    b.HasKey("Id");

                    b.ToTable("Response", (string)null);
                });

            modelBuilder.Entity("JeBalance.SQLLite.Model.UserSQLS", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFisc")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsVip")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("person_id");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("PersonId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("JeBalance.SQLLite.Model.CalomniateurSQLS", b =>
                {
                    b.HasOne("JeBalance.SQLLite.Model.PersonSQLS", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("JeBalance.SQLLite.Model.DenonciationSQLS", b =>
                {
                    b.HasOne("JeBalance.SQLLite.Model.ResponseSQLS", "DenonciationResponse")
                        .WithMany()
                        .HasForeignKey("DenonciationResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JeBalance.SQLLite.Model.PersonSQLS", "Informant")
                        .WithMany()
                        .HasForeignKey("InformantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JeBalance.SQLLite.Model.PersonSQLS", "Suspect")
                        .WithMany()
                        .HasForeignKey("SuspectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DenonciationResponse");

                    b.Navigation("Informant");

                    b.Navigation("Suspect");
                });

            modelBuilder.Entity("JeBalance.SQLLite.Model.UserSQLS", b =>
                {
                    b.HasOne("JeBalance.SQLLite.Model.PersonSQLS", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });
#pragma warning restore 612, 618
        }
    }
}