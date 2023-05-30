﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PsicoAppAPI.Data;

#nullable disable

namespace PsicoAppAPI.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("AppointmentStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RequestedUserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestingUserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentStatusId");

                    b.HasIndex("RequestedUserId");

                    b.HasIndex("RequestingUserId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.AppointmentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AppointmentStatuses");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<int>("ForumPostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ForumPostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.FeedPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("PublishedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FeedPosts");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.ForumPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("PublishedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ForumPosts");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Specialist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpecialityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("UserId");

                    b.ToTable("Specialists");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
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
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Appointment", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.AppointmentStatus", "AppointmentStatus")
                        .WithMany("Appointment")
                        .HasForeignKey("AppointmentStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PsicoAppAPI.Models.User", "RequestedUser")
                        .WithMany()
                        .HasForeignKey("RequestedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PsicoAppAPI.Models.User", "RequestingUser")
                        .WithMany("Appointment")
                        .HasForeignKey("RequestingUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppointmentStatus");

                    b.Navigation("RequestedUser");

                    b.Navigation("RequestingUser");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Client", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Comment", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.ForumPost", "ForumPost")
                        .WithMany("Comments")
                        .HasForeignKey("ForumPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PsicoAppAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("ForumPost");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.FeedPost", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.User", "User")
                        .WithMany("FeedPosts")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.ForumPost", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Specialist", b =>
                {
                    b.HasOne("PsicoAppAPI.Models.Speciality", "Speciality")
                        .WithMany("Specialists")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PsicoAppAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Speciality");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.AppointmentStatus", b =>
                {
                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.ForumPost", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.Speciality", b =>
                {
                    b.Navigation("Specialists");
                });

            modelBuilder.Entity("PsicoAppAPI.Models.User", b =>
                {
                    b.Navigation("Appointment");

                    b.Navigation("FeedPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
