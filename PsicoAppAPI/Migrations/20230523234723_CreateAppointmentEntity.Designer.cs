﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PsicoAppAPI.Data;

#nullable disable

namespace PsicoAppAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230523234723_CreateAppointmentEntity")]
    partial class CreateAppointmentEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("PsicoAppAPI.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientName")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpecialistId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("specialistName")
                        .HasColumnType("INTEGER");

                    b.Property<int>("specialisttId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("SpecialistId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .HasColumnType("TEXT");

                    b.Property<int>("ForumPostId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ForumPostTitle")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpecialistId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpecialistName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ForumPostId");

                    b.HasIndex("SpecialistId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.FeedPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("OnPublished")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpecialistId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpecialistName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("SpecialistId");

                    b.ToTable("FeedPosts");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.ForumPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly?>("PublishedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("ForumPosts");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("specialities");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstLastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("Phone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecondLastName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Client", b =>
                {
                    b.HasBaseType("PsicoAppAPI.Models.User");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Specialist", b =>
                {
                    b.HasBaseType("PsicoAppAPI.Models.User");

                    b.Property<int>("SpecialityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpecialityName")
                        .HasColumnType("INTEGER");

                    b.HasIndex("SpecialityId");

                    b.HasDiscriminator().HasValue("Specialist");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Appointment", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.Client", "Client")
                        .WithMany("Appointment")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PsicoAppAPI.Models.Specialist", "Specialist")
                        .WithMany("Appointment")
                        .HasForeignKey("SpecialistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Specialist");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Comment", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.ForumPost", "ForumPost")
                        .WithMany("Comments")
                        .HasForeignKey("ForumPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PsicoAppAPI.Models.Specialist", "specialist")
                        .WithMany("Comments")
                        .HasForeignKey("SpecialistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ForumPost");

                    b.Navigation("specialist");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.FeedPost", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.Client", null)
                        .WithMany("FeedPosts")
                        .HasForeignKey("ClientId");

                    b.HasOne("PsicoAppAPI.Models.Specialist", "Specialist")
                        .WithMany("FeedPosts")
                        .HasForeignKey("SpecialistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specialist");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.ForumPost", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Specialist", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.Speciality", null)
                        .WithMany("Specialists")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PsicoAppAPI.Models.ForumPost", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Speciality", b =>
                {
                    b.Navigation("Specialists");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Client", b =>
                {
                    b.Navigation("Appointment");

                    b.Navigation("FeedPosts");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Specialist", b =>
                {
                    b.Navigation("Appointment");

                    b.Navigation("Comments");

                    b.Navigation("FeedPosts");
                });
#pragma warning restore 612, 618
        }
    }
}